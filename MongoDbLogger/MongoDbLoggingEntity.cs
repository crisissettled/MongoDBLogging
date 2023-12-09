namespace MongoDbLogging {
    internal record LoggingEntity(string Environment, string ServiceName,string LogLevel, string CategoryName, string Message, DateTime LogDate);
    internal record ServiceInfo(string Environment, string ServiceName);
}
