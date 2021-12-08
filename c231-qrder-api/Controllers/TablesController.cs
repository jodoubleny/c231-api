using Amazon.DynamoDBv2;
using Amazon.Runtime;
using AutoMapper;
using c231_qrder.Models;
using c231_qrder.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Web;

namespace c231_qrder.Controllers
{
    [ApiController]
    [Route("api/")]
    public class TablesController : ControllerBase
    {
        private readonly ITablesService tablesService;

        public TablesController(
            IAmazonDynamoDB dynamoDBClient,
            IMapper mapper
            )
        {
            tablesService = new TablesService(dynamoDBClient, mapper);
        }

        // GET: api/restaurant/5/tables
        [HttpGet("restaurant/{id}/tables")]
        public async Task<ActionResult<IEnumerable<TableDto>>> GetAllTables(string id)
        {
            IEnumerable<TableDto> resultTableDtos = new List<TableDto>();
            try
            {
                resultTableDtos = await tablesService.GetAllByRestaurantIdAsync(id);
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

        // POST: api/restaurant/5/table
        [HttpPost("restaurant/{id}/table")]
        public async Task<IActionResult> PostTable(string id, TableCreateDto tableCreateDto)
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
                await tablesService.AddAsync(id, tableCreateDto);
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

        // PUT: api/restaurant/5/table
        [HttpPut("restaurant/{id}/table")]
        public async Task<IActionResult> PutTable(string id, TableDto input)
        {
            // Returns errors
            if (input is null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await tablesService.SaveAsync(id, input);
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

        // DELETE: api/restaurant/5/table?tid=5
        [HttpDelete("restaurant/{id}/table")]
        public async Task<IActionResult> DeleteTable(string id, [FromQuery(Name = "tid")] string tableId)
        {
            try
            {
                await tablesService.RemoveAsync(id, tableId);
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
