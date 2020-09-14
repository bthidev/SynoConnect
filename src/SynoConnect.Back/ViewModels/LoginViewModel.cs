using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Splat;
using SynoConnect.Back.Api;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace SynoConnect.Back.ViewModels
{
    public class LoginViewModel : ViewModelBase, IRoutableViewModel
    {
        readonly IServiceProvider serviceProvider;
        readonly ConfigService _configService;
        private bool _loginProgresse;
        public ViewModelActivator Activator { get; }
        public string Username { get; set; }
        public string Url { get; set; }
        public string PassWord { get; set; }
        public bool Stayloggin { get; set; }
        public bool LoginProgresse
        {
            get => _loginProgresse;
            set
            {
                this.RaiseAndSetIfChanged(ref _loginProgresse, value);
            }
        }
        public ReactiveCommand<Unit, bool> LoginCommand { get; }

        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

        public IScreen HostScreen { get; }


        public LoginViewModel(IScreen screen)
        {
            serviceProvider = Locator.Current.GetService<IServiceProvider>();
            _configService = serviceProvider.GetService<ConfigService>();

            Url = _configService.Settings.Url;
            Username = _configService.Settings.Username;
            PassWord = _configService.Settings.PassWord;
            Stayloggin = _configService.Settings.Stayloggin;


            HostScreen = screen;
            LoginCommand = ReactiveCommand.CreateFromTask(LogUser);
        }

        public async Task TryLogUser()
        {
            if (Stayloggin)
            {
                await LogUser();
            }
        }
        private async Task<bool> LogUser()
        {
            LoginProgresse = true;
            BaseSyno syno = serviceProvider.GetService<BaseSyno>();
            if (await syno.ConnectUser(Url, Username, PassWord))
            {
                _configService.Settings.Url = Url;
                _configService.Settings.Username = Username;
                if (Stayloggin)
                {
                    _configService.Settings.PassWord = PassWord;
                    _configService.Settings.Stayloggin = Stayloggin;
                }
                _configService.SetConfig();
                await HostScreen.Router.Navigate.Execute(new DownloadStationViewModel(HostScreen));

            }
            LoginProgresse = false;

            return true;
        }

    }
}
