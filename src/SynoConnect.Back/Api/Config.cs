using Newtonsoft.Json;
using SynoConnect.Back.Models;
using System.IO;

namespace SynoConnect.Back.Api
{
    public class ConfigService
    {
        public SettingsModels Settings { get; set; }
        public void GetConfig()
        {
            if (File.Exists("./Config.json"))
            {
                using (StreamReader r = new StreamReader("./Config.json"))
                {
                    string json = r.ReadToEnd();
                    Settings = JsonConvert.DeserializeObject<SettingsModels>(json);
                }
            }
            else
            {
                Settings = new SettingsModels();
            }
        }

        public void SetConfig()
        {
            using (StreamWriter file = File.CreateText("./Config.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                //serialize object directly into file stream
                serializer.Serialize(file, Settings);
            }
        }
    }
}
