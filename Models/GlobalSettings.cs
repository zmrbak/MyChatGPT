using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyChatGPT.Models
{
    public static class GlobalSettings
    {
        private static string openAiServer;
        private static string openAiKey;
        private static string userPassword;
        static string configFile = Path.Combine(FileSystem.AppDataDirectory, "configuration.json");

        static GlobalSettings()
        {
            LoadOpenAiSettings();
        }

        public static void LoadOpenAiSettings()
        {
            if (File.Exists(configFile))
            {
                var config = JsonSerializer.Deserialize<Configurations>(File.ReadAllText(configFile));
                openAiServer = config.OpenAiServer;
                openAiKey = config.OpenAiKey;
                userPassword = config.UserPassword;
            }
        }

        public static void SaveOpenAiSettings()
        {
            var json = JsonSerializer.Serialize(new Configurations
            {
                OpenAiServer = OpenAiServer,
                OpenAiKey = OpenAiKey,
                UserPassword = UserPassword,
            });
            File.WriteAllText(configFile, json);
        }



        public static string OpenAiServer { get { return openAiServer; } set => openAiServer = value; }
        public static string OpenAiKey { get { return openAiKey; } set => openAiKey = value; }
        public static string UserPassword { get { return userPassword; } set => userPassword = value; }
    }
}
