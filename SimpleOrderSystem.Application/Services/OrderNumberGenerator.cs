

using SimpleOrderSystem.Application.Interfaces;

namespace SimpleOrderSystem.Application.Services
{
    public sealed class OrderNumberGenerator : IOrderNumberGenerator
    {
        private int _counter = 1000;
        private readonly object _lock = new();

        public string Generate()
        {
            lock (_lock)
            {
                _counter++;
                return $"ORD-{_counter}";

            }
        }
    }
}
