using MyGameTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyGameTest.Models
{
    public class Time : ITime
    {
        private Task _loopTask;
        private int _fps;
        private CancellationTokenSource _cts;

        public Time(int fps = 60)
        {
            _fps = Math.Min(Math.Max(fps, 1), 500);
        }

        public void Loop(CancellationToken token)
        {
            var lastUpdateTime = DateTime.UtcNow;
            var baseDeltaTime = 1d / _fps;
            while (!token.IsCancellationRequested)
            {
                var timeNow = DateTime.UtcNow;
                var deltaTime = (timeNow - lastUpdateTime).TotalSeconds;
                lastUpdateTime = timeNow;
                OnUpdate?.Invoke(deltaTime);

                var nextUpdateTime = timeNow.AddSeconds(baseDeltaTime).AddMilliseconds(-1);
                while (DateTime.UtcNow < nextUpdateTime)
                    Thread.Sleep(1);
            }
        }

        public event Action<double> OnUpdate;

        public void Start()
        {
            Pause();
            _cts = new CancellationTokenSource();
            _loopTask = new Task(() => Loop(_cts.Token));
            _loopTask.Start();
        }

        public void Pause()
        {
            _cts?.Cancel();
            _loopTask = null;
        }
    }
}
