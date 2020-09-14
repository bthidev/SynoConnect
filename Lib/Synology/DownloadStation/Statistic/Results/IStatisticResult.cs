using System;
using System.Collections.Generic;
using System.Text;

namespace Synology.DownloadStation.Statistic.Results
{
    public interface IStatisticResult
    {
        int SpeedDownload { get; }
        int SpeedUpload { get; }
        int EmuleSpeedDownload { get; }
        int EmuleSpeedUpload { get; }
    }
}
