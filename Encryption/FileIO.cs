using System.IO;
using System.Text;

namespace Encryption
{
	internal class FileIO
	{
		//ファイルパス
		private const string path = @"C:\Users\Public\pass.txt";

		/// <summary>
		/// ファイル書き出し
		/// </summary>
		/// <param name="text">書き出しテキスト</param>
		/// <param name="path">ファイルパス</param>
		public static void Save(string text, string path = path)
		{
			//上書きモードでファイル書き出し
			using (var writer = new StreamWriter(path, false, Encoding.UTF8))
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
			if (!File.Exists(path))
			{
				Save("");
			}
			using (var reader = new StreamReader(path))
			{
				var text = reader.ReadToEnd();
				return text;
			}
		}
	}
}
