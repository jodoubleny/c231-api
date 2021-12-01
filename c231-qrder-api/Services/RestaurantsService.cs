using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using c231_qrder.Models;

namespace c231_qrder.Services
{
    public class RestaurantsService
    {
        private IAmazonDynamoDB _dynamoDBClient { get; set; }

        public RestaurantsService(IAmazonDynamoDB dynamoDBClient)
        {
            _dynamoDBClient = dynamoDBClient;
        }

        public async Task<List<Restaurant>> GetAllAsync()
        {
            DynamoDBContext context = new DynamoDBContext(_dynamoDBClient);
            List<Restaurant> restaurants = await context.ScanAsync<Restaurant>(
                new List<ScanCondition>()
                {
                    new ScanCondition("Id", ScanOperator.IsNotNull)
                }).GetRemainingAsync();
            return restaurants;
        }
    }
}
