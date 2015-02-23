namespace RaygunLogging.Objects
{
    using RaygunLogging.Objects.Abstract;

    public class Employee : IEntity
    {
        #region Public Properties

        public int EmployeeId { get; set; }

        public string Name { get; set; }

        #endregion
    }
}