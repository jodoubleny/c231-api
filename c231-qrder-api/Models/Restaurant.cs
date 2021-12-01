using Amazon.DynamoDBv2.DataModel;

namespace c231_qrder.Models
{
    [DynamoDBTable("restaurants")]
    public class Restaurant
    {
        [DynamoDBHashKey]
        public string Id { get; set; }
        [DynamoDBProperty]
        public string Name { get; set; }
    }
}
