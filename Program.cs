using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
/*742343553  Ltybcrf159632*/
namespace triton1
{
    class Program
    {
        static void Main(string[] args)
        {
           // GUID g = new GUID();
            string response = "";
            string message = "";
           
       
Console.WriteLine("-------------------------------1 step - ClientLogin  ----------------------");
response = http_sender.POST("https://api.login.icq.net/auth/clientLogin", "clientName=Mail.ru%20Windows%20ICQ&clientVersion=10.0.1999&devId=ic1nmMjqg7Yu-0hL&f=json&pwd=Ltybcrf159632&r=dce7ba59-bf6e-4daa-ad63-67e04799dd6b&s=742343553&tokenType=longterm");
Console.WriteLine(response);
Console.WriteLine("-------------------------------2 step - StartSession ----------------------");
StartSession start = new StartSession(response);
response = http_sender.POST("https://api.icq.net/aim/startSession", start.getStrPack());
Console.WriteLine(response);
Console.WriteLine("-------------------------------3 step - fetchBaseURL ----------------------"); 
        Fetch.getContactList(response);
        Fetch.FetchEvent();
Console.WriteLine("-------------------------------4 step - setTimeZone ----------------------");
response = http_sender.GET(setTimeZone.initTimeZone(response));
Console.WriteLine(response);
//Console.WriteLine("-------------------------------5 step - sendMessage ----------------------");
//response = http_sender.POST("https://api.icq.net/im/sendIM", sendData.getDataMesssage(response, "online"));
           
           // Console.WriteLine(response);
#region MainLoop
            Fetch.AsyncUpdateState();

            do
            {
                message = Console.ReadLine();
                if (message.Equals("exit"))
                {
                    Fetch.isWork = false;
                    Thread.Sleep(1000);
                    Console.WriteLine(EndSession.end_session());

                }
                else
                {
                    sendData.sendMessage(message, "672880971");
                }

            } while (!message.Equals("exit"));

#endregion
            Console.WriteLine("-------------------------------6 step - EndSession ----------------------");
        
           
                Console.ReadKey();
        }
    }
}

