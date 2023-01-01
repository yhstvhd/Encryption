﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Encryption
{
    /// <summary>
    /// ファイル保存データクラス
    /// </summary>
    internal class SaveEntity
    {
        //暗号化キー
        public string Key { get; set; }

        //パスワードリスト
        public List<UsagePass> PassList { get; set; } = new List<UsagePass>();
    }

    /// <summary>
    /// パスワードリストのデータクラス
    /// </summary>
    internal class UsagePass
    {
        //用途
        public string Usage { get; set; }

        //パスワード
        public string Password { get; set; }
        public UsagePass(string usage, string password)
        {
            Usage = usage;
            Password = password;
        }
    }
}
