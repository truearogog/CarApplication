using CarApplication.Domain.Commands;
using CarApplication.Domain.Queries;
using CarApplication.EntityFramework.Commands;
using CarApplication.EntityFramework.Queries;
using CarApplication.WPF.Stores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApplication.WPF.HostBuilders
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddCommands(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<IGetAllCarsQuery, GetAllCarsQuery>();
                services.AddSingleton<ICreateCarCommand, CreateCarCommand>();
                services.AddSingleton<IUpdateCarCommand, UpdateCarCommand>();
                services.AddSingleton<IDeleteCarCommand, DeleteCarCommand>();
            });

            return hostBuilder;
        }

        public static IHostBuilder AddStores(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<ModalNavigationStore>();
                services.AddSingleton<CarsStore>();
                services.AddSingleton<SelectedCarStore>();
            });

            return hostBuilder;
        }
    }
}
