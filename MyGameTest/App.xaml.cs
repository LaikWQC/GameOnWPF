using MyGameTest.Data;
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
            ServiceLocator.Current.InitializeService(new LocationService());
            ServiceLocator.Current.InitializeService(new HeroService());

            Loader.Load();

            timeService.Start();
            FpsCalculator.Initialize(0.25);
        }
    }
}
