using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;

namespace triton1
{
    static class http_sender
    {
        public static int nonce_;
        public static string aimsid;

        public static string GET(string url)
        {
           
            try
            {
                string Out = String.Empty;

                using (var webClient = new WebClient())
                {
                    webClient.Headers.Add("User-Agent", "742343553 Mail.ru Windows ICQ (version 10.0.1999)");
                    webClient.Encoding = Encoding.UTF8;
                    
                    // Выполняем запрос по адресу и получаем ответ в виде строки
                    Out = webClient.DownloadString(url);
                    webClient.Dispose();
                }

           
                return Out;
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message + " " + e.HResult);
                return null;

            }



        }

     /*   public static string FetchEvent(string resp)
        {
            string fbu;
            string json = resp;
            dynamic pl = JObject.Parse(json);
           fbu = (string)pl["response"]["data"]["fetchBaseURL"];
            fbu += "&f=json&r=1&timeout=1&peek=0&hidden=1";
            aimsid = (string)pl["response"]["data"]["aimsid"];
            return fbu;
        }
        */

        public static string POST(string Url, string Data)
        {
            try
            {
                System.Net.WebRequest req = System.Net.WebRequest.Create(Url);
                req.Method = "POST";
                req.Timeout = 1000;
                req.ContentType = "application/x-www-form-urlencoded";
              
             //   req.Headers.Add("Content-Encoding", "gzip");
                byte[] sentData = Encoding.GetEncoding(1251).GetBytes(Data);
                req.ContentLength = sentData.Length;
                System.IO.Stream sendStream = req.GetRequestStream();
                sendStream.Write(sentData, 0, sentData.Length);
                sendStream.Close();
                System.Net.WebResponse res = req.GetResponse();
                System.IO.Stream ReceiveStream = res.GetResponseStream();
                System.IO.StreamReader sr = new System.IO.StreamReader(ReceiveStream, Encoding.UTF8);
                //Кодировка указывается в зависимости от кодировки ответа сервера
                Char[] read = new Char[256];
                int count = sr.Read(read, 0, 256);
                string Out = String.Empty;
                while (count > 0)
                {
                    String str = new String(read, 0, count);
                    Out += str;
                    count = sr.Read(read, 0, 256);
                }
                nonce_++;
                return Out;
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message +" " + e.HResult);
                return null;
            }
           
        }

        public static string hmac_base64(string pwd, string data)
        {
            byte[] key = new byte[64];
            key = Encoding.Default.GetBytes(pwd);
            Stream s = new MemoryStream(ASCIIEncoding.Default.GetBytes(data));
            HMACSHA256 pl = new HMACSHA256(key);
            byte[] hashValue = pl.ComputeHash(s);
            pl.Dispose();
            return System.Convert.ToBase64String(hashValue);
        }
        private static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static long getCurrentTimeToUnixFormat()
        {
            //return System.Convert.ToString(DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
            return System.Convert.ToInt64(DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
        }
         private static bool is_latin(char _c)
        {
            if ((_c >= 'a' && _c <= 'z') || (_c >= 'A' && _c <= 'Z'))
                return true;

            return false;
        }
       private static bool is_digit(char _c)
        {
            return (_c >= '0' && _c <= '9');
        }
       public static string EscapeSymbol(string s)
        {
            var EscStr = "";

            for (int i = 0; i < s.Length; i++)
            {
                if (is_latin(s[i]) || is_digit(s[i]) || Strchr(s[i].ToString(), "-._~") || s[i].ToString() ==  "." || s[i].ToString() == "-")
                {
                    EscStr += s[i];
                }
                else
                {
                    byte[] ba = Encoding.Default.GetBytes(s[i].ToString());
                    var hexString = BitConverter.ToString(ba);
                    hexString = hexString.Replace("-", "");
                    EscStr = EscStr + '%' + hexString;
                }
            }
            return EscStr;

        }
        private static bool Strchr(string originalString, string charToSearch)
        {
            int found = originalString.IndexOf(charToSearch);
            return found > 0 ? true : false;
        }

       

    }



}
/*Auth on https://api.login.icq.net/auth/clientLogin step 1
 {"response":{"statusCode":200, "statusText":"OK", "requestId":"dce7ba59-bf6e-4da
a-ad63-67e04799dd6b", "data":{"hostTime":1547447139, "token":{"expiresIn":0, "a"
:"%2Fw8BAAAAAABLWRsoAAAAA4t1rHJSo0%2Fak30qj9ZRjssAAAAJNjcyODgwOTcxAAAAA3VpbgAAAAA%3D"}, "sessionSecret":"aG9VQ0R6V06vO4u9", "loginId":"672880971"}}}
*/
/*start session on https://api.icq.net/aim/startSession step 2
 /*{"response":{"statusCode":400, "statusText":"Invalid Request.  statusDetailCode
1015", "statusDetailCode":1015, "ts":1547541374, "data":{"ts":1547541374}}}
*/



     



