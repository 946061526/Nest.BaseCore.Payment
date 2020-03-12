using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Nest.BaseCore.Payment
{

    /// <summary>
    /// 读取配置文件帮助类
    /// </summary>
    public class AppSettingsHelper
    {
        public static IConfiguration Configuration { get; set; }
        static AppSettingsHelper()
        {
            //var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");

            //ReloadOnChange = true 当appsettings.json被修改时重新加载            
            Configuration = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
            .Build();
        }

        /// <summary>
        /// 根据键获取配置对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetAppSettings<T>(string key) where T : class, new()
        {
            var appconfig = new ServiceCollection()
             .AddOptions()
             .Configure<T>(Configuration.GetSection(key))
             .BuildServiceProvider()
             .GetService<IOptions<T>>()
             .Value;
            return appconfig;
        }

        public static string GetKeyValue(string key)
        {
            return Configuration.GetSection(key).Value;
        }
    }
}

