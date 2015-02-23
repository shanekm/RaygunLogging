namespace Logging
{
    using System.Reflection;
    using Logging.Abstract;

    public class AssemblyResolver
    {
        #region Constants

        private const string AspNamespace = "ASP";

        #endregion

        #region Fields

        private readonly IAssembly _assemblyLoader;

        private readonly IHttpContext _httpContext;

        #endregion

        #region Constructors and Destructors

        public AssemblyResolver()
            : this(new HttpContextAdapter(), new AssemblyAdapter())
        {
        }

        internal AssemblyResolver(IHttpContext httpContext, IAssembly assemblyLoader)
        {
            this._httpContext = httpContext;
            this._assemblyLoader = assemblyLoader;
        }

        #endregion

        #region Public Methods and Operators

        public Assembly GetApplicationAssembly()
        {
            var baseWebApplicationAssembly = this.GetWebApplicationAssembly();

            if (baseWebApplicationAssembly != null)
            {
                return baseWebApplicationAssembly;
            }

            return this._assemblyLoader.GetEntryAssembly();
        }

        #endregion

        #region Methods

        private Assembly GetWebApplicationAssembly()
        {
            if (this._httpContext != null && this._httpContext.ApplicationInstance != null)
            {
                var webApplicationType = this._httpContext.ApplicationInstance.GetType();

                if (webApplicationType != null)
                {
                    while (webApplicationType.Namespace == AspNamespace)
                    {
                        webApplicationType = webApplicationType.BaseType;
                    }

                    return webApplicationType.Assembly;
                }
            }

            return null;
        }

        #endregion
    }
}