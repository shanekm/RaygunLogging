namespace RaygunLogging.Objects
{
    using RaygunLogging.Objects.Abstract;

    public class Person : IEntity
    {
        #region Public Properties

        public int Age { get; set; }

        public string Name { get; set; }

        #endregion
    }
}