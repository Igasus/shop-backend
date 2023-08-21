namespace Shop.Infrastructure.Options;

public class AzureOptions
{
    public const string Section = "Azure";
    
    public ServiceBusOptions ServiceBus { get; set; }
    
    public class ServiceBusOptions
    {
        public string ConnectionString { get; set; }
        public string QueueName { get; set; }
    }
}
