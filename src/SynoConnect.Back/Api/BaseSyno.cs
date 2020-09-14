using Microsoft.Extensions.DependencyInjection;
using SynoConnect.Back.Models;
using Synology;
using Synology.Classes;
using Synology.DownloadStation.Info.Results;
using Synology.DownloadStation.Task.Parameters;
using Synology.DownloadStation.Task.Results;
using Synology.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SynoConnect.Back.Api
{
    public class BaseSyno
    {
        readonly IServiceProvider _serviceProvider;
        readonly IServiceScope _scope;
        ISynologyConnectionSettings settingsSave;
        public BaseSyno(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _scope = _serviceProvider.CreateScope();
        }
        public async Task<bool> DeleteTask(string ID)
        {
            ISynologyConnection syno = _scope.ServiceProvider.GetService<ISynologyConnection>();
            ResultData<IEnumerable<ITaskMinimalResult>> result = await syno.DownloadStation().Task().DeleteAsync(new TaskDeleteParameters() { Ids = new[] { ID } });
            return result.Success;
        }
        public async Task<bool> PauseTask(string ID)
        {
            ISynologyConnection syno = _scope.ServiceProvider.GetService<ISynologyConnection>();
            ResultData<IEnumerable<ITaskMinimalResult>> result = await syno.DownloadStation().Task().PauseAsync(new[] { ID });
            return result.Success;
        }
        public async Task<bool> ResumeTask(string ID)
        {
            ISynologyConnection syno = _scope.ServiceProvider.GetService<ISynologyConnection>();
            ResultData<IEnumerable<ITaskMinimalResult>> result = await syno.DownloadStation().Task().ResumeAsync(new[] { ID });
            return result.Success;
        }
        public async void GetCurrentSpeed(){
            ISynologyConnection syno = _scope.ServiceProvider.GetService<ISynologyConnection>();
            var result = await syno.DownloadStation().Statistic().InfoAsync();
            if ( result.Success){
                Console.WriteLine($"speeddownload : {result.Data.SpeedDownload} uploadspeed : { result.Data.SpeedUpload }");
            }
        }
        public async Task<bool> AddTask(NewDownloadModels newDownloadModels)
        {

            ISynologyConnection syno = _scope.ServiceProvider.GetService<ISynologyConnection>();

            if (!string.IsNullOrEmpty(newDownloadModels.Uri))
            {
                TaskCreateParameters request = new TaskCreateParameters()
                {
                    Destination = newDownloadModels.Destination,
                    Password = newDownloadModels.Password,
                    UnzipPassword = newDownloadModels.UnzipPassword,
                    Username = newDownloadModels.Username,
                    File = newDownloadModels.Uri,
                };
                ResultData result = await syno.DownloadStation().Task().CreateAsync(request);
                return result.Success;
            }
            else
            {
                var result = await syno.DownloadStation().Task().CreatePostAsync(new TaskCreateParametersPost
                {
                    Filename = newDownloadModels.File,
                    Destination = newDownloadModels.Destination,
                    File = new byte[0],
                });
            }
            return false;

        }
        public async Task<IConfigResult> GetSettings()
        {
            ISynologyConnection syno = _scope.ServiceProvider.GetService<ISynologyConnection>();
            ResultData<IConfigResult> result = (await syno.DownloadStation().Info().ConfigAsync());
            if (result.Success)
            {
                return result.Data;
            }
            else
            {
                return null;
            }
        }
        public async Task<bool> SetSettings(IConfigResult config)
        {
            ISynologyConnection syno = _scope.ServiceProvider.GetService<ISynologyConnection>();
            ResultData result = (await syno.DownloadStation().Info().SetConfigAsync(new Synology.DownloadStation.Info.Parameters.SetConfigParameters
            {
                KbpsTorrentMaxDownload = config.TorrentMaxDownload,
                KbpsTorrentMaxUpload = config.TorrentMaxUpload,
                KbpsEmuleMaxDownload = config.EmuleMaxDownload,
                KbpsEmuleMaxUpload = config.EmuleMaxUpload,
                KbpsNzbMaxDownload = config.NzbMaxDownload,
                KbpsHttpMaxDownload = config.HttpMaxDownload,
                KbpsFtpMaxDownload = config.FtpMaxDownload,
                EmuleEnabled = config.EmuleEnabled,
                UnzipEnabled = config.UnzipServiceEnabled,
                DefaultDestination = config.DefaultDestination,
                EmuleDefaultDestination = config.EmuleDefaultDestination
            }
            ));
            return result.Success;
        }
        public async Task<ITaskListResult> GetTask()
        {
            ISynologyConnection syno = _scope.ServiceProvider.GetService<ISynologyConnection>();
            ResultData<ITaskListResult> dsResTasks = await syno.DownloadStation().Task().ListAsync(new TaskListParameters
            {
                Additional = TaskDetailsType.Detail | TaskDetailsType.Transfer | TaskDetailsType.File | TaskDetailsType.Tracker | TaskDetailsType.Peer
            });
            return dsResTasks.Data;

        }
        public async Task<bool> ConnectUser(string url, string username, string password)
        {
            try
            {
                Regex r = new Regex(@"^(?<proto>\w+)://[^/]+?(?<port>:\d+)?/", RegexOptions.None, TimeSpan.FromMilliseconds(150));
                Match m = r.Match(url);
                if (m.Success)
                {
                    Console.WriteLine(m.Result("${proto}${port}"));
                    int port = 80;
                    if (string.IsNullOrEmpty(m.Groups["port"].Value))
                    {
                        port = (m.Groups["proto"].Value == "https") ? 443 : 80;
                    }
                    else
                    {
                        port = int.Parse(m.Groups["port"].Value.Replace(":", ""));
                    }

                    Uri uri = new Uri(url);

                    settingsSave = _scope.ServiceProvider.GetService<ISynologyConnectionSettings>();

                    settingsSave.BaseHost = uri.Host;
                    settingsSave.Ssl = (m.Groups["proto"].Value == "https");
                    settingsSave.Port = port;
                    settingsSave.SslPort = port;
                    settingsSave.Username = username;
                    settingsSave.Password = password;
                    settingsSave.SessionName = "SynoConnect";

                    ISynologyConnection syno = _scope.ServiceProvider.GetService<ISynologyConnection>();
                    ResultData<Synology.Api.Auth.Results.IAuthResult> resLogin = await syno.Api().Auth().LoginAsync();
                    if (resLogin.Error == null || (resLogin.Error != null && resLogin.Error.Code == 403))
                    {
                        return true;
                    }
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

    }
}
