using Serilog;
using Serilog.Events;
using System.Security.Cryptography;
using System.Text;

namespace Roommates.Global.Helpers
{
    public static class Extensions
    {
        public static string ToSHA256(this string text)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(text);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public static void LogUserAction(this ILogger logger, string key, string message, Dictionary<string, object> properties, LogEventLevel logLevel = LogEventLevel.Information)
        {
            var enrichedProperties = new Dictionary<string, object>();
            enrichedProperties.Add("LogKey", key);
            if (properties != null)
            {
                foreach (var property in properties)
                {
                    enrichedProperties.Add(property.Key, property.Value);
                }
            }

            switch (logLevel)
            {
                case LogEventLevel.Information:
                    logger.Information(message, enrichedProperties);
                    break;
                case LogEventLevel.Warning:
                    logger.Warning(message, enrichedProperties);
                    break;
                case LogEventLevel.Error:
                    logger.Error(message, enrichedProperties);
                    break;
                default:
                    logger.Warning(message, enrichedProperties);
                    break;
            }
        }
    }
}
