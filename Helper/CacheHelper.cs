using System;
using System.Collections.Generic;
using Line_bot.Models;

namespace Line_bot.Helper
{
    public class CacheHelper
    {
        //Dictionary是一個類別 Dictionary<key, value> 角括號只能放類別，key是唯一值。一個key對應一個value。
        //初始值的寫法只會給予一次，用"="給初始值
        private static Dictionary<string, UserInfo> Userinfos { get; set; } = new Dictionary<string, UserInfo>();

        //下行的"UserInfo"是指Get這個function會回傳的型態，其形態為"UserInfo"。
        public static UserInfo Get(string id)
        {
            UserInfo info = Userinfos.GetValueOrDefault(id);
            if (info == null)
            {
                info = new UserInfo();
                Set(id, info);
            }
            /*else if (IsTimeOut(info))
            {
                info = new UserInfo();
                info.State = States.TimeOut.ToString();
                Set(id, info);
            }*/
            return info;
        }
        public static void Set(string id, UserInfo info)
        {
            //使用TryAdd是因為有可能key(id)會重複。
            bool isSuccess = Userinfos.TryAdd(id, info);
            if (!isSuccess)
            {
                info.UpdateTime = DateTime.Now;
                Userinfos.Remove(id);
                Userinfos.Add(id, info);
            }
        }
        public static void Clear(string id, UserInfo info)
        {
            Userinfos.Remove(id);
        }
        /*private static bool IsTimeOut(UserInfo info)
        {
            double durningTime = (DateTime.Now - info.UpdateTime).TotalSeconds;
            return durningTime >= 5;
        }*/
    }
}
