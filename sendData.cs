using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace triton1
{
    static class sendData
    {

      
        private static string buildRequestMesssage(string message,string uin)
        {
            string dataMessageUrl;
            string requestId;


            //string json = req;
            // dynamic pl = JObject.Parse(json);
            requestId = recvData.reqId;//(string)pl["requestId"];
            //aimsid=012.0244339948.3200095238%3A742343553&f=json&message=Test%20Hello%20World&notifyDelivery=true&offlineIM=1&r=7c24891b-ab95-4bb6-9120-3b1afa99338c-1&t=672880971

            dataMessageUrl = "aimsid=" + Fetch.aimsid + "&f=json&message=" + EncodingTextData(message) + "&notifyDelivery=true&offlineIM=1&r=" + requestId + "&t=" + uin; 

            return dataMessageUrl;
        }
        private static string EncodingTextData(string message)
        {
            Encoding utf8 = Encoding.GetEncoding("UTF-8");
            Encoding win1251 = Encoding.GetEncoding("Windows-1251");
            byte[] utf8Bytes = win1251.GetBytes(message);
            byte[] win1251Bytes = Encoding.Convert(win1251, utf8, utf8Bytes);
            return http_sender.EscapeSymbol(win1251.GetString(win1251Bytes));
        }

        public static void sendMessage(string message,string uin)
        {
            string response;
            response = http_sender.POST("https://api.icq.net/im/sendIM", sendData.buildRequestMesssage(message, uin));
                
                if(get_response_code(response)!=200)
                {
                Console.WriteLine("Ошибка отправки сообщения");
                }


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
