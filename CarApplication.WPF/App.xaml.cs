using CarApplication.Domain.Commands;
using CarApplication.Domain.Queries;
using CarApplication.EntityFramework;
using CarApplication.EntityFramework.Commands;
using CarApplication.EntityFramework.Queries;
using CarApplication.WPF.Stores;
using CarApplication.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CarApplication.WPF.HostBuilders;

namespace CarApplication.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .AddDbContext()
                .AddCommands()
                .AddStores()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<CarsViewModel>((services) => CarsViewModel.LoadViewModel(
                        services.GetRequiredService<CarsStore>(),
                        services.GetRequiredService<SelectedCarStore>(),
                        services.GetRequiredService<ModalNavigationStore>()
                        ));

                    services.AddSingleton<MainViewModel>(); 
                    services.AddSingleton<MainWindow>((services) => new MainWindow()
                    {
                        DataContext = services.GetRequiredService<MainViewModel>()
                    });
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            var carsDbContextFactory = _host.Services.GetRequiredService<CarsDbContextFactory>();
            using (var context = carsDbContextFactory.Create())
            {
                context.Database.Migrate();
            }

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
    }
}
