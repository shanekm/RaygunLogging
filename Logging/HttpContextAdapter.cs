namespace Logging
{
    using System.Web;
    using Logging.Abstract;

    public class HttpContextAdapter : IHttpContext
    {
        #region Fields

        private readonly HttpContext _httpContext;

        #endregion

        #region Constructors and Destructors

        public HttpContextAdapter()
        {
            this._httpContext = HttpContext.Current;
        }

        #endregion

        #region Public Properties

        public HttpApplication ApplicationInstance
        {
            get
            {
                return this._httpContext != null ? this._httpContext.ApplicationInstance : null;
            }
        }

        public HttpContext Instance
        {
            get
            {
                return this._httpContext;
            }
        }

        #endregion
    }
}