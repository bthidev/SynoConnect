using Synology.Classes;
using Synology.FileStation.Upload.Parameters;
using Synology.Interfaces;
using System.Threading.Tasks;

namespace Synology.FileStation.Upload
{
    /// <summary>
    /// Upload request.
    /// </summary>
    public interface IUploadRequest : ISynologyRequest
    {
        /// <summary>
        /// Upload File async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="parameters">Parameters.</param>
        Task<ResultData> UploadAsync(UploadParameters parameters);
    }
}