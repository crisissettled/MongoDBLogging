using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Reflection;

namespace MongoDbLogging {
    public static class MongoDbLoggerExtension {      
        public static ILoggingBuilder AddMongoDbLogger(this ILoggingBuilder builder, IConfiguration config, Assembly assembly) {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?? "NotSet";
            var serviceName = assembly.GetName().Name?? "Unknown";

            var configObj = config.Get<MongoDbLoggerConfiguration>();
            if (configObj == null) {  throw new ArgumentNullException(nameof(config)); }
            var mongodbclient = new MongoClient(configObj.ConnectionUri);

            var db = mongodbclient.GetDatabase(configObj.Database);
            var collection = db.GetCollection<LoggingEntity>(configObj.Collection);

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider>(new MongoDbLoggerProvider(collection, envName, serviceName)));                  

            return builder;
        }
    }
}
