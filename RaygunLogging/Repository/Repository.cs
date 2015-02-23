namespace RaygunLogging.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using log4net;
    using RaygunLogging.Objects.Abstract;
    using RaygunLogging.Repository.Abstract;

    public class Repository<T> : IRepository<T>
        where T : class, IEntity
    {
        #region Fields

        private readonly IList<T> list = new List<T>();

        private readonly ILog log;

        #endregion

        #region Constructors and Destructors

        public Repository(ILog log)
        {
            this.log = log;
        }

        #endregion

        #region Public Methods and Operators

        public void AddItem(T item)
        {
            this.log.Debug("Debug: Adding items in repo");
            this.list.Add(item);
        }

        public T GetItem(string name)
        {
            this.log.Debug("Debug: Getting item from repo");
            return this.list.FirstOrDefault(x => x.Name == name);
        }

        public IEnumerable<T> GetItems()
        {
            this.log.Info("Info: Getting items from repo");
            return this.list;
        }

        #endregion
    }
}