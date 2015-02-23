namespace Logging.Abstract
{
    using System.Reflection;

    public interface IAssembly
    {
        #region Public Methods and Operators

        Assembly GetEntryAssembly();

        #endregion
    }
}