namespace Logging
{
    using System;
    using System.Linq;
    using System.Web.UI.WebControls;

    using Mindscape.Raygun4Net;
    using Mindscape.Raygun4Net.Messages;
    using NLog;
    using NLog.Config;
    using NLog.Targets;

    [Target("RayGun")]
    public class RayGunTarget : TargetWithLayout
    {
        #region Public Properties

        [RequiredParameter]
        public string ApiKey { get; set; }

        [RequiredParameter]
        public string IgnoreCookieNames { get; set; }

        [RequiredParameter]
        public string IgnoreFormFieldNames { get; set; }

        [RequiredParameter]
        public string IgnoreHeaderNames { get; set; }

        [RequiredParameter]
        public string IgnoreServerVariableNames { get; set; }

        #endregion

        #region Methods

        protected override void Write(LogEventInfo logEvent)
        {
            Exception exception = ResolveLoggedExceptionObject(logEvent);

            if (exception != null)
            {
                this.SendMessage(exception, logEvent);
            }
        }

        private static Exception ResolveLoggedExceptionObject(LogEventInfo loggingEvent)
        {
            Exception exception = null;

            var exceptionObject = loggingEvent.Exception;
            if (exceptionObject != null)
            {
                exception = exceptionObject.GetBaseException();
            }

            if (exception == null)
            {
                var messageObject = loggingEvent.Message;
                if (messageObject != null)
                {
                    var messageObjectAsException = new Exception(messageObject);
                    exception = messageObjectAsException;
                }
            }

            return exception;
        }

        private RaygunClient CreateRaygunClient()
        {
            var client = new RaygunClient(this.ApiKey);

            client.IgnoreFormFieldNames(this.SplitValues(this.IgnoreFormFieldNames));
            client.IgnoreCookieNames(this.SplitValues(this.IgnoreCookieNames));
            client.IgnoreHeaderNames(this.SplitValues(this.IgnoreHeaderNames));
            client.IgnoreServerVariableNames(this.SplitValues(this.IgnoreServerVariableNames));

            return client;
        }

        private void SendMessage(Exception exception, LogEventInfo logEvent)
        {
            RaygunMessage raygunMessage = new RaygunMessageBuilder().BuildMessage(exception, logEvent);

            var client = this.CreateRaygunClient();
            client.SendInBackground(raygunMessage);
        }

        private string[] SplitValues(string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Split(',');
            }

            return new[] { string.Empty };
        }

        #endregion
    }
}