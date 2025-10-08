using System;
using System.Collections.Generic;
using System.IO;

using log4net;
using log4net.Config;
using System.Diagnostics;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace CoTGEnumNetwork.Packets.Handlers
{

    public static class LoggerProvider2
    {
        /// <summary>
        /// Provider instance which configures log4net to prepare for getting a logger instance.
        /// </summary>
        static LoggerProvider2()
        {
            string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App.config");
            FileInfo finfo = new(logFilePath);
            var rep = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(rep, finfo);
        }

        /// <summary>
        /// Gets a logger instance specific to the caller.
        /// </summary>
        /// <returns>Logger designated to the specific caller.</returns>
        public static ILog GetLogger()
        {
            var caller = new StackTrace().GetFrame(1).GetMethod().DeclaringType;
            return LogManager.GetLogger(caller);
        }
    }



    // the global generic network handler between the bridge and the server
    public class NetworkHandler<MessageType> where MessageType : MirrorImage.BasePacket
    {

        // Logger
        private static ILog Logger = LoggerProvider2.GetLogger();


        public delegate bool MessageHandler(int userId, MirrorImage.BasePacket msg);
        private readonly Dictionary<Type, List<MessageHandler>> _handlers = new();



        // server & scripts will register to events, this allows scripts more than the current state
        public void Register<T>(Func<int, T, bool> onPacket) where T : MessageType
        {
            if (onPacket == null) return;
            if (!_handlers.ContainsKey(typeof(T)))
            {
                _handlers.Add(typeof(T), new List<MessageHandler>());
            }
            _handlers[typeof(T)].Add((clientId, packet) =>
            {
                if (packet is T typedPacket)
                {
                    return onPacket(clientId, typedPacket);
                }
                return false;
            });
        }
        // every message (bridge->server or server->bridge) pass should pass here


        //just for see in config if is true or false for log 
        private static bool Getlogstatus()
        {
            //check this one is broken 
            string filePath = Path.Join(".", "Settings", "GameInfo.json"); // Remplacez par le chemin de votre fichier JSON

            if (!File.Exists(filePath))
            {
                bool logstatus = false;
                return logstatus;
            }
            else
            {
                string jsonContent = File.ReadAllText(filePath);

                JObject jsonObject = JObject.Parse(jsonContent);

                //  Console.WriteLine("FICHIER TROUVER ");
                bool logstatus = jsonObject["gameInfo"]["ENABLE_LOG_PKT"].Value<bool>();

                //   Console.WriteLine("FICHIER BOOLEAN =  " + logstatus);
                return logstatus;
            }

        }


        public bool OnMessage<T>(int userId, T packet) where T : MessageType
        {

            if (packet.GetType().Name != "C2S_World_SendCamera_Server" && packet.GetType().Name != "C2S_OnReplication_Acc")
            {
                //
                if (Getlogstatus())
                {
                    //Console.WriteLine($"Receiving {packet.GetType().Name} {Newtonsoft.Json.JsonConvert.SerializeObject(packet, Newtonsoft.Json.Formatting.Indented)} from {userId}");

                    Logger.Info($"Receiving {packet.GetType().Name} {Newtonsoft.Json.JsonConvert.SerializeObject(packet, Newtonsoft.Json.Formatting.Indented)} from {userId}");
                    string emptystring = "RECEIVE ";
                    foreach (byte b in packet.GetBytes())
                    {
                        //  //  Console.Write($"{b:X2} "); // Display each byte in hexadecimal with two digits.
                        emptystring = emptystring + $"{b:X2}" + "";
                    }
                    Logger.Debug(emptystring);
                }

            }


            bool success = true;

            if (packet is MirrorImage.IUnusedPacket && Getlogstatus())
            {
                Console.WriteLine($"Received UnusedPacket {packet.GetType()} from {userId}");
            }

            if (_handlers.TryGetValue(packet.GetType(), out var delegates))
            {
                foreach (MessageHandler handler in delegates)
                {
                    success = handler(userId, packet) && success;
                }
            }
            return success;
        }
    }
}
