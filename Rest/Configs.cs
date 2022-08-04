using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest
{
    class Configs
    {
        public static string KEY = "AIzaSyB6uYfdnU1MA1F44l2qDUEptgXzyqiDelk";
        public static string OPEN_NOW = "true";
        public static string RADIUS = "10000";
        public static string TYPE = "bar";
        public static string NearbyURL = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?key={KEY}&location=[LATITUD]%2C[LONGITUD]&opennow={OPEN_NOW}&radius={RADIUS}&type={TYPE}";

        public static string DistanceURL = $"https://maps.googleapis.com/maps/api/distancematrix/json?key={KEY}&destinations=[LATITUD_DESTINO]%2C[LONGITUD_DESTINO]&origins=[LATITUD_ORIGEN]%2C[LONGITUD_ORIGEN]&departure_time=[START_TIME]";
    }
}
