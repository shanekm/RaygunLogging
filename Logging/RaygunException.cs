namespace Logging
{
    using System;

    public class RaygunException : Exception
    {
        #region Constructors and Destructors

        public RaygunException(string message)
            : base(message)
        {
        }

        public RaygunException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion
    }
}