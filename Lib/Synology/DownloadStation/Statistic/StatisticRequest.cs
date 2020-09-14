using Synology.Classes;
using Synology.DownloadStation.Task.Parameters;
using Synology.DownloadStation.Task.Results;
using Synology.Utilities;
using System.Collections.Generic;
using Synology.Attributes;
using Synology.Parameters;
using System.Threading.Tasks;
using System;
using System.Linq;
using Synology.DownloadStation.Statistic.Results;

namespace Synology.DownloadStation.Statistic
{
    [Request("Statistic")]
    internal class StatisticRequest : DownloadStationRequest, IStatisticRequest
    {
        public StatisticRequest(IDownloadStationApi api) : base(api)
        {
        }

        [RequestMethod("getinfo")]
        public async Task<ResultData<IStatisticResult>> InfoAsync()
        {
            var res = await this.GetDataAsync<StatisticResult>(new SynologyRequestParameters(this)
            {
            });

            return new ResultData<IStatisticResult>
            {
                Data = res.Data,
                Error = res.Error,
                Success = res.Success
            };
        }
    }
}

