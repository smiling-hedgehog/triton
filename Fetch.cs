using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;

namespace triton1
{
    public static class Fetch
    {
        public static string aimsid;
        private static string FetchTile = "&f=json&r=1&timeout=1&peek=0&hidden=1";
        private static string FetchBaseUrl;
        private static int r = 1;
        public static bool isWork = true;
        private static string resp;
        public static void getContactList(string response)
        {
            /*При инициализации, fetchBaseUrl отличается присутствием дополнительного параметра first=1.
             При наличии которого сервер отдаёт список контактов*/
            dynamic json = JObject.Parse(response);
            aimsid = (string)json["response"]["data"]["aimsid"];
            FetchBaseUrl = (string)json["response"]["data"]["fetchBaseURL"];
            /*пример //bos.icq.net/bos-d013e/aim/fetchEvents?aimsid=026.0663845947.1633452841:742343553&first=1&rnd=1553887966.752189
             далее можем GET запросом получить список контактов
            Console.WriteLine("get contact list "+ FetchBaseUrl)*/
            ;
        }
        public static void FetchEvent()
        {  
            string fetchUrl;
            r++;
            FetchTile = "&f=json&r=" + r + "&timeout=1&peek=0";
            fetchUrl = FetchBaseUrl + FetchTile;
            resp = http_sender.GET(fetchUrl);

            if (get_response_code(resp) == 200)
            {
                dynamic json = JObject.Parse(resp);
                FetchBaseUrl = (string)json["response"]["data"]["fetchBaseURL"];
              
                fetchUrl = FetchBaseUrl + FetchTile;
                recvData.AsyncParseData(resp);
               
            }
            else
            {
                Console.WriteLine("Error code " + get_response_code(resp));
            }
        
        }
        private static int get_response_code(string response)
        {
         int Status;
            dynamic json = JObject.Parse(response);
            Status = (int)json["response"]["statusCode"];
            return Status;

        }

        public static async void AsyncUpdateState()
        {
            await Task.Run(() =>
            {
                while (isWork)
                {
                    
                    Fetch.FetchEvent();
                   
                }


            });

        }
    }
}
