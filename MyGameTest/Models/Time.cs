using MyGameTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MyGameTest.Models
{
    public class Time : ITime
    {
        private Timer _timer;
        private int _ticksPerSec = 50;
        private double _baseDeltaTime;
        private int _extraTicks = 0;
        private bool _isUpdating = false;
        private DateTime _lastUpdate;

        public Time()
        {
            _lastUpdate = DateTime.Now;
            _baseDeltaTime = 1d / _ticksPerSec;

            _timer = new Timer(_baseDeltaTime * 1000);
            _timer.AutoReset = true;
            _timer.Elapsed += OnElapsed;
        }

        private void OnElapsed(object sender, ElapsedEventArgs e)
        {
            if (_isUpdating)
            {
                _extraTicks++;
                return;
            }

            _isUpdating = true;
            var deltaTime = _baseDeltaTime * (1 + _extraTicks);
            _extraTicks = 0;
            //OnUpdate?.Invoke(deltaTime);

            OnUpdate?.Invoke((e.SignalTime - _lastUpdate).TotalSeconds);
            _lastUpdate = e.SignalTime;

            _isUpdating = false;
        }

        public event Action<double> OnUpdate;

        public void Start() => _timer.Start();
    }
}
