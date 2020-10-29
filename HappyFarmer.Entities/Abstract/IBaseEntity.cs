namespace HappyFarmer.Entities.Abstract
{
    public interface IBaseEntity<T>
    {
        public T Id { get; set; }
    }
}
