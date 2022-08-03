using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest
{
    class Config
    {
        public static string KEY = "AIzaSyB6uYfdnU1MA1F44l2qDUEptgXzyqiDelk";
        public static string OPEN_NOW = "true";
        public static string RADIUS = "10000";
        public static string TYPE = "bar";
        public static string URL = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?key={KEY}&location=[LATITUD]%2C[LONGITUD]&opennow={OPEN_NOW}&radius={RADIUS}&type={TYPE}";
    }
}
