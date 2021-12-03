using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using AutoMapper;
using c231_qrder.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Table = c231_qrder.Models.Table;

namespace c231_qrder.Services
{
    public class RestaurantService : ServiceBase, IRestaurantsService
    {
        private readonly IDynamoDBContext context;
        private readonly IMapper mapper;

        public RestaurantService(
            IAmazonDynamoDB dynamoDBClient,
            IMapper mapper
            )
        {
            context = new DynamoDBContext(dynamoDBClient);
            this.mapper = mapper;
        }

        public async Task<IEnumerable<RestaurantDto>> GetAllAsync()
        {
            List<Restaurant> allRestaurants = await context.ScanAsync<Restaurant>(
                new List<ScanCondition>()
                {
                    new ScanCondition("SortKey", ScanOperator.BeginsWith, restaurantSortKeyPrefix)
                }).GetRemainingAsync();

            var allRestaurantDtos = new List<RestaurantDto>();
            allRestaurants.ForEach(r =>
            {
                var targetRestaurantDto = mapper.Map<RestaurantDto>(r);
                allRestaurantDtos.Add(targetRestaurantDto);
            });

            return allRestaurantDtos;
        }

        public async Task<RestaurantDto> GetByIdAsync(string id)
        {
            Restaurant targetRestauranat = await context.LoadAsync<Restaurant>(id, restaurantSortKeyPrefix + id);
            if (targetRestauranat is null)
            {
                throw new DataException();
            }

            // Dto mapping: Restaurant -> RestaurantDto
            var resultRestaurantDto = mapper.Map<RestaurantDto>(targetRestauranat); 

            return resultRestaurantDto;
        }

        public async Task<RestaurantDto> AddAsync(RestaurantCreateDto restaurantCreateDto)
        {
            // Dto mapping: RestaurantCreateDto -> Restaurant
            var newRestaurant = mapper.Map<Restaurant>(restaurantCreateDto);

            // set properties
            string guid = base.GetGuidAsStr();
            newRestaurant.RestaurantId = guid;
            newRestaurant.SortKey = restaurantSortKeyPrefix + guid;
            newRestaurant.IsRunning = false;

            await context.SaveAsync(newRestaurant);

            // Dto mapping: Restaurant -> RestaurantDto
            var resultRestaurantDto = mapper.Map<RestaurantDto>(newRestaurant);

            return resultRestaurantDto;
        }

        public async Task SaveAsync(RestaurantDto restaurantDto)
        {
            if (!await IsAvailable(restaurantDto.RestaurantId))
            {
                throw new DataException();
            }

            // Dto mapping: RestaurantDto -> Restaurant
            Restaurant targetRestaurant = mapper.Map<Restaurant>(restaurantDto);

            await context.SaveAsync(targetRestaurant);
        }

        public async Task RemoveAsync(string id)
        {
            if (!await IsAvailable(id))
            {
                throw new DataException();
            }

            await context.DeleteAsync<Restaurant>(id, restaurantSortKeyPrefix + id);
        }

        public async Task<bool> IsAvailable(string id)
        {
            RestaurantDto restaurantDto = await GetByIdAsync(id);
            return (restaurantDto is not null);
        }

        //public async Task<IEnumerable<TableDto>> GetTablesByRestaurantIdAsync(string id)
        //{
        //    RestaurantDto targetRestaurantDto = await GetByIdAsync(id);
        //    if (targetRestaurantDto is null)
        //    {
        //        throw new DataException();
        //    }

        //    List<Table> resultTables = targetRestaurantDto.Tables;

        //    var resultTableDtos = new List<TableDto>();
        //    resultTables.ForEach(t =>
        //    {
        //        var targetTableDto = mapper.Map<TableDto>(t);
        //        resultTableDtos.Add(targetTableDto);
        //    });

        //    return resultTableDtos;
        //}

        //public async Task AddTableAsync(string id, TableCreateDto tableCreateDto)
        //{
        //    RestaurantDto targetRestaurantDto = await GetByIdAsync(id);

        //    // Dto mapping: RestaurantDto -> Restaurant
        //    var targetRestaurant = mapper.Map<Restaurant>(targetRestaurantDto);

        //    // Dto mapping: TableCreateDto -> Table
        //    var newTable = mapper.Map<Table>(tableCreateDto);

        //    // set properties
        //    string guid = base.GetGuidAsStr();
        //    newTable.TableId = guid;

        //    // check dupulicate table names
        //    Table duplicateTable = targetRestaurant.Tables.Find(t => t.TableName.Contains(newTable.TableName));
        //    if (duplicateTable is not null)
        //    {
        //        throw new DuplicateNameException();
        //    }

        //    targetRestaurant.Tables.Add(newTable);

        //    await context.SaveAsync(targetRestaurant);
        //}

        //public async Task EditTableAsync(string id, TableDto tableDto)
        //{

        //}
    }
}
