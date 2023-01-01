using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    internal class FileIO
    {
        //ファイルパス
        private const string path = @"C:\Users\YHSHD\Desktop\pass.txt";

        /// <summary>
        /// ファイル書き出し
        /// </summary>
        /// <param name="text">書き出しテキスト</param>
        /// <param name="path">ファイルパス</param>
        public static void Write(string text,string path = path)
        {
            //上書きモードでファイル書き出し
            using (var writer = new StreamWriter(path, true))
            {
                writer.WriteLine(text);
            }
        }

        /// <summary>
        /// ファイル読み込み
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <returns></returns>
        public static string Read(string path = path)
        {
            using (var reader = new StreamReader(path))
            {
                var text = reader.ReadToEnd();
                return text;
            }
        }

    }
}
