using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Line_bot.Models
{
    public enum States
    {
        Init,
        GuessPlayer,
        TimeOut,
    }
    public class UserInfo
    {
        public int Ans { get; set; }
        public string State { get; set; }
        public DateTime UpdateTime { get; set; }

        //建構子創造答案
        //建構子:只要new這個物件時就會同時產生
        //只有第一次會呼叫，答案不會變。
        public UserInfo()
        {
            this.State = States.Init.ToString();
            Random guessAns = new Random();
            this.Ans = guessAns.Next(0, 2);
        }
    }
}
