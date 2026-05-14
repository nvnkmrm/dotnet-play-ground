using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace ProdAnalysis;

public class Program
{
    public static async Task Main(string[] args)
    {
        // Initialize DynamoDB client and context
        var client = new AmazonDynamoDBClient();
        var context = new DynamoDBContext(client);
        
        // Example usage: Query GSI with partition key and multiple sort keys
        var partitionKey = "eve-6d979dc8-444d-4101-b824-4afc2f7bc1df"; // Replace with your EventEditionId (eeid_g)
        var sortKeys = new List<string> 
        { 
            "1524155082159817-WCO",
            "1524285282960465-EAS",
            "1510427651444926-ZDY",
            "1530348111598252-A21"
        };

        var customerData = await QueryGsiWithMultipleSortKeys(context, partitionKey, sortKeys);

        PrintSourceIdAndType(customerData);
        
        Console.WriteLine("\n" + new string('=', 50));
        
    }

    /// <summary>
    /// Queries the eeid_gsi GSI using partition key and multiple sort keys
    /// </summary>
    /// <param name="context">DynamoDB context</param>
    /// <param name="partitionKey">The EventEditionId (partition key for GSI)</param>
    /// <param name="sortKeys">List of SourceId values (sort keys for GSI)</param>
    public static async Task<List<CustomerData>> QueryGsiWithMultipleSortKeys(
        DynamoDBContext context, 
        string partitionKey, 
        List<string> sortKeys)
    {
        Console.WriteLine($"Querying GSI 'eeid_gsi' with partition key: {partitionKey}");
        Console.WriteLine($"Sort keys: {string.Join(", ", sortKeys)}");
        Console.WriteLine();

        var allResults = new List<CustomerData>();

        foreach (var sortKey in sortKeys)
        {
            try
            {
                Console.WriteLine($"Querying with sort key: {sortKey}");
                
                // Query the GSI using partition key and sort key
                var queryConfig = new DynamoDBOperationConfig
                {
                    IndexName = "eeid_gsi"
                };
                
                var results = await context.QueryAsync<CustomerData>(
                    partitionKey, 
                    QueryOperator.Equal, 
                    new List<object> { sortKey },
                    queryConfig
                ).GetRemainingAsync();

                Console.WriteLine($"Found {results.Count} records for sort key: {sortKey}");
                
                foreach (var item in results)
                {
                    Console.WriteLine($"  - ID: {item.Id}, SourceId: {item.SourceId}, Email: {item.Email}, Status: {item.Status}");
                }
                
                allResults.AddRange(results);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error querying sort key {sortKey}: {ex.Message}");
                Console.WriteLine();
            }
        }

        
        Console.WriteLine($"Total records found across all sort keys: {allResults.Count}");
        Console.WriteLine("\n=== Summary ===");
        return allResults;
        
    }

    /// <summary>
    /// Prints SourceId and Type for each customer in the list
    /// </summary>
    /// <param name="customers">List of CustomerData objects</param>
    public static void PrintSourceIdAndType(List<CustomerData> customers)
    {
        Console.WriteLine("=== Customer SourceId and Type ===");
        foreach (var customer in customers)
        {
            Console.WriteLine($"SourceId: {customer.SourceId}, Type: {customer.Type}, Status: {customer.Status}");
        }
        Console.WriteLine($"Total customers processed: {customers.Count}");
    }
}

