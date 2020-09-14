using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Splat;
using SynoConnect.Back.Api;
using SynoConnect.Back.ViewModels;
using SynoConnect.Translatte;
using System;
using System.Reactive;
using System.Threading.Tasks;

namespace SynoConnect.Desktop.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IScreen
    {
        // The Router associated with this Screen.
        // Required by the IScreen interface.
        public RoutingState Router { get; } = new RoutingState();
        public string LoaddingMessage { get; private set; }

        // The command that navigates a user to first view model.
        public ReactiveCommand<Unit, IRoutableViewModel> GoNext { get; }

        // The command that navigates a user back.
        public ReactiveCommand<Unit, Unit> GoBack => Router.NavigateBack;
        public ReactiveCommand<Unit, Unit> LoaddCommand { get; }

        public MainWindowViewModel()
        {
            // Manage the routing state. Use the Router.Navigate.Execute
            // command to navigate to different view models. 
            //
            // Note, that the Navigate.Execute method accepts an instance 
            // of a view model, this allows you to pass parameters to 
            // your view models, or to reuse existing view models.
            //

            GoNext = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new LoginViewModel(this))
            );
            LoaddCommand = ReactiveCommand.CreateFromTask(Loadder);
        }

        public async Task Loadder()
        {
            await Task.Run(() => { });
            LoaddingMessage = "Load translatte";
            Locator.CurrentMutable.RegisterConstant(new Translattor(), typeof(Translattor));
            LoaddingMessage = Locator.Current.GetService<Translattor>().GetTranslatte("MainWindows/Loading");
            IServiceProvider provider = Locator.Current.GetService<IServiceProvider>();
            provider.GetService<ConfigService>().GetConfig();
            Router.Navigate.Execute(new LoginViewModel(this));
        }
    }
}
