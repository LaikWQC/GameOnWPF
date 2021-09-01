using MyGameTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameTest.Models
{
    public class Time : ITime
    {
        private Task _loopTask;
        private int _fps;

        public Time(int fps = 60)
        {
            _fps = Math.Min(Math.Max(fps, 1), 500);
        }

        public void Loop()
        {
            var lastUpdateTime = DateTime.UtcNow;
            var baseDeltaTime = 1d / _fps;
            //TODO add cancelation token
            while (true)
            {
                var timeNow = DateTime.UtcNow;
                var deltaTime = (timeNow - lastUpdateTime).TotalSeconds;
                lastUpdateTime = timeNow;
                OnUpdate?.Invoke(deltaTime);

                var nextUpdateTime = timeNow.AddSeconds(baseDeltaTime).AddMilliseconds(-1);
                while (DateTime.UtcNow < nextUpdateTime)
                    System.Threading.Thread.Sleep(1);
            }
        }

        public event Action<double> OnUpdate;

        public void Start()
        {
            _loopTask = new Task(Loop);
            _loopTask.Start();
        }
    }
}
