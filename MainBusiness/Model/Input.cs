using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Line_bot.MainBusiness.Model
{
    public class Input
    {
        public string destination { get; set; }
        public Event[] events { get; set; }
    }

    public class Event
    {
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
        public Message message { get; set; }
        public long timestamp { get; set; }
        public Source source { get; set; }
        public string replyToken { get; set; }
        public string mode { get; set; }
    }
    public class Message
    {
        public string type { get; set; }
        public string id { get; set; }
        public string text { get; set; }
    }
    public class TextMessage
    {
        public string type { get; set; }
        public string text { get; set; }
    }
    public class Output
    {
        public string replyToken { get; set; }
        public List<TextMessage> messages { get; set; }
    }
    public class Source
    {
        public string type { get; set; }
        public string userId { get; set; }
        public string groupId { get; set; }
        public string roomId { get; set; }
    }
}
