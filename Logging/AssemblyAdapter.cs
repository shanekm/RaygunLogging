namespace Logging
{
    using System.Reflection;
    using Logging.Abstract;

    public class AssemblyAdapter : IAssembly
    {
        #region Public Methods and Operators

        public Assembly GetEntryAssembly()
        {
            return Assembly.GetEntryAssembly();
        }

        #endregion
    }
}