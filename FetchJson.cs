using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace triton1
{

    public class Yours
    {
        public long lastRead { get; set; }
    }

    public class Theirs
    {
        public long lastDelivered { get; set; }
        public long lastRead { get; set; }
    }

    public class Message
    {
        public long msgId { get; set; }
        public long reqId { get; set; }
        public bool outgoing{ get; set; }
        public string wid { get; set; }
        public int time { get; set; }
        public string notification { get; set; }
        public string locale { get; set; }
        public string text { get; set; }
        public string mediaType { get; set; }
    }

    public class Tail
    {
        public List<Message> messages { get; set; }
        public long olderMsgId { get; set; }
    }

    public class Person
    {
        public string sn { get; set; }
        public string friendly { get; set; }
    }

    public class EventData
    {
        public string sn { get; set; }
        public long lastMsgId { get; set; }
        public string patchVersion { get; set; }
        public int unreadCnt { get; set; }
        public Yours yours { get; set; }
        public Theirs theirs { get; set; }
        public Tail tail { get; set; }
        public List<Person> persons { get; set; }
    }

    public class Event
    {
        public string type { get; set; }
        public EventData eventData { get; set; }
        public int seqNum { get; set; }
    }

    public class Data
    {
        public int pollTime { get; set; }
        public int ts { get; set; }
        public string fetchBaseURL { get; set; }
        public int fetchTimeout { get; set; }
        public int timeToNextFetch { get; set; }
        public List<Event> events { get; set; }
    }

    public class Response
    {
        public int statusCode { get; set; }
        public string statusText { get; set; }
        public string requestId { get; set; }
        public Data data { get; set; }
    }

    public class RootObject
    {
        public Response response { get; set; }
    }
}
