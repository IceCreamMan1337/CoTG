using System.IO;
using Newtonsoft.Json.Linq;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServerConsole.Logic
{
    public class ChildrenOfTheGraveServerConfig
    {
        public string ClientLocation { get; private set; } = "C:\\ChildrenOfTheGrave\\League_Sandbox_Client";
        public bool AutoStartClient { get; private set; } = true;

        private ChildrenOfTheGraveServerConfig()
        {
        }

        public static ChildrenOfTheGraveServerConfig Default()
        {
            return new ChildrenOfTheGraveServerConfig();
        }

        public static ChildrenOfTheGraveServerConfig LoadFromJson(string json)
        {
            var result = new ChildrenOfTheGraveServerConfig();
            result.LoadConfig(json);
            return result;
        }

        public static ChildrenOfTheGraveServerConfig LoadFromFile(string path)
        {
            var result = new ChildrenOfTheGraveServerConfig();
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
