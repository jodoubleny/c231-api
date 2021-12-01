using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using c231_qrder.Models;
using c231_qrder.Services;
using Microsoft.AspNetCore.Mvc;

namespace c231_qrder.Controllers
{
    [ApiController]
    [Route("api/")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IAmazonDynamoDB _dynamoDBClient;
        public RestaurantsController(
            IAmazonDynamoDB dynamoDBClient
            )
        {
            _dynamoDBClient = dynamoDBClient;
        }

        // [GET] api/restaurants
        [HttpGet]
        [Route("restaurants")]
        public async Task<List<Restaurant>> GetRestaurants()
        {
            RestaurantsService dynamoDBService = new RestaurantsService(_dynamoDBClient);
            List<Restaurant> restaurants = await dynamoDBService.GetAllAsync();
            return restaurants;
        }
    }
}
