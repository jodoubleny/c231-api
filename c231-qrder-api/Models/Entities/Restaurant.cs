using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;

namespace c231_qrder.Models
{
    [DynamoDBTable("restaurants")]
    public class Restaurant
    {
        [DynamoDBHashKey]
        public string RestaurantId { get; set; }
        [DynamoDBRangeKey]
        public string SortKey { get; set; }
        [DynamoDBProperty]
        public string RestaurantName { get; set; }
        public bool IsRunning { get; set; }
        public List<MenuItem> Menus { get; set; }

        [DynamoDBIgnore]
        public static string restaurantSortKeyPrefix = "INFO#";
    }
}
