namespace Infrastructure.Exceptions
{
    /// <summary>
    /// check entity not found ?
    /// </summary>
    public class EntityNotFoundException : ApplicationException
    {
        public EntityNotFoundException() : base("Entity was not found.")
        {
        }
    }
}
