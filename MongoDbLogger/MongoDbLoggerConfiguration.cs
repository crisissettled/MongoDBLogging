namespace MongoDbLogging {
    internal sealed class MongoDbLoggerConfiguration {
        public string? Host { get; set; }
        public string? Port { get; set; }
        public string? Database { get; set; }
        public string? Collection { get; set; }
        public string ConnectionUri {
            get {
                return $"mongodb://{Host}:{Port}";
            }
        }      
    }
}
