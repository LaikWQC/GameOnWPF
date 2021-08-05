using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MyGameTest.Models
{
    public class FpsCalculator : TimeObject
    {
        public static FpsCalculator Instance { get; private set; }
        public static void Initialize(double updateFrequency = 1)
        {
            Instance = new FpsCalculator(updateFrequency);
        }

        private List<DateTime> _log = new List<DateTime>();
        private Timer _timer;

        private FpsCalculator(double updateFrequency) 
        {
            _timer = new Timer(updateFrequency * 1000);
            _timer.AutoReset = true;
            _timer.Elapsed += OnElapsed;
            _timer.Start();
        }

        private void OnElapsed(object sender, ElapsedEventArgs e)
        {
            var timeNow = DateTime.Now;
            lock (_log)
                FPS = _log.Count(x => (timeNow - x).TotalSeconds < 1);
        }
        protected override void Update(double deltaTime)
        {
            lock (_log)
            {
                _log.Add(DateTime.Now);
                if (_log.Count > 100)
                    _log.RemoveAt(0);
            }                
        }

        public int FPS
        {
            get => _fps;
            set => Set(() => FPS, ref _fps, value);
        }
        private int _fps;
    }
}
