
using System.Collections.Generic;

namespace SynoConnect.Back.Models
{
    class HackJsonIFileresult
    {
        public List<FileInfo> files { get; set; }
        public int total { get; set; }
        public int offset { get; set; }
    }
    public class FileInfo
    {
        public bool isdir { get; set; }
        public object children { get; set; }
        public string path { get; set; }
        public string name { get; set; }
        public object additional { get; set; }
    }

}
