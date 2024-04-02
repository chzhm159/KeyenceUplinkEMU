using KeyenceUplinkEMU.entity;
using log4net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace KeyenceUplinkEMU.utils
{
    internal class AppCfg
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AppCfg));
        private static readonly Lazy<AppCfg> _instance = new Lazy<AppCfg>(() => new AppCfg());
        private static IConfigurationRoot cfg;

        public static AppCfg Inst => _instance.Value;
        

        public static Dictionary<string, Dictionary<string, string>>? I18NRes { get; private set; }


        private AppCfg() {
            ConfigurationBuilder appCfgBuider = new ConfigurationBuilder();
            appCfgBuider.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "config"))
            .AddJsonFile("appsettings.json", true, reloadOnChange: true);
            cfg = appCfgBuider.Build();
        }
      
        
        
        /// <summary>
        /// 获取当前语言
        /// </summary>
        internal static string GetLanguage() {
            return "zh-CN";
        }

        /// <summary>
        /// 判断当前是否为中文环境(纯粹是为了减少大量不必要的重复代码)
        /// </summary>
        /// <returns></returns>
        internal static bool iszhCN() {
            string lang = GetLanguage();
            return lang.Equals("zh-CN", StringComparison.OrdinalIgnoreCase);
        }
        //public static string GetStringVaue(string key, string defaultValue = "none") {

        //    return cfg.GetValue<string>(key) ?? defaultValue;
        //}
        //public static int GetIntVaue(string key, int defaultValue = -1) {
        //    int v = cfg.GetValue<int>(key);
        //    if (v == 0) {
        //        return defaultValue;
        //    } else {
        //        return v;
        //    }
        //}
        internal static AckEntity AckMap =new AckEntity();
        JsonSerializerSettings jss = new JsonSerializerSettings();
        public static void LoadAck() {
            string tagPath = "config/ack.json";
            using (StreamReader file = File.OpenText(tagPath)) {
                JsonSerializer serializer = new JsonSerializer();
                AckMap = serializer.Deserialize(file, typeof(AckEntity)) as AckEntity;

            }
        }
        public static bool SaveAck() {
            try {
                string tagPath = "config/ack.json";
                using (StreamWriter writer = File.CreateText(tagPath)) {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Formatting = Formatting.Indented;
                    serializer.Serialize(writer, AckMap);
                }
                return true;
            } catch(Exception ex) {
                log.ErrorFormat("保存异常:{0},{1}",ex.Message,ex.StackTrace);
                return false;
            }
            
        }
        public static string AckMsg(string cmd) {
            string ack = "E1";
            //if (AckMap.ContainsKey(cmd)) {
            //    ack = AckMap[cmd];
            //}
            return ack;
        }

    }
}
