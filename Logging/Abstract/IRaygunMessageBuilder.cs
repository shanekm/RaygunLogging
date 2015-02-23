namespace Logging.Abstract
{
    using System;
    using Mindscape.Raygun4Net.Messages;
    using NLog;

    public interface IRaygunMessageBuilder
    {
        #region Public Methods and Operators

        RaygunMessage BuildMessage(Exception exception, LogEventInfo logEventInfo);

        #endregion
    }
}