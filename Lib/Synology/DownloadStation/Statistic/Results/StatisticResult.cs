using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Synology.DownloadStation.Statistic.Results
{
    internal class StatisticResult : IStatisticResult
    {
        [JsonProperty("speed_download")]
        public int SpeedDownload { get; set; }
        [JsonProperty("speed_upload")]
        public int SpeedUpload { get; set; }
        [JsonProperty("emule_speed_download")]
        public int EmuleSpeedDownload { get; set; }
        [JsonProperty("emule_speed_upload")]
        public int EmuleSpeedUpload { get; set; }
    }
}
