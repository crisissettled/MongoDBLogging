using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;



namespace MongoDbLogging {
    internal sealed class MongoDbLogger : ILogger {

        private readonly string _environment;
        private readonly string _serviceName;
        private readonly string _categoryName;  
     
        private readonly IMongoCollection<LoggingEntity> _mongoCollection;

        public MongoDbLogger(IMongoCollection<LoggingEntity> mongoCollection, string environment, string serviceName, string categoryName) {
            (_mongoCollection, _environment, _serviceName, _categoryName) = (mongoCollection, environment, serviceName, categoryName);
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel) {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) {
            if (!IsEnabled(logLevel)) return;

            var loggingEntity = new LoggingEntity(_environment, _serviceName, logLevel.ToString(), _categoryName, $"{formatter(state, exception)}", DateTime.Now);
            InserLoggingToMongoDb(loggingEntity);
        }

        private void InserLoggingToMongoDb(LoggingEntity loggingEntity) {
            _mongoCollection.InsertOne(loggingEntity);
        }
    }
}



