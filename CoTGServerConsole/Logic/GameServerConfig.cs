using System.IO;
using Newtonsoft.Json.Linq;

namespace CoTG.CoTGServerConsole.Logic
{
    public class CoTGServerConfig
    {
        public string ClientLocation { get; private set; } = "C:\\CoTG\\League_Sandbox_Client";
        public bool AutoStartClient { get; private set; } = true;

        private CoTGServerConfig()
        {
        }

        public static CoTGServerConfig Default()
        {
            return new CoTGServerConfig();
        }

        public static CoTGServerConfig LoadFromJson(string json)
        {
            var result = new CoTGServerConfig();
            result.LoadConfig(json);
            return result;
        }

        public static CoTGServerConfig LoadFromFile(string path)
        {
            var result = new CoTGServerConfig();
            if (File.Exists(path))
            {
                result.LoadConfig(File.ReadAllText(path));
            }
            return result;
        }

        private void LoadConfig(string json)
        {
            var data = JObject.Parse(json);
            AutoStartClient = (bool)data.SelectToken("autoStartClient");
            ClientLocation = (string)data.SelectToken("clientLocation");
        }
    }
}
