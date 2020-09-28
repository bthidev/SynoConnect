using DynamicData;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Splat;
using SynoConnect.Back.Api;
using SynoConnect.Back.Models;
using Synology.DownloadStation.Statistic.Results;
using Synology.DownloadStation.Task.Results;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace SynoConnect.Back.ViewModels
{
    public class DownloadStationViewModel : ViewModelBase, IRoutableViewModel
    {
        readonly IServiceProvider serviceProvider;
        private IStatisticResult statisticResult;
        private bool _loginProgresse;
        private List<ITaskResult> internalList;
        private ITaskResult _taskSelected;
        private string filter = "";
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        public ReactiveCommand<string, Unit> FilterCommand { get; }
        public ReactiveCommand<long, Unit> RefreshCommand { get; }
        public ReactiveCommand<TaskOption, Unit> ChangeStateTaskCommand { get; }
        public bool LoginProgresse
        {
            get => _loginProgresse;
            set
            {
                this.RaiseAndSetIfChanged(ref _loginProgresse, value);
            }
        }
        public IScreen HostScreen { get; }
        public ObservableCollection<ITaskResult> taskListResult { get; private set; }
        public ITaskResult taskSelected
        {
            get => _taskSelected;
            set
            {
                this.RaiseAndSetIfChanged(ref _taskSelected, value);
            }
        }

        public IStatisticResult StatisticResult
        {
            get => statisticResult;
            set
            {
                this.RaiseAndSetIfChanged(ref statisticResult, value);
            }
        }
    public DownloadStationViewModel(IScreen screen)
        {
            serviceProvider = Locator.Current.GetService<IServiceProvider>();
            HostScreen = screen;
            taskListResult = new ObservableCollection<ITaskResult>();
            internalList = new List<ITaskResult>();
            FilterCommand = ReactiveCommand.Create<string>(SetFilter);
            ChangeStateTaskCommand = ReactiveCommand.CreateFromTask<TaskOption>(SetChangeStateOfTask);
        }

        public async Task RefreshDownload()
        {
            if (taskListResult.Count == 0)
            {
                LoginProgresse = true;
            }
            var previousSelected = _taskSelected;
            internalList = (await serviceProvider.GetService<BaseSyno>().GetTask()).Tasks.ToList();
            StatisticResult = await serviceProvider.GetService<BaseSyno>().GetCurrentSpeed();
            taskListResult.Clear();
            taskListResult.AddRange(internalList.Where(w => w.Status.Contains(filter)));
            if (previousSelected != null)
            {
                taskSelected = previousSelected;
            }
            LoginProgresse = false;

        }
        private async Task SetChangeStateOfTask(TaskOption taskOption)
        {
            if (_taskSelected != null)
            {
                switch (taskOption)
                {
                    case TaskOption.Add:
                        break;
                    case TaskOption.Resume:
                        await serviceProvider.GetService<BaseSyno>().ResumeTask(_taskSelected.Id);
                        break;
                    case TaskOption.Pause:
                        await serviceProvider.GetService<BaseSyno>().PauseTask(_taskSelected.Id);
                        break;
                    case TaskOption.Edit:
                        break;
                    case TaskOption.Delete:
                        await serviceProvider.GetService<BaseSyno>().DeleteTask(_taskSelected.Id);
                        break;
                    default:
                        break;
                }
            }
            await RefreshDownload();

        }
        private void SetFilter(string filterValue)
        {
            filter = filterValue;
            taskListResult.Clear();
            taskListResult.AddRange(internalList.Where(w => w.Status.Contains(filter)));
        }
    }
}
