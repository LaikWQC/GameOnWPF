using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameTest.Services
{
    public class TimeService : IService
    {
        private ITime _time;
        public TimeService(ITime time)
        {
            _time = time;
            _time.OnUpdate += OnTimerUpdate;
        }

        private void OnTimerUpdate(double deltaTime)
        {
            OnUpdate?.Invoke(deltaTime);
        }

        public event Action<double> OnUpdate;
        public void Start() => _time?.Start();
    }

    public interface ITime
    {
        event Action<double> OnUpdate;
        void Start();
    }
}
