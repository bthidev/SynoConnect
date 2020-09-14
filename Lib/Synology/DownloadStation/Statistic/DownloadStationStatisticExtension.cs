using Synology;
using Synology.DownloadStation;
using Synology.DownloadStation.Statistic;

namespace Synology
{
    /// <summary>
    /// Download station task extension.
    /// </summary>
	public static class DownloadStationStatisticExtension
	{
        /// <summary>
        /// Task the specified api.
        /// </summary>
        /// <returns>The task.</returns>
        /// <param name="api">API.</param>
		public static IStatisticRequest Statistic(this IDownloadStationApi api)
		{
			return api.Request<IStatisticRequest>();
		}
	}
}

