using System;
using System.Collections.Generic;
using System.Text;

namespace Synology.DownloadStation.Statistic.Results
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStatisticResult
    {
        /// <summary>
        /// 
        /// </summary>
        int SpeedDownload { get; }
        /// <summary>
        /// 
        /// </summary>
        int SpeedUpload { get; }
        /// <summary>
        /// 
        /// </summary>
        int EmuleSpeedDownload { get; }
        /// <summary>
        /// 
        /// </summary>
        int EmuleSpeedUpload { get; }
    }
}
