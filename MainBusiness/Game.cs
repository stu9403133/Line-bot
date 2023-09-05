namespace Line_bot.Models
{
    public class Game
    {
        private int InputNum { get; set; }
        private string Hint { get; set; }
        private UserInfo Info { get; set; }

        //很適合用construct，不然每次輸入答案不一樣，一開始給一個職就不動了
        //封閉開放原則，程式與程式之間不要有相連性，好管理
        //此為建構子名稱與class依樣就是建構子
        public Game(int inputNum, UserInfo info)
        {
            this.InputNum = inputNum;
            this.Info = info;
        }
        //程式多載，有不同的進入點，因參數的不同有不同的結果。function名稱都一樣，如何決定是要跑上面的Fun.還是下面的，用吃幾個參數去判斷。
       
        public void PlayGuessGame()
        {
            if (this.Info.Ans < this.InputNum)
            {
                this.Hint = "小一點";
            }
            else if (this.Info.Ans > this.InputNum)
            {
                this.Hint = "大一點";
            }
            else
            {
                this.Hint = "冰狗~答對了";
            }
        }
        
        public string GetResult()
        {
            return this.Hint;
        }
    }
}
