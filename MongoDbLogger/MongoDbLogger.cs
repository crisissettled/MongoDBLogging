using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;



namespace MongoDbLogging {
    internal sealed class MongoDbLogger : ILogger {
        
        private readonly string _environment;
        private readonly string _serviceName;
        private readonly string _name;
        private readonly MongoDbLoggerConfiguration _config;

        private readonly IMongoClient _mongodbclient;

        public MongoDbLogger(string environment, string serviceName,string name, MongoDbLoggerConfiguration config) {

            (this._environment, this._serviceName, _name) = (environment, serviceName, name);
            _config = config;
            if (_mongodbclient == null) {               
                _mongodbclient = new MongoClient(_config.ConnectionUri);
            }
        }
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel) {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) {
            if (!IsEnabled(logLevel)) return;
            
            var loggingEntity = new LoggingEntity(_environment,_serviceName,logLevel.ToString(), _name, $"{formatter(state, exception)}", DateTime.Now);
            InserLoggingToMongoDb(loggingEntity);
        }

        private void InserLoggingToMongoDb(LoggingEntity loggingEntity) {           
            var db = _mongodbclient.GetDatabase(_config.Database);
            var collection = db.GetCollection<LoggingEntity>(_config.Collection);

            collection.InsertOne(loggingEntity);
        }
    }
}



 