using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDirectory.Services
{
    /// <summary>
    /// This class will help me locate a business in the map by just knowing it's address
    /// </summary>
    public class GeoCoordService
    {
        private IConfigurationRoot _config;
        private ILogger<GeoCoordService> _logger;

        public GeoCoordService(ILogger<GeoCoordService> logger, IConfigurationRoot config)
        {
            _logger = logger;
            _config = config;
        }
        /// <summary>
        /// This method will help me obtain latitude and longitude using only an address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task<GeoCoordResult> GetCoordByAddress(string address)
        {
            var result = new GeoCoordResult
            {
                Success = false,
                Message = "Fallo en la obtencion de datos"
                
            };
            var apiKey = _config["Keys:BingKey"];
            var encodedName = WebUtility.UrlEncode(address);
            var url = $"http://dev.virtualearth.net/REST/V1/Locations?q={encodedName}&key={apiKey}";
            var client = new HttpClient();
            var json = await client.GetStringAsync(url);

            // Reads the result
            // This might need changes if the Bing Api changes
            var results = JObject.Parse(json);
            var resources = results["resourceSets"][0]["resources"];
            if (!resources.HasValues)
            {
                result.Message = $"No se pudo encontrar '{address}' como una localidad";
            }
            else
            {
                var confidence = (string)resources[0]["confidence"];
                if (confidence != "High")
                {
                    result.Message = $"No se pudo encontrar una locacion confidente de '{address}";
                }
                else
                {
                    var coords = resources[0]["geocodePoints"][0]["coordinates"];
                    result.Latitude = (double)coords[0];
                    result.Longitude = (double)coords[1];
                    result.Success = true;
                    result.Message = "Exito";
                }
            }
            return result;

        }
    }
}
