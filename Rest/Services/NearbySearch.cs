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
            string searchURL = Configs.NearbyURL.Replace("[LATITUD]", location.lat.ToString());
            searchURL = searchURL.Replace("[LONGITUD]", location.lng.ToString());
            List<NearbyResult> barList = new List<NearbyResult>();
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.BaseAddress = searchURL;
                    var url = searchURL;
                    webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    string data = "";
                    var response = webClient.UploadString(url, data);
                    var dataResponse = JsonConvert.DeserializeObject<NearbyRoot>(response);
                    barList = dataResponse.results;
                }
            }
            catch (Exception)
            {
                /*do nothing*/
            }
            return barList;
        }
    }
}
