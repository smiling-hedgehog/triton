using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace triton1
{
    static class recvData
    {
        public static string reqId;

   

        public static void AsyncParseData(string json)
        {
          
            try
            {
                RootObject p = JsonConvert.DeserializeObject<RootObject>(json);
                reqId = p.response.requestId;
                if (p.response.data.events.Count>0)/*если нам пришел ответ с массивом данных*/
                {
                    if (p.response.data.events[0].eventData.tail != null)/*если есть какое то сообщение*/
                    {
                        if (p.response.data.events[0].eventData.tail.messages[0].outgoing!=true)/*это сообщение не подтверждение успешной доставки*/
                        {
                            Logger.saveLog(json);
                            Logger.saveLog("----------------------------------------------------------");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(p.response.data.events[0].eventData.tail.messages[0].text);/*вытаскиваем месседж*/



                            if (p.response.data.events[0].eventData.tail.messages[0].text == "проверка связи")
                            {
                                sendData.sendMessage("На связи", "672880971");

                            }



                            Console.ForegroundColor = ConsoleColor.White;
                        }

                    }
                    else if (p.response.data.events[0].type == "typing")/*если тип сообщения typing значит нам печатают*/
                    {

                        Console.WriteLine("typing");
                    }
                }
            }
            catch (Exception e)
            {
               
                Console.WriteLine(e.Message + " " + e.StackTrace);

            }
        }


    }
}
