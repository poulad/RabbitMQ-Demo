namespace Demo.Services
{
    public interface IQueueService
    {
        void Publish(object message);

        T Dequeue<T>();
    }
}