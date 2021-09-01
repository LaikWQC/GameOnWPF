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

        private Queue<DateTime> _log = new Queue<DateTime>();
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
            var timeLimit = DateTime.UtcNow.AddSeconds(-1);
            lock (_log)
            {
                var stillRemove = true;
                while(stillRemove)
                {
                    if(_log.Peek()>timeLimit)
                        stillRemove = false;
                    else
                        _log.Dequeue();
                }
                FPS = _log.Count();
            }                
        }
        protected override void Update(double deltaTime)
        {
            lock (_log)
            {
                _log.Enqueue(DateTime.UtcNow);
            }                
        }

        public int FPS
        {
            get => _fps;
            set
            {
                if (_fps == value) return;
                _fps = value;
                FpsChanged?.Invoke();
            }
        }
        private int _fps;

        public event Action FpsChanged;
    }
}
