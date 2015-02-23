namespace Logging
{
    using System;
    using Logging.Abstract;
    using Mindscape.Raygun4Net.Messages;
    using NLog;

    public class RaygunMessageBuilder : IRaygunMessageBuilder
    {
        #region Static Fields

        public static readonly Type DeclaringType = typeof(RayGunTarget);

        #endregion

        #region Public Methods and Operators

        public RaygunMessage BuildMessage(Exception exception, LogEventInfo logEventInfo)
        {
            var raygunMessageBuilder = Mindscape.Raygun4Net.RaygunMessageBuilder.New;
            var assemblyResolver = new AssemblyResolver();
            var applicationAssembly = assemblyResolver.GetApplicationAssembly();

            raygunMessageBuilder.SetExceptionDetails(exception)
                .SetClientDetails()
                .SetVersion(applicationAssembly != null ? applicationAssembly.GetName().Version.ToString() : null)
                .SetTags(new[] { logEventInfo.Level.Name, logEventInfo.LoggerName })
                .SetMachineName(Environment.MachineName);

            var raygunMessage = raygunMessageBuilder.Build();

            if (exception != null)
            {
                if (raygunMessage.Details.Error != null)
                {
                    raygunMessage.Details.Error.Message = string.Format(
                        "{0}: {1}",
                        logEventInfo.LoggerName ?? exception.GetType().Name,
                        logEventInfo.FormattedMessage);
                }
            }
            else
            {
                raygunMessage.Details.Error = new RaygunErrorMessage { Message = logEventInfo.FormattedMessage };
            }

            return raygunMessage;
        }

        #endregion
    }
}