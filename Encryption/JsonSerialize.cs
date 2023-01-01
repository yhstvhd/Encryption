using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Encryption
{
    internal static class JsonSerialize<T>
    {
        /// <summary>
        /// Jsonにシリアライズします
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string Serialize(T o)
        {
            string jsonString = JsonSerializer.Serialize(o);
            return jsonString;
        }

        /// <summary>
        /// Jsonからデシリアライズします
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static T Deserialize(string str)
        {
            T obj = JsonSerializer.Deserialize<T>(str);
            return obj;
        }
    }
}
