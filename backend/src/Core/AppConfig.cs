using Microsoft.Extensions.Configuration;

namespace Core
{
    public static class AppConfig
    {
        public static IConfigurationRoot Configuration
        {
            get
            {
                return new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: true)
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Development.json"), optional: true)
                .Build();
            }
        }

        public static string GetConnectionString() => Configuration.GetSection("ConnectionStrings").GetSection("PostgresqlDBConnectionString").Value;

        public static T GetSettings<T>(string key = null)
        {
            T obj = (T)Activator.CreateInstance(typeof(T));
            string keyValue = string.IsNullOrEmpty(key) ? obj.GetType().Name : key;
            Configuration.Bind(keyValue, obj);
            return obj;
        }

    }
}
