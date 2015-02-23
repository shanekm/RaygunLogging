namespace UnitTests
{
    using log4net;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ninject;
    using RaygunLogging.Objects;
    using RaygunLogging.Objects.Abstract;
    using RaygunLogging.Repository;
    using RaygunLogging.Repository.Abstract;

    [TestClass]
    public class UnitTests
    {
        #region Public Methods and Operators

        [TestMethod]
        public void Can_Resolve_Concrete_Type_With_Generics()
        {
            var kernel = new StandardKernel();
            kernel.Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target.Member.ReflectedType));
            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            var item = kernel.Get(typeof(IRepository<IEntity>));

            // Type correct?
            Assert.AreNotEqual(typeof(IRepository<Employee>), item.GetType());
        }

        [TestMethod]
        public void Can_Resolve_Concrete_Type_With_Generics_Correct_Type()
        {
            var kernel = new StandardKernel();
            kernel.Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target.Member.ReflectedType));
            kernel.Bind<IRepository<Person>>().To<Repository<Person>>();
            var item = kernel.Get(typeof(IRepository<Person>));

            // Type correct?
            Assert.AreNotEqual(typeof(Repository<Employee>), item.GetType());
        }

        [TestMethod]
        public void Can_Resolve_Concrete_Type_With_Generics_Incorrect_Type()
        {
            var kernel = new StandardKernel();
            kernel.Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target.Member.ReflectedType));
            kernel.Bind<IRepository<Person>>().To<Repository<Person>>();
            var item = kernel.Get(typeof(IRepository<Person>));

            // Type correct?
            Assert.AreEqual(typeof(Repository<Person>), item.GetType());
        }

        #endregion
    }
}