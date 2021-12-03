using Amazon.DynamoDBv2;
using Amazon.Runtime;
using AutoMapper;
using c231_qrder.Models;
using c231_qrder.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace c231_qrder.Controllers
{
    [ApiController]
    [Route("api/")]
    public class TablesController : ControllerBase
    {
        private readonly ITableService tableService;

        public TablesController(
            IAmazonDynamoDB dynamoDBClient,
            IMapper mapper
            )
        {
            tableService = new TableService(dynamoDBClient, mapper);
        }

        // GET: api/restaurant/5/tables
        [HttpGet("restauranttables/{id}/tables")]
        public async Task<ActionResult<IEnumerable<TableDto>>> GetAllTables(string id)
        {
            IEnumerable<TableDto> resultTableDtos = new List<TableDto>();
            try
            {
                resultTableDtos = await tableService.GetAllByRestaurantIdAsync(id);
            }
            catch (DataException)
            {
                return NotFound();
            }
            catch (AmazonServiceException)
            {
                return StatusCode(500, "A problem happend while processing the request from the remote server.");
            }
            catch
            {
                return StatusCode(500, "Something went wrong while processing the request in the server.");
            }

            return Ok(resultTableDtos);
        }

        // POST: api/restaurant/5
        [HttpPost("restaurant/{id}")]
        public async Task<ActionResult<Order>> PostTable(string id, TableCreateDto tableCreateDto)
        {
            // Returns errors
            if (tableCreateDto is null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await tableService.AddAsync(id, tableCreateDto);
            }
            catch (DuplicateNameException)
            {
                return StatusCode(500, "The name is duplicate.");
            }
            catch (DataException)
            {
                return NotFound();
            }
            catch (AmazonServiceException)
            {
                return StatusCode(500, "A problem happend while processing the request from the remote server.");
            }
            catch
            {
                return StatusCode(500, "Something went wrong while processing the request in the server.");
            }

            return NoContent();
        }
    }
}
