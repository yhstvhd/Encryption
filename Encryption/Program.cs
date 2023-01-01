using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Encryption
{
    internal class Program
    {

        static void Main(string[] args)
        {
            //パスワード入力
            Console.WriteLine("パスワードを入力してください");
            //共通暗号Key
            string key = Console.ReadLine();
            Encryption.Key = key;

            while (true)
            {
				Console.Clear();
				Console.WriteLine("処理を選択してください");
				Console.WriteLine("1:   暗号化");
				Console.WriteLine("2:   復号");
                Console.WriteLine();
                Console.WriteLine("終了する場合は[q]を押してください");
                Console.Write("番号？=>");

				var selected = Console.ReadLine();
				switch (selected)
				{
					case "1":
						Encode(key);
						break;
					case "2":
						Decode();
						break;
					case "q":
                        return;
					default:
						Console.WriteLine("正しいものを入力してください。");
						break;
				}
			}
        }

        /// <summary>
        /// 暗号化
        /// </summary>
        static void Encode(string key)
        {
            //保存
            var saveEntity = new SaveEntity();
            saveEntity.Key = Encryption.Encrypt(key);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("暗号化するパスワードを入力してください");
                Console.WriteLine("終了する場合は[q]を入力してください");

                var command = Console.ReadLine();
                switch (command)
                {
                    case "q":
                        //保存
                        //jsonにシリアライズ
                        var json = JsonSerialize<SaveEntity>.Serialize(saveEntity);
                        FileIO.Write(json);
                        return;
                    default:
                        Console.WriteLine("用途を入力します");
                        var usage = Console.ReadLine();

                        //パスワード暗号化
                        var password = Encryption.Encrypt(command);

                        saveEntity.PassList.Add(new UsagePass(usage, password));
                        break;
                }
            }
        }

        /// <summary>
        /// 復号
        /// </summary>
        static void Decode()
        {
            //ファイルから読み出し
            string fileText = FileIO.Read();

            //Jsonデシリアライズ
            var saveEntity = JsonSerialize<SaveEntity>.Deserialize(fileText);

            try
            {
                var decode = Encryption.Decode(saveEntity.Key);
            }
            catch (Exception)
            {
                Console.WriteLine("パスワードが不正です");
                return;
            }

            while (true)
            {
                Console.Clear();

                //用途だけ全部表示
                Console.WriteLine("復号するパスワードを番号で指定してください");
                Console.WriteLine("終了する場合は[q]を入力してください");
                Console.WriteLine("[番号] 用途      パスワード");
                for (int i = 0; i < saveEntity.PassList.Count; i++)
                {
                    Console.WriteLine("[{0}] {1}        ●●●●", i + 1, saveEntity.PassList[i].Usage);
                }
                Console.WriteLine();
                Console.Write("番号=>");
                var input = Console.ReadLine();



                try
                {
                    switch (input)
                    {
                        case "q":
                            return;
                        default:
                            var num = int.Parse(input);
                            //入力チェック
                            if (num > saveEntity.PassList.Count)
                            {
                                throw new Exception();
                            }

                            //デコード
                            var result = Encryption.Decode(saveEntity.PassList[num - 1].Password);

							Console.Clear();
							Console.WriteLine("■■結果■■");
                            //表示
                            Console.WriteLine(result);
                            Console.WriteLine();
                            Console.WriteLine("終了する場合はqを入力してください。それ以外は何かキーを押してください");
                            input = Console.ReadLine();
                            if(input == "q")
                            {
                                return;
                            }

                            break;
                    }

                }
                catch (Exception)
                {
                    Console.WriteLine("正しい番号を指定してください。");
                    return;
                }
            }
        }
    }
}
