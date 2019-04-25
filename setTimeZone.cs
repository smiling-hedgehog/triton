using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace triton1
{
    static class setTimeZone
    {
        public static string initTimeZone(string req)
        {
            String url;

            long ts;
         
            string json = req;
            dynamic pl = JObject.Parse(json);
    
            ts = (long)pl["response"]["data"]["ts"];
            long nowOffset = System.Convert.ToInt64(http_sender.getCurrentTimeToUnixFormat()) - ts;


            long server_time = System.Convert.ToInt64(http_sender.getCurrentTimeToUnixFormat()) - nowOffset;
            long time_offset = System.Convert.ToInt64(http_sender.getCurrentTimeToUnixFormat()) - server_time;


            /*
              time_t server_time = std::chrono::system_clock::to_time_t(std::chrono::system_clock::now()) - params_.time_offset_;

    time_t time_offset = boost::posix_time::to_time_t(boost::posix_time::second_clock::local_time()) - server_time;
             */
            //server_time   = ts из прошлого requests   time_offset = разница серверного и текущего времени
            url = "https://api.icq.net/timezone/set?f=json&aimsid=" + http_sender.EscapeSymbol(Fetch.aimsid) + "&r=fdd2b42a-6842-4dbb-9176-bca66e31d76f" + "&TimeZoneOffset=" + time_offset;

           
           


            return url;
        }

    }
}
