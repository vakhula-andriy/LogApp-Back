namespace LogApp.Core.Models
{
    public interface IEntity<T>
    {
        public T ID { get; set; }
    }
}
