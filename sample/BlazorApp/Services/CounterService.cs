using DependencyInjectionExtensions;

namespace BlazorApp.Services
{
    public interface ICounterService : ITransient
    {
        int Count { get; }

        void IncrementCount();
    }

    public class CounterService : ICounterService
    {
        public int Count { get; private set; } = 0;

        public void IncrementCount() => Count++;
    }
}
