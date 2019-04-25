using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace triton1
{
 


    class StartSession
    {
        const string WIM_CAP_VOIP_VOICE = "094613504c7f11d18222444553540000";
        const string WIM_CAP_VOIP_VIDEO = "094613514c7f11d18222444553540000";
        const string WIM_CAP_FILETRANSFER = "094613434c7f11d18222444553540000";
        const string WIM_CAP_UNIQ_REQ_ID = "094613534c7f11d18222444553540000";
        const string WIM_CAP_EMOJI = "094613544c7f11d18222444553540000";
        const string WIM_CAP_MENTIONS = "0946135b4c7f11d18222444553540000";
        const string WIM_CAP_MAIL_NOTIFICATIONS = "094613594c7f11d18222444553540000";
        const string WIM_CAP_INTRO_DLG_STATE = "0946135a4c7f11d18222444553540000";
        const int SAAB_SESSION_OLDER_THAN_AUTH_UPDATE = 1010;
        const string WIM_API_START_SESSION_HOST = "https://api.icq.net/aim/startSession";
        const string ICQ_APP_IDTYPE = "ICQ";
        const string WIM_EVENTS = "myInfo,presence,buddylist,typing,dataIM,userAddedToBuddyList,service,webrtcMsg,mchat,hist,hiddenChat,diff,permitDeny,imState,notification,apps";
        const string WIM_PRESENCEFIELDS = "aimId,buddyIcon,bigBuddyIcon,iconId,bigIconId,largeIconId,displayId,friendly,offlineMsg,state,statusMsg,userType,phoneNumber,cellNumber,smsNumber,workNumber,otherNumber,capabilities,ssl,abPhoneNumber,moodIcon,lastName,abPhones,abContactName,lastseen,mute,livechat,official";
        const string WIM_INTERESTCAPS = "8eec67ce70d041009409a7c1602a5c84,"  + WIM_CAP_VOIP_VOICE  + ","  + WIM_CAP_VOIP_VIDEO;
        const string WIM_ASSERTCAPS = WIM_CAP_VOIP_VOICE + "," + WIM_CAP_VOIP_VIDEO + "," + WIM_CAP_UNIQ_REQ_ID + "," + WIM_CAP_EMOJI + "," + WIM_CAP_MAIL_NOTIFICATIONS + "," + WIM_CAP_MENTIONS + "," + WIM_CAP_INTRO_DLG_STATE;
        const string WIM_INVISIBLE = "false";
        const string WIM_APP_VER =  "5000";

        public SortedDictionary<string, string> post_parameters = new SortedDictionary<string, string>();
        private string rawData;
        private string token;
        private string sessionSecret;
        private long _hostTime;
        private long time_offset;
        public StartSession(string firstRequest)
        {
            rawData = firstRequest;
            dynamic pl = JObject.Parse(rawData);
            token = (string)pl["response"]["data"]["token"]["a"];
            sessionSecret = (string)pl["response"]["data"]["sessionSecret"]; ;
            _hostTime = (long)pl["response"]["data"]["hostTime"];
            time_offset = System.Convert.ToInt64(http_sender.getCurrentTimeToUnixFormat()) - Convert.ToInt64(_hostTime);
            buildStartPackage();
        }
        public void buildStartPackage()
        {
            try
            {
                set_post_parameter("a", http_sender.EscapeSymbol((token)));
                set_post_parameter("activeTimeout", "180");
                string assert_caps = WIM_ASSERTCAPS;
                set_post_parameter("assertCaps", http_sender.EscapeSymbol(assert_caps));
                set_post_parameter("buildNumber", "1999");
                set_post_parameter("clientName", http_sender.EscapeSymbol("Mail.ru Windows ICQ"));
                set_post_parameter("clientVersion", WIM_APP_VER);
                set_post_parameter("deviceId", "ae96e792-6139-4a08-9180-dd8d32e44cbd");//вообще надо через escape прогонять но нам не надо тк const
                set_post_parameter("events", http_sender.EscapeSymbol(WIM_EVENTS));
                set_post_parameter("f", "json");
                set_post_parameter("imf", "plain");
                set_post_parameter("inactiveView", "offline");
                set_post_parameter("includePresenceFields", http_sender.EscapeSymbol((WIM_PRESENCEFIELDS)));
                string interest_caps = WIM_INTERESTCAPS;
                set_post_parameter("interestCaps", http_sender.EscapeSymbol(interest_caps));
                set_post_parameter("invisible", WIM_INVISIBLE);
                set_post_parameter("k", "ic1nmMjqg7Yu-0hL");//вообще надо через escape прогонять но нам не надо тк const
                set_post_parameter("language", "en-us");//вообще надо через escape прогонять но нам не надо тк const
                set_post_parameter("majorVersion", "100");
                set_post_parameter("minorVersion", "0");
                set_post_parameter("mobile", "0");
                long ts = System.Convert.ToInt64(http_sender.getCurrentTimeToUnixFormat()) - time_offset;
                set_post_parameter("nonce", System.Convert.ToString(ts) + "-" +http_sender.nonce_);//счётчик сколько отправили столько и минус
                set_post_parameter("pointVersion", "0");
                set_post_parameter("rawMsg", "0");
                set_post_parameter("sessionTimeout", "7776000");

                set_post_parameter("ts", System.Convert.ToString(ts));//
                set_post_parameter("view", "online");

                //string sessionKey = http_sender.EscapeSymbol(http_sender.hmac_base64("Ltybcrf159632", sessionSecret));
                string sessionKey = http_sender.hmac_base64("Ltybcrf159632", sessionSecret);
                //Logger.saveLog("session key " + sessionKey);

                //set_post_parameter("sig_sha256", http_sender.EscapeSymbol(http_sender.hmac_base64(sessionKey, getStrStartSession(true))));//hmac in base64
                set_post_parameter("sig_sha256", http_sender.hmac_base64(sessionKey, getStrStartSession(true)));//hmac in base64

               // Logger.saveLog("sig_sha256 " + http_sender.EscapeSymbol(http_sender.hmac_base64(sessionKey, getStrStartSession(true))));


            }
            catch (Exception e) {

                Console.WriteLine(e.Message + " " + e.HResult);
            }



        }
        public void set_post_parameter(string key, string value)
        {
            post_parameters.Add(key, value);
        }

     
        public string getStrPack()
        {
            
            string str = "";
            foreach (KeyValuePair<string, string> keyValue in post_parameters)
            {
                str += keyValue.Key;
                str += "=";
              
                str += keyValue.Value;
                str += "&";
           
            }
            if (str.Length > 0)
            {
                str = str.TrimEnd('&');
           
            }
        

            return str;


        }

        public string getStrStartSession(bool method_)
        {
            //buildStartPackage();
            string str="";

       
            str = method_ ? "POST" : "GET";
           str = str + '&' + http_sender.EscapeSymbol(WIM_API_START_SESSION_HOST) + '&';

           

            foreach (KeyValuePair<string, string> keyValue in post_parameters)
            {
                if (keyValue.Key != "sig_sha256") {
                    str += keyValue.Key;
                    str += http_sender.EscapeSymbol("=");
                    str += http_sender.EscapeSymbol(keyValue.Value);
                    str += http_sender.EscapeSymbol("&");
                }
              
            }
            if (str.Length > 0)
            {
                //str = str.TrimEnd(http_sender.EscapeSymbol("&"));
                int ind = str.Length - 3;
                // вырезаем последний символ
               str =str.Remove(ind);
               // Console.WriteLine(text);

            }
            
            return str;
        }
    }
}
 