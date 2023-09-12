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
{   //創建LineKey這個類別
    //並且這個類別具有Authoriztion這個屬性和兩個function
    public class LineKey
    {
        public string Authorization { get; set; }

        //key.json的json文字檔在讀取後先丟給一個變數儲存
        //因為json文字檔包含了大小括號以及名稱等等(就是整個文字檔)
        //利用ReadToEnd()方法從頭讀取到尾並且以字串方式去給變數儲存
        //接著利用JsonConvert.DeserializeObject方法將完全字串的json檔案存取authorization部分
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
