namespace RaygunLogging.Service
{
    using System;
    using System.Collections.Generic;
    using log4net;
    using RaygunLogging.Objects.Abstract;
    using RaygunLogging.Repository.Abstract;

    public class Service<T>
        where T : IEntity
    {
        #region Fields

        private readonly ILog log;

        private readonly IRepository<T> repository;

        #endregion

        #region Constructors and Destructors

        public Service(IRepository<T> repository, ILog log)
        {
            this.repository = repository;
            this.log = log;
        }

        #endregion

        #region Public Methods and Operators

        public void AddItem(T item)
        {
            this.repository.AddItem(item);
        }

        public void AddItems(ICollection<T> items)
        {
            foreach (T item in items)
            {
                this.repository.AddItem(item);
            }
        }

        public void DisplayItems()
        {
            foreach (T person in this.repository.GetItems())
            {
                Console.WriteLine(person.Name);
                this.log.Warn("Test warn message in service");
            }
        }

        #endregion
    }
}