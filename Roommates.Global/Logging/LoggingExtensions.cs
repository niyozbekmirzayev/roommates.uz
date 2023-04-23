using Serilog;
using Serilog.Events;

namespace Roommates.Global.Logging
{
    public static class LoggingExtensions
    {
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
