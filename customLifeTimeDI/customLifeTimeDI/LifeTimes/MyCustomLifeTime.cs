using System.Collections.Concurrent;

namespace customLifeTimeDI.LifeTimes
{
    public class MyCustomLifeTime : IServiceScopeFactory
    {
        private readonly ConcurrentDictionary<object, object> _instances = new ConcurrentDictionary<object, object>();
        public IServiceScope CreateScope()
        {
            return new MyCustomLifeTimeScope(_instances);
        }

        private class MyCustomLifeTimeScope : IServiceScope
        {
            private readonly ConcurrentDictionary<object, object> _instances;

            public MyCustomLifeTimeScope(ConcurrentDictionary<object, object> instances)
            {
                _instances = instances;
            }



            public IServiceProvider ServiceProvider => new MyCustomServiceProvider(_instances);

            public void Dispose()
            {
                foreach (var instance in _instances.Values)
                {
                    if (instance is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }

                _instances.Clear();
            }
        }

        private class MyCustomServiceProvider : IServiceProvider
        {
            private readonly ConcurrentDictionary<object, object> _instances;

            public MyCustomServiceProvider(ConcurrentDictionary<object, object> instances)
            {
                _instances = instances;
            }

            public object? GetService(Type serviceType)
            {
                if (_instances.TryGetValue(serviceType, out var instance))
                {
                    return instance;
                }

                instance = ActivatorUtilities.CreateInstance(null, serviceType);
                _instances.TryAdd(serviceType, instance);
                return instance;

            }
        }
    }
}
