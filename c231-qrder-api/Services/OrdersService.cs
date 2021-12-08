using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using AutoMapper;
using c231_qrder.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace c231_qrder.Services
{
    public class OrdersService : ServiceBase, IOrdersService
    {
        private readonly IAmazonDynamoDB dynamoDBClient;
        private readonly IDynamoDBContext context;
        private readonly IMapper mapper;

        public OrdersService(
            IAmazonDynamoDB dynamoDBClient,
            IMapper mapper
            )
        {
            this.dynamoDBClient = dynamoDBClient;
            context = new DynamoDBContext(this.dynamoDBClient);
            this.mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllByRestaurantIdAsync(string id)
        {
            if (!await IsRestaurantAvailable(id))
            {
                throw new DataException();
            }

            var config = new DynamoDBOperationConfig()
            {
                QueryFilter = new List<ScanCondition>()
                {
                    new ScanCondition("OrderId", ScanOperator.BeginsWith, Order.orderSortKeyPrefix)
                }
            };
            List<Order> allOrders = await context.QueryAsync<Order>(id, config).GetRemainingAsync();
            List<Order> notArchivedOrders = allOrders.Where(o => (o.IsArchived == false)).ToList();

            var allOrderDtos = new List<OrderDto>();
            allOrders.ForEach(o =>
            {
                var targetOrderDto = mapper.Map<OrderDto>(o);
                allOrderDtos.Add(targetOrderDto);
            });

            return allOrderDtos;
        }

        public async Task AddAsync(string id, OrderCreateDto orderCreateDto)
        {
            if (!await IsRestaurantAvailable(id))
            {
                throw new DataException();
            }

            // check tableId is in the Restaurant.Tables

            // Dto mapping: OrderCreateDto -> Order
            var newOrder = mapper.Map<Order>(orderCreateDto);

            // set properties
            string guid = base.GetGuidAsStr();
            newOrder.RestaurantId = id;
            newOrder.OrderId = Order.orderSortKeyPrefix + guid;
            newOrder.IsArchived = false;

            await context.SaveAsync(newOrder);
        }

        public async Task SaveAsync(string id, OrderDto orderDto)
        {
            if (!await IsOrderPresent(id, orderDto.OrderId))
            {
                throw new DataException();
            }

            // Dto mapping: OrderDto -> Order
            Order targetOrder = mapper.Map<Order>(orderDto);

            await context.SaveAsync(targetOrder);
        }

        public async Task ArchiveAsync(string id, string orderId)
        {
            if (!await IsOrderPresent(id, orderId))
            {
                throw new DataException();
            }

            Order targetOrder = await context.LoadAsync<Order>(id, orderId);
            targetOrder.IsArchived = true;

            await context.SaveAsync(targetOrder);
        }

        public async Task RemoveAsync(string id, string orderId)
        {
            if (!await IsOrderPresent(id, orderId))
            {
                throw new DataException();
            }

            await context.DeleteAsync<Restaurant>(id, orderId);
        }

        public async Task<bool> IsRestaurantAvailable(string id)
        {
            var restaurantService = new RestaurantsService(dynamoDBClient, mapper);
            var targetRestaurantDto = await restaurantService.GetByIdAsync(id);
            if (targetRestaurantDto is not null)
            {
                // IsRunning == true -> return true
                return (targetRestaurantDto.IsRunning);
            }
            return false;
        }

        public async Task<bool> IsOrderPresent(string id, string orderId)
        {
            // check there is the restaurant
            if (!await IsRestaurantAvailable(id))
            {
                return false;
            }

            // check there is the order in the restaurant
            //var orderDtos = new List<OrderDto>();
            //orderDtos.AddRange(await GetAllByRestaurantIdAsync(id));
            //TableDto targetorderDto = tableDtos.Find(td => td.TableId == tableId);

            Order targetOrder = await context.LoadAsync<Order>(id, orderId);

            return (targetOrder is not null);
        }

        public async Task<RestaurantDto?> GetRestaurantDtoById(string id)
        {
            var restaurantService = new RestaurantsService(dynamoDBClient, mapper);
            return (await restaurantService.GetByIdAsync(id));
        }
    }
}
