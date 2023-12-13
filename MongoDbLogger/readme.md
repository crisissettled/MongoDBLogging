## How to use

### Add code in Program.cs

```
  builder.Logging.ClearProviders()
      .AddMongoDbLogger(builder.Configuration.GetSection("MongoDbLogging"),Assembly.GetExecutingAssembly());

  (replace "MongoDbLogging" with your appsetting node name )
```

### In appsettings.json, add MongoDB settings where your logs writen to

```
"MongoDbLogging": {
   "Host": "localhost",
   "Port": 27017,
   "Database": "test_api",
   "Collection": "logging"
 }
```

### Entity used to write data to mongodb which is public record, can be used to do Get query from mongodb

```
   public record LoggingEntity(string Environment, string ServiceName,string LogLevel, string CategoryName, string Message, DateTime LogDate);
```

### Data in mongodb

```
{
  "_id": {
    "$oid": "6578f157734b10fc601b8f55"
  },
  "Environment": "Development",
  "ServiceName": "test_api",
  "LogLevel": "Information",
  "CategoryName": "Microsoft.Hosting.Lifetime",
  "Message": "Now listening on: http://localhost:5194",
  "LogDate": {
    "$date": "2023-12-12T23:48:15.153Z"
  }
}
```
