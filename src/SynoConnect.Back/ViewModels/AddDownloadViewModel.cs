using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Splat;
using SynoConnect.Back.Api;
using SynoConnect.Back.Models;
using System;
using System.Reactive;
using System.Threading.Tasks;

namespace SynoConnect.Back.ViewModels
{
    public class AddDownloadViewModel : ViewModelBase
    {
        private NewDownloadModels _downloadModel;
        private readonly IServiceProvider serviceProvider;
        private readonly BaseSyno _syno;

        public ReactiveCommand<Unit, Unit> SaveCommand { get; }

        public NewDownloadModels DownloadModels
        {
            get => _downloadModel;
            set
            {
                this.RaiseAndSetIfChanged(ref _downloadModel, value);
            }
        }
        public async Task GetSettings()
        {
            Synology.DownloadStation.Info.Results.IConfigResult Result = await _syno.GetSettings();
            DownloadModels = new NewDownloadModels
            {
                Destination = Result.DefaultDestination
            };

        }
        public AddDownloadViewModel()
        {
            serviceProvider = Locator.Current.GetService<IServiceProvider>();
            _syno = serviceProvider.GetService<BaseSyno>();
            SaveCommand = ReactiveCommand.CreateFromTask(SaveDownload);
        }

        private async Task SaveDownload()
        {
            await _syno.AddTask(DownloadModels);

        }
    }
}
