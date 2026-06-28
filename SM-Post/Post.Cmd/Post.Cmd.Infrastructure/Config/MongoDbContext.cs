namespace Post.Cmd.Infrastructure.Config;

public class MongoDbContext
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string EventStoreCollectionName { get; set; }
    
}