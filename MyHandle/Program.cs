using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHandle
{
    public class FormatString
    {

        private static readonly string[] VietNamChar = new string[]
            {
        "aAeEoOuUiIdDyY",
        "áàạảãâấầậẩẫăắằặẳẵ",
        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
        "éèẹẻẽêếềệểễ",
        "ÉÈẸẺẼÊẾỀỆỂỄ",
        "óòọỏõôốồộổỗơớờợởỡ",
        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
        "úùụủũưứừựửữ",
        "ÚÙỤỦŨƯỨỪỰỬỮ",
        "íìịỉĩ",
        "ÍÌỊỈĨ",
        "đ",
        "Đ",
        "ýỳỵỷỹ",
        "ÝỲỴỶỸ"
            };

        public static string[] arrayStr = new string[] { "!", ",", ".", "\"", "\\", "/", "-", "  " };
        public static string filterSeal(string str)
        {
            // thay thế và lọc dấu từng char
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                {
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);

                }
            }
            return str;
        }
        public static string removeSpace(string str)
        {
            str = str.Trim();
            while (str.Contains("  "))
            {
                str = str.Replace("  ", " ");
            }
            str = str.Replace(" ", "-");
            str = str.ToLower();
            return str;
        }
        public static string removeOddLetter(string str)
        {
            str = str.Trim();
            for (int i = 0; i < arrayStr.Length; i++)
            {
                while (str.Contains(arrayStr[i]))
                {
                    str = str.Replace(arrayStr[i], " ");
                }
            }
            str = str.ToLower();
            return str;
        }
        public static string friendUrl(string str)
        {
            str = str.Trim();
            while (str.Contains("  "))
            {
                str = str.Replace("  ", " ");
            }
            str = str.Replace(" ", "-");
            str = str.ToLower();
            return str;
        }
        public static string[] splitString(string str)
        {
            string[] result = str.Split(new char[] { '-' });
            return result;
        }
    }

    class Program
    {
        //static void Main(string[] args)
        //{
        //    string str = "Xin chào bạn      mình là Sang  ! \\ -    hiện đang là sinh viên BK    ";
        //    //str = FormatString.filterSeal(str);
        //    str = FormatString.removeOddLetter(str);
        //    Console.WriteLine(str);
        //    string[] temp = FormatString.splitString(str);
        //    foreach (var i in temp)
        //    {
        //        Console.WriteLine(i);
        //    }
        //    Console.Read();
        //}
    }
}

