using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("パスワードを入力してください");
            //共通暗号Key
            Encryption.Key = Console.ReadLine();

            Console.WriteLine("処理を選択してください");
            Console.WriteLine("1:   暗号化");
            Console.WriteLine("2:   復号");
            var selected = Console.ReadLine();

            switch (selected)
            {
                case "1":
                    //平文
                    string plainText = "passsw@rd";
                    Console.WriteLine("plain: {0}", plainText);

                    var str = Encryption.Encrypt(plainText);

                    FileIO.Write(str);
                    break;
                case "2":
                    //ファイルから読み出し
                    string text = FileIO.Read();
                    var decoded = Encryption.Decode(text);
                    Console.WriteLine(decoded);

                    break;
                default:
                    Console.WriteLine("正しいものを入力してください。");
                    break;
            }

            Console.ReadKey();
        }
    }
}
