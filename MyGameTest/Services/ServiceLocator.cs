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

        public bool TryGetService<T>(out T result) where T : IService
        {
            if (!_services.TryGetValue(typeof(T), out var service))
            {
                result = default;
                return false;
            }
            result = (T)service;
            return true;
        }
    }

    public interface IService { }
}
