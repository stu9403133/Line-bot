using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Line_bot.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Line_bot.MainBusiness.Model;
using Line_bot.Models;
using Line_bot.MainBusiness;

namespace Userinfos.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public IActionResult Index([FromBody] Input input)
        {
            //知道使用者是誰
            //三元運算式 : 是一個判斷式，為判斷如果成立就用前面的值(第一個);不成立就是後面那個值
            //語法糖
            //較難出現的先判斷
            //把每個id當一個人來玩，如群組當作一個人才成立
            string id = input.events[0].source.groupId != null ? input.events[0].source.groupId :
                        input.events[0].source.roomId != null ? input.events[0].source.roomId : input.events[0].source.userId;
            var info = CacheHelper.Get(id);

            string inputText = input.events[0].message.text;
            Output output = null;
            string outputText = "";

            //下面這段if是判斷這些狀態，控流程用的，
            //單一職責原則 (Single responsibility principle):相似功能的東西放一起。好閱讀
            //參考game 的class去抓出來做一個class

            if (info.State == States.Init.ToString())
            {
                Talk normalTalk = new Talk();
                normalTalk.NormalTalk(inputText, info, input, id);
                outputText = normalTalk.GetResult();
                
            }
            else if (info.State == States.GuessPlayer.ToString())
            {
                bool isNumber = int.TryParse(inputText, out int num);
                if (isNumber)
                {
                    //猜數字的方法 
                    //var 偷懶用的，後面型態是甚麼var就是甚麼
                    Game guessGame = new Game(num, info);
                    guessGame.PlayGuessGame();
                    outputText = guessGame.GetResult();

                    //這裡放快取重置的function

                    if (outputText == "冰狗~答對了")
                    {
                        //塞回去用的儲存回去。
                        CacheHelper.Clear(id);
                    }
                    CacheHelper.Set(id, info);
                }
                else
                {
                    outputText = "請輸入數字";
                    CacheHelper.Set(id, info);
                }
            }
            else if (info.State == States.TimeOut.ToString())
            {
                outputText = "忘記了ㄏㄏ請重新輸入";
                CacheHelper.Clear(id);
            }

            output = new Output()
            {
                replyToken = input.events[0].replyToken,
                messages = new List<TextMessage>()
                {
                    new TextMessage()
                    {
                        type = "text",
                        text = outputText
                    }
                }
            };

            Reply(output);
            return Ok();
        }
        private void Reply(Output output)
        {
            HttpClient Client = new HttpClient();
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.line.me/v2/bot/message/reply"),
                Headers = {
                        { HttpRequestHeader.Authorization.ToString(), "Bearer PXRHLnm9Ci6NpW87H0h3X8onCCKMCRe5n62osYe0tjrk4tcap7NyKbS9EvjHGA72J+Qykz1RKUKZvFiSGQN3snGZRQGMUdeJ0TF4GSXyTTneBFcbQiVCkkwdu7GRxqQT8iAlJ9pFZedDWn/zU0SgngdB04t89/1O/w1cDnyilFU=" },
                        { HttpRequestHeader.ContentType.ToString(), "application/json" }
                },
                Content = new StringContent(JsonConvert.SerializeObject(output), Encoding.UTF8, "application/json")
            };
            var response = Client.SendAsync(request);
        }
    }
}
