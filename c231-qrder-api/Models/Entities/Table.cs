using Amazon.DynamoDBv2.DataModel;

namespace c231_qrder.Models
{
    [DynamoDBTable("restaurants")]
    public class Table
    {
        [DynamoDBHashKey]
        public string RestaurantId { get; set; }
        [DynamoDBRangeKey("SortKey")]
        public string TableId { get; set; }
        public string TableName { get; set; }

        [DynamoDBIgnore]
        public static string tableSortKeyPrefix = "TABLE#";
    }
}
