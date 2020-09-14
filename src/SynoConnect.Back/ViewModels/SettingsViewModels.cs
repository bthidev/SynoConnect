using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Splat;
using SynoConnect.Back.Api;
using Synology.DownloadStation.Info.Results;
using System;
using System.Reactive;
using System.Threading.Tasks;

namespace SynoConnect.Back.ViewModels
{
    public class SettingsViewModels : ViewModelBase
    {
        private readonly IServiceProvider serviceProvider;
        private readonly BaseSyno _syno;
        private IConfigResult _configResult;
        private bool _loaddingProgresse;
        public bool LoaddingProgresse
        {
            get => _loaddingProgresse;
            set
            {
                this.RaiseAndSetIfChanged(ref _loaddingProgresse, value);
            }
        }
        public ReactiveCommand<Unit, Unit> SaveCommand { get; }
        public IConfigResult ConfigResult
        {
            get => _configResult;
            set
            {
                this.RaiseAndSetIfChanged(ref _configResult, value);
            }
        }
        public SettingsViewModels()
        {
            LoaddingProgresse = true;
            serviceProvider = Locator.Current.GetService<IServiceProvider>();
            _syno = serviceProvider.GetService<BaseSyno>();
            SaveCommand = ReactiveCommand.CreateFromTask(SaveSettings);
        }
        private async Task SaveSettings()
        {
            await _syno.SetSettings(ConfigResult);
        }
        public async Task GetSettings()
        {
            LoaddingProgresse = true;
            ConfigResult = await _syno.GetSettings();
            LoaddingProgresse = false;

        }
    }
}
