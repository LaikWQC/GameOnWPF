using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameTest.Services
{
    public class ServiceLocator
    {
        private Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

        private ServiceLocator() { }
        public static ServiceLocator Current { get; } = new ServiceLocator();

        public void InitializeService(IService service)
        {
            var type = service.GetType();
            _services[type] = service;
        }

        public T GetService<T>() where T : IService
        {
            if (!_services.TryGetValue(typeof(T), out var service))
                return default;
            return (T)service;
        }
    }

    public interface IService { }
}
