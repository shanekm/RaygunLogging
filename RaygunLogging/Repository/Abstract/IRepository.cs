namespace RaygunLogging.Repository.Abstract
{
    using System.Collections.Generic;

    public interface IRepository<T>
    {
        #region Public Methods and Operators

        void AddItem(T item);

        IEnumerable<T> GetItems();

        T GetItem(string name);

        #endregion
    }
}