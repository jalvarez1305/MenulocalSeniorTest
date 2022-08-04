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
    public class DistanceService
    {
        int SECONDS_PER_MINUTE = 60;
        public int GetMinutes(Location Origen,Location Destination,DateTime DepartureTime)
        {
            /*Set URL*/
            string searchURL = Configs.DistanceURL.Replace("[LATITUD_ORIGEN]", Origen.lat.ToString("N8"));
            searchURL = searchURL.Replace("[LONGITUD_ORIGEN]", Origen.lng.ToString("N8"));

            searchURL = searchURL.Replace("[LATITUD_DESTINO]", Destination.lat.ToString("N8"));
            searchURL = searchURL.Replace("[LONGITUD_DESTINO]", Destination.lng.ToString("N8"));

            searchURL = searchURL.Replace("[START_TIME]", ConvertToUnixTimestamp(DepartureTime).ToString());

            List<NearbyResult> barList = new List<NearbyResult>();
            int Minutes = 0;
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
                    var dataResponse = JsonConvert.DeserializeObject<DistanceRoot>(response);
                    try
                    {
                        var element = dataResponse.rows.FirstOrDefault().elements.FirstOrDefault();
                        Minutes = element.duration.value / SECONDS_PER_MINUTE;
                    }
                    catch (Exception) { /*do nothing*/}
                }
            }
            catch (Exception)
            {
                /*do nothing*/
            }
            return Minutes;
        }
        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }
}
