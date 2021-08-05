using MyGameTest.Models;
using MyGameTest.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MyGameTest
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var timeService = new TimeService(new Time());
            ServiceLocator.Current.InitializeService(timeService);

            timeService.Start();
            FpsCalculator.Initialize(0.25);
        }
    }
}
