using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Configuration;
using API.Home.Kelly.Models;
using API.Home.Kelly.Models.UPCDModels;
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
    }
}
