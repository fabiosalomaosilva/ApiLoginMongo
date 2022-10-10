namespace ApiLoginMongo.Data
{
    public class StoreDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string ApiLoginCollectionName { get; set; } = null!;
    }
}
