using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Configuration;
using API.Home.Kelly.Models;
using API.Home.Kelly.Models.UPCDModels;
using EntityFrameworkCore.Data.Home.Kelly;
using System.Net.Http;

namespace API.Home.Kelly.Controllers
{
    [Authorize]
    [ApiController]
    [Route("controllers")]
    public class InventoryController : Controller
    {
        //Application level API key for https://upcdatabase.org/api, see README.md for web.config info
        private static readonly string UPCToken = System.Configuration.ConfigurationManager.AppSettings["UPCDatabaseToken"];

        #region External Call Methods
        //This method looks up a given UPC in the https://upcdatabase.org/api database. 
        public static UPCDReturn? GetProduct(string barcode)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(@"https://api.upcdatabase.org/product/");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", UPCToken);
                Task<HttpResponseMessage> responseTask = client.GetAsync(barcode);
                responseTask.Wait();
                HttpResponseMessage response = responseTask.Result;
                Task<UPCDReturn?> readTask = response.Content.ReadFromJsonAsync<UPCDReturn>();
                readTask.Wait();
                UPCDReturn? result = readTask.Result;

                return result;
            }
        }
        #endregion

        #region Internal Call Methods
        public static bool WriteNewItem(Item item)
        {
            try
            {
                using (Data_Home_KellyContext _context = new Data_Home_KellyContext())
                {
                    _context.Items.Add(item);
                    _context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateItem(Item item, string location = null)
        {
            try
            {
                using(Data_Home_KellyContext _context =new Data_Home_KellyContext())
                {
                    Item? i = _context.Items
                        .Where(i => i.Id == item.Id)
                        .FirstOrDefault();

                    if(i is not null)
                    {
                        if (location is not null)
                        {
                            i.Location = location;
                        }
                        _context.SaveChanges();
                    }
                    else
                    {
                        return false;                        
                    }
                }

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Endpoints
        [HttpGet("item/query")]
        public ActionResult<Item> GetItem([FromQuery] string barcode)
        {
            UPCDReturn product = GetProduct(barcode);

            if (product.Success)
            {
                Item item = new Item()
                {
                    Barcode = product.Barcode,
                    Title = product.Title,
                    Alias = product.Alias,
                    Description = product.Description,
                    Brand = product.Brand,
                    Manufacturer = product.Manufacturer,
                    Mpn = ((int?)product.MPN),
                    Msrp = ((int?)product.MSRP),
                    Asin = product.ASIN,
                    Category = product.Category,
                };
                return Ok(item);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("item/add")]
        public ActionResult AddItem([FromBody] Item item)
        {
            bool success = WriteNewItem(item);

            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("item/update")]
        public ActionResult MoveItem([FromBody] Item item, string location)
        {
            bool success = UpdateItem(item, location);

            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion
    }
}
