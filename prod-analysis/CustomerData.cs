using Amazon.DynamoDBv2.DataModel;

[DynamoDBTable("prod-customer-data")]
public class CustomerData
{
    public CustomerData()
    {
        
    }
    
    [DynamoDBHashKey("eeid_p")]
    public string CustomPartitionKey { get; set; }

    [DynamoDBRangeKey("cuid")]
    [DynamoDBGlobalSecondaryIndexRangeKey("eeid_gsi")]
    public string SourceId { get; set; }

    [DynamoDBProperty("id_g")]
    public string Id { get; set; }
    
    [DynamoDBProperty("eeid_g")]
    [DynamoDBGlobalSecondaryIndexHashKey("eeid_gsi")]
    public string EventEditionId { get; set; }

    [DynamoDBProperty("email_g")]
    public string Email { get; set; }

    [DynamoDBProperty("type")]
    public string? Type { get; set; }
    
    [DynamoDBProperty("regId")]
    public string? RegistrationId { get; set; }

    [DynamoDBProperty("status")]
    public string Status { get; set; }
    
}