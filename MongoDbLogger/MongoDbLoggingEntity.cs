namespace MongoDbLogging {
    public record LoggingEntity(string Environment, string ServiceName,string LogLevel, string CategoryName, string Message, DateTime LogDate);
}
