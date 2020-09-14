using Synology.Parameters;
using Synology.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Synology.DownloadStation.Task.Parameters
{
    public class TaskCreateParametersPost : PostParameters
    {
        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>The URI.</value>
        public string Filename { get; set; }
        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        /// <value>The file.</value>
		public byte[] File { get; set; }
        /// <summary>
        /// Gets or sets the destination.
        /// </summary>
        /// <value>The destination.</value>
        public string Destination { get; set; }

        /// <summary>
        /// Parameters this instance.
        /// </summary>
        /// <returns>The parameters.</returns>
        public override FormParameter[] Parameters()
        {
            var fileCondition = new Random().Next();

            var parameters = new List<FormParameter>
            {
                new FormParameter("destination", Destination),
                // From the documentation, the file must always be the last parameter
                new FileFormDataParameter("file", Filename, File)
            };

            return parameters.ToArray();
        }
    }
}
