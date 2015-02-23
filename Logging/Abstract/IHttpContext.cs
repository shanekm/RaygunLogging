namespace Logging.Abstract
{
    using System.Web;

    public interface IHttpContext
    {
        #region Public Properties

        HttpApplication ApplicationInstance { get; }

        HttpContext Instance { get; }

        #endregion
    }
}