using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Trending.Core;
using Trending.MVVM.ViewModel;

namespace Trending
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<MainWindow>(provider => new MainWindow()
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });

            services.AddTransient<Func<Type, ViewModelBase>>(provider => viewModelType => (ViewModelBase)provider.GetRequiredService(viewModelType));

            // register ViewModel classes for injection here (use AddTransient)
            services.AddSingleton<MainViewModel>();
            services.AddTransient<LogInViewModel>();
            services.AddTransient<MonitorInputsViewModel>();
            services.AddTransient<CreateUserViewModel>();
            services.AddTransient<CreateInputViewModel>();
            services.AddTransient<CreateOutputViewModel>();
            services.AddTransient<DbAnalogInputsViewModel>();
            services.AddTransient<DbAnalogOutputsViewModel>();
            services.AddTransient<DbDigitalInputsViewModel>();
            services.AddTransient<DbDigitalOutputsViewModel>();
            services.AddTransient<DbUsersViewModel>();

            services.AddSingleton<NavigationService>();

            _serviceProvider = services.BuildServiceProvider();

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }
    }
}
