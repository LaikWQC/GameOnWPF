using MyGameTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyGameTest.ViewModels
{
    public class LocationViewModel : TimeObject
    {
        protected override void Start(double deltaTime)
        {
        }
        protected override void Update(double deltaTime)
        {
            Test = deltaTime;
            RaisePropertyChanged(() => Test);
        }

        public double Test { get; set; }
    }
}
