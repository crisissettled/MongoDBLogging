using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Concurrent;

namespace MongoDbLogging {
    internal sealed class MongoDbLoggerProvider : ILoggerProvider {
        private readonly ConcurrentDictionary<string, MongoDbLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);
        private readonly string _envName;
        private readonly string _serviceName;

        private readonly IMongoCollection<LoggingEntity> _mongoCollection;

        public MongoDbLoggerProvider(IMongoCollection<LoggingEntity> mongoCollection, string EnvName, string ServiceName) {
           _mongoCollection = mongoCollection;
           _envName = EnvName;
           _serviceName = ServiceName;
        }

        public ILogger CreateLogger(string categoryName) {
            return _loggers.GetOrAdd(categoryName, (categoryName) => new MongoDbLogger(_mongoCollection,_envName, _serviceName, categoryName));
        }

        public void Dispose() {
            _loggers.Clear();     
        }        
    }
}
