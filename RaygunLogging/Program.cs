namespace RaygunLogging
{
    using System;
    using System.Linq;
    using System.Reflection;
    using log4net;
    using log4net.Config;
    using NLog;
    using log4net.Util;
    using Ninject;
    using Ninject.Modules;
    using RaygunLogging.Objects;
    using RaygunLogging.Objects.Abstract;
    using RaygunLogging.Repository;
    using RaygunLogging.Repository.Abstract;
    using RaygunLogging.Service;
    using LogManagerLog4net = log4net.LogManager;
    using LogManagerNLog = NLog.LogManager;

    internal class Program
    {
        #region Static Fields

        // log4net
        private static readonly ILog Log4Net = LogManagerLog4net.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly Logger NLog = LogManagerNLog.GetCurrentClassLogger();

        #endregion

        #region Methods

        //private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        //{
        //    // Log4Net.Error(e.ExceptionObject);
        //    NLog.Error(e.ExceptionObject);
        //}


        private static void Main(string[] args)
        {
            // Log4net 
            // Init - in AssemblyInfo.cs
            // [assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
            // Load log4net configuration
            XmlConfigurator.Configure();

            // Check valid log4net configuration
            Log4NetConfig.Verify();

            // Unhandled exceptions
            //AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            // NLog - Logging to log and raygun.io
            NLog.Trace("NLog Sample trace message");
            NLog.Debug("NLog Sample debug message");
            NLog.Info("NLog Sample informational message");
            NLog.Warn("NLog Sample warning message");
            NLog.Error("NLog Sample error message");
            NLog.Fatal("NLog Sample fatal error message");

            // Log4Net - Logging to log and raygun.io
            Log4Net.Debug("Log4Net Sample debug message");
            Log4Net.Info("Log4Net Sample informational message");
            Log4Net.Warn("Log4Net Sample warning message");
            Log4Net.Error("Log4Net Sample error message");
            Log4Net.Fatal("Log4Net Sample fatal error message");
            

            // alternatively you can call the Log() method 
            // and pass log level as the parameter.
            NLog.Log(LogLevel.Info, "Sample informational message");

            // Throw exception
            try
            {
                throw new Exception("Test Exception");
            }
            catch (Exception ex)
            {
                NLog.Error(ex);
            }

            // IoC
            var kernel = new StandardKernel(new NinjectDep());
            var service = kernel.Get<Service<Person>>();

            service.AddItem(new Person { Age = 21, Name = "John" });

            try
            {
                // This should throw an error
                service.AddItem((Person)(new Employee { EmployeeId = 33, Name = "Bill" } as IEntity));
            }
            catch (Exception ex)
            {
                NLog.Error(ex);
            }

            service.DisplayItems();

            Console.WriteLine("Finished...");
            Console.Read();
        }

        #endregion

        // IoC
        public static class Log4NetConfig
        {
            #region Public Methods and Operators

            public static void Verify()
            {
                if (!LogManagerLog4net.GetRepository().Configured)
                {
                    // log4net not configured
                    foreach (
                        LogLog message in
                            LogManagerLog4net.GetRepository().ConfigurationMessages.Cast<LogLog>())
                    {
                        // evaluate configuration message
                        var m = message;
                    }
                }
            }

            #endregion
        }

        public class NinjectDep : NinjectModule
        {
            #region Public Methods and Operators

            public override void Load()
            {
                this.Bind<ILog>().ToMethod(context => LogManagerLog4net.GetLogger(context.Request.Target.Member.ReflectedType));
                this.Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            }

            #endregion
        }
    }
}