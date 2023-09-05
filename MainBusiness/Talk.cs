using System;
using System.Linq;
using Line_bot.Helper;
using Line_bot.Models;
using Line_bot.MainBusiness.Model;

namespace Line_bot.MainBusiness
{
    public class Talk
    {
        private string Hint { get; set; }
        public string[] EatWhat { get; set; }
        public string[] Porks { get; set; }
        public Talk()
        {
            EatWhat = new[] { "吃啥", "吃什麼", "吃甚麼", "假啥", "吃?" };
            Porks = new[] { "豬肉", "死豬", "胖豬", "胖胖", "肥豬" };
        }
        public void NormalTalk(string inputText, UserInfo info, Input input, string id)
        {
            if (inputText == "好累")
            {
                this.Hint = "年假結束QQ";
            }
            else if (inputText == "涂已其")
            {
                string[] reply = { "豬肉 𓃟 ", "死胖豬", "滴8改==", "大漂亮^^", "哀丫擠醬", "吵啥吵0.0", "看書好不好 ==" };
                Random replyAns = new Random();
                int replyNum = replyAns.Next(0, 6);
                this.Hint = reply[replyNum];
            }
            else if (inputText == "涂已華")
            {
                string[] reply = { "大帥哥", "嗯~哥哥好濕", "潮到吃水", "帥氣", "帥氣無法擋" };
                Random replyAns = new Random();
                int replyNum = replyAns.Next(0, 4);
                this.Hint = reply[replyNum];
            }
            else if (inputText == "涂已狒")
            {
                string[] reply = { "壞脾氣心理師請更新謝謝 ^^", "哇澎湖花ㄝ", "夭壽水枸杞", "還不去運動啊", "澎湖豬五花" };
                Random replyAns = new Random();
                int replyNum = replyAns.Next(0, 4);
                this.Hint = reply[replyNum];
            }
            else if (inputText == "涂酥華")
            {
                string[] reply = { "呼叫樟腦丸 ! !", "還敢玩手機阿ㄇㄉㄈㄎ", "開車帶大家出去丸耶~~", "大江gogo", "還敢去打球阿", "去煮好吃的親", "幫涂已其解一下數學", "土豪哥請斗內給涂已華" };
                Random replyAns = new Random();
                int replyNum = replyAns.Next(0, 8);
                this.Hint = reply[replyNum];
            }
            else if (inputText == "chi阿")
            {
                string[] reply = { "chi車出去丸", "chi買多多給小布", "chi 買新衣服", "chiㄙㄤ一下" };
                Random replyAns = new Random();
                int replyNum = replyAns.Next(0, 3);
                this.Hint = reply[replyNum];
            }
            else if (inputText == "機機人")
            {
                string[] replyHello = new string[6] { "你好ㄚ~", "一起拉屎巴~", "哈哈", "Hello", "銃啥", "尛" };
                Random replyHelloAns = new Random();
                int ranNum = replyHelloAns.Next(0, 5);
                this.Hint = replyHello[ranNum];
            }
            else if (EatWhat.Contains(inputText))
            {
                string[] meals = new string[9];
                meals[0] = "陳記大滷麵";
                meals[1] = "冬粉鴨";
                meals[2] = "賣噹噹";
                meals[3] = "肯德基";
                meals[4] = "至尊便當";
                meals[5] = "大便 💩";
                meals[6] = "八方";
                meals[7] = "羊博士便當";
                meals[8] = "還敢吃啊豬肉 : )";
                Random replyMealsAns = new Random();
                int ranNum = replyMealsAns.Next(0, 8);
                this.Hint = meals[ranNum];
            }
            else if (Porks.Contains(inputText))
            {
                string[] replyPorks = { "呼叫涂已其", "呼叫涂已慧", "胖胖老師請回答", "涂羽胖...好像叫到太多人0.0" };
                Random replyPorkAns = new Random();
                int replyPorksNum = replyPorkAns.Next(0, 3);
                this.Hint = replyPorks[replyPorksNum];
            }
            else if (inputText == "玩遊戲")
            {
                this.Hint = "請在0~100之中猜數字!!";
                info.State = States.GuessPlayer.ToString();
                CacheHelper.Set(id, info);
            }
            if (inputText != "玩遊戲")
            {
                CacheHelper.Clear(id);
            }

        }


        public string GetResult()
        {
            return this.Hint;
        }


    }
}
