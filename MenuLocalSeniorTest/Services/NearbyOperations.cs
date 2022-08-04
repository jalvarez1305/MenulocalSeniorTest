using System;
using System.Linq;
using System.Collections.Generic;
using Rest.Models;
using Rest.Services;
using System.Text.RegularExpressions;

namespace MenuLocalSeniorTest.Services
{
    public class NearbyOperations
    {
        int MINUTES_TO_DRINK = 20;
        int SECONDS_PER_MINUTE = 60;

        public List<NearbyResult> ListOfBars;
        public NearbyOperations(List<NearbyResult> ListOfBars)
        {
            this.ListOfBars = ListOfBars;
        }

        public void OrderList(Location startLocation)
        {
            foreach (var item in ListOfBars)
            {
                item.Distance = Math.Abs(Math.Abs(item.geometry.location.lat) - Math.Abs(startLocation.lat));
                item.Distance += Math.Abs(Math.Abs(item.geometry.location.lng) - Math.Abs(startLocation.lng));
            }
            ListOfBars.Sort(new NearbyResultComparer());
        }
        public void GetTimeToArrive(Location startLocation,DateTime StartTime)
        {
            Location lastLocation = startLocation;
            DateTime lastTime = StartTime;
            DirectionsService directionsService = new DirectionsService();

            foreach (var item in ListOfBars)
            {
                Route firstRoad = directionsService.GetRoutes(lastLocation, item.geometry.location, lastTime);
                int Minutes = firstRoad.legs.FirstOrDefault().duration.value / SECONDS_PER_MINUTE;
                item.ArrivedTime = lastTime.AddMinutes(Minutes);
                lastTime = lastTime.AddMinutes(Minutes).AddMinutes(MINUTES_TO_DRINK);
                lastLocation = item.geometry.location;

                /*Agrego las rutas*/
                foreach (var itemSteps in firstRoad.legs.FirstOrDefault().steps)
                {
                    item.Roads+= HtmlToPlainText(itemSteps.html_instructions)+"\n";
                }
            }
        }
        public void FilterReachableBarList(DateTime EndTime)
        {
            List<NearbyResult> copyOfListOfBars = new List<NearbyResult>();
            foreach (var item in ListOfBars)
            {
                if (item.ArrivedTime<=EndTime)
                {
                    copyOfListOfBars.Add(item);
                }
                else
                {
                    /*do notnihg*/
                }
            }
            ListOfBars = copyOfListOfBars;
        }
        private static string HtmlToPlainText(string html)
        {
            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            var text = html;
            //Decode html specific characters
            text = System.Net.WebUtility.HtmlDecode(text);
            //Remove tag whitespace/line breaks
            text = tagWhiteSpaceRegex.Replace(text, "><");
            //Replace <br /> with line breaks
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            //Strip formatting
            text = stripFormattingRegex.Replace(text, string.Empty);

            return text;
        }
    }
}
