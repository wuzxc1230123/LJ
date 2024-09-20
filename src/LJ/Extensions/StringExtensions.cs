using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LJ.Extensions
{
    /// <summary>
    /// String 拓展
    /// </summary>
    public static partial class StringExtensions
    {

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToUpperCamelCase(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return str;

            return string.Concat(str.First().ToString().ToUpper(), str.AsSpan(1));
        }
    }
}
