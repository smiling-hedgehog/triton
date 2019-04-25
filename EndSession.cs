using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace triton1
{
    static class EndSession
    {

        // api.icq.net/aim/endSession?f=json&aimsid=151.0770045335.**********%3A672880971&invalidateToken=1
        public static int end_session()
        {
            string EndSessionUrl;
            string resp;
            EndSessionUrl = "https://api.icq.net/aim/endSession?f=json&aimsid=" + http_sender.EscapeSymbol(Fetch.aimsid) + "&invalidateToken=1";
            resp = http_sender.GET(EndSessionUrl);
             return get_response_code(resp);
           
        }

        private static int get_response_code(string response)
        {
            int Status;
            dynamic json = Newtonsoft.Json.Linq.JObject.Parse(response);
            Status = (int)json["response"]["statusCode"];
            return Status;

        }

    }
}
