using Rest.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Rest.Services
{
    public class NearbySearch
    {
        public List<NearbyResult> GetBars(Location location)
        {
            /*Set URL*/
            string searchURL = Config.URL.Replace("[LATITUD]", location.lat.ToString());
            searchURL = searchURL.Replace("[LONGITUD]", location.lng.ToString());

            Config config = new Config();
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.BaseAddress = Configs.ConfigSelectEndPoint;
                    var url = Configs.ConfigSelectEndPoint;
                    webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    string data = JsonConvert.SerializeObject(filters);
                    var response = webClient.UploadString(url, data);
                    var dataResponse = JsonConvert.DeserializeObject<RootConfig>(response);
                    config = dataResponse.data.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                /*do nothing*/
            }
            return config;
        }
    }
}
