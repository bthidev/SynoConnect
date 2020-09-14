using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Splat;
using SynoConnect.Back.Api;
using SynoConnect.Desktop.ViewModels;
using SynoConnect.Desktop.Views;
using Synology;
using System;

namespace SynoConnect.Desktop
{
    public class App : Application
    {
        public IServiceProvider Container { get; private set; }
        public override void Initialize()
        {
            Init();
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                //var temp = Container.GetService<IViewFor<MainWindowViewModel>>();
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel()
                };
            }

            base.OnFrameworkInitializationCompleted();

        }
        void Init()
        {
            IHost host = Host
              .CreateDefaultBuilder()
              .ConfigureServices(services => { ConfigureServices2(services); })
              .UseEnvironment(Environments.Development)
              .Build();
            // Since MS DI container is a different type,
            // we need to re-register the built container with Splat again
            Container = host.Services;
            Locator.CurrentMutable.RegisterConstant(Container, typeof(IServiceProvider));
        }
        void ConfigureServices2(IServiceCollection services)
        {
            services.AddSynology();
            services.AddLogging();
            services.AddSingleton<ConfigService>();
            services.AddSingleton<BaseSyno>();
            // register your personal services here, for example
        }
    }
}