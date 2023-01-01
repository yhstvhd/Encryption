using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Encryption
{
	/// <summary>
	/// 暗号化クラス
	/// </summary>
	internal static class Encryption
	{
		//初期化ベクトル
		//128bit
		private const string AES_IV_128 = @"pf69DL6GrWFyZcMK";

		////暗号化鍵
		////256bit
		//private const string AES_KEY_256 = @"G'&'(&SIKDSDFSF)DSKJHFDFSD+JFSDJ";

		//ブロックサイズ（暗号・復号処理単位)
		private const int BlockSize = 128;

		//AES-256
		private const int KeySize = 256;


		/// <summary>
		/// 暗号化鍵（共通パスワード）
		/// </summary>
		public static string Key
		{
			private get;
			set;
		}

		/// <summary>
		/// 対称鍵暗号を使って文字列を暗号化する
		/// </summary>
		/// <param name="text">暗号化するテキスト</param>
		/// <param name="key">暗号化キー(共通パスワード)</param>
		/// <returns>暗号文</returns>
		public static string Encrypt(string text)
		{
			using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
			{
				// ブロックサイズ（何文字単位で処理するか）
				rijndaelManaged.BlockSize = BlockSize;
				// 暗号化方式はAES-256を採用
				rijndaelManaged.KeySize = KeySize;
				//暗号利用モード
				rijndaelManaged.Mode = CipherMode.CBC;
				//パディング
				rijndaelManaged.Padding = PaddingMode.PKCS7;

				rijndaelManaged.IV = Encoding.UTF8.GetBytes(AES_IV_128);
				rijndaelManaged.Key = Encoding.UTF8.GetBytes(Key);
				//暗号化
				ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor(rijndaelManaged.Key, rijndaelManaged.IV);

				byte[] encrypted;
				using (MemoryStream mStream = new MemoryStream())
				{
					using (CryptoStream ctStream = new CryptoStream(mStream, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter sw = new StreamWriter(ctStream))
						{
							sw.Write(text);
						}
						encrypted = mStream.ToArray();
					}
				}

				// Base64形式（64種類の英数字で表現）で返す
				return (Convert.ToBase64String(encrypted));
			}
		}

		/// <summary>
		/// 暗号文を復号する
		/// </summary>
		/// <param name="cipher">暗号文</param>
		/// <returns>復号文</returns>
		public static string Decode(string cipher)
		{
			using (var rijndael = new RijndaelManaged())
			{
				//暗号化サイズ、AES256設定
				rijndael.BlockSize = BlockSize;
				rijndael.KeySize = KeySize;
				//暗号利用モード、パディング設定
				rijndael.Mode = CipherMode.CBC;
				rijndael.Padding = PaddingMode.PKCS7;
				//ベクトル、Key設定
				rijndael.IV = Encoding.UTF8.GetBytes(AES_IV_128);
				rijndael.Key = Encoding.UTF8.GetBytes(Key);

				//復号
				ICryptoTransform cryptoTransform = rijndael.CreateDecryptor(rijndael.Key, rijndael.IV);
				string plain = string.Empty;
				//テキスト変換
				using (MemoryStream stream = new MemoryStream(Convert.FromBase64String(cipher)))
				{
					using (CryptoStream cryptoStream = new CryptoStream(stream, cryptoTransform, CryptoStreamMode.Read))
					{
						using (StreamReader sr = new StreamReader(cryptoStream))
						{
							plain = sr.ReadLine();
						}
					}
				}
				return plain;
			}
		}
	}
}
