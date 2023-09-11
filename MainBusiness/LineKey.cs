using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Line_bot.MainBusiness.Model;
using Newtonsoft.Json;

namespace Line_bot.MainBusiness
{
    public class LineKey
    {
        public string Authorization { get; set; }

        public LineKey Read()
        {
            StreamReader r = new StreamReader("key.json");
            string jsonString = r.ReadToEnd();
            LineKey m = JsonConvert.DeserializeObject<LineKey>(jsonString);
            return m;
        }
        public void Reply(Output output)
        {
            LineKey Key = Read();
            HttpClient Client = new HttpClient();
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.line.me/v2/bot/message/reply"),
                Headers = {
                        { HttpRequestHeader.Authorization.ToString(), Key.Authorization},
                        { HttpRequestHeader.ContentType.ToString(), "application/json" }
                },
                Content = new StringContent(JsonConvert.SerializeObject(output), Encoding.UTF8, "application/json")
            };
            var response = Client.SendAsync(request);
        }
    }
}
