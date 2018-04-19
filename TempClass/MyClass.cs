using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TempClass
{
    public class functionClass
    {
        public static List<string> listOddLetters = new List<string>() { ",", ".", "!", "?", "-", "=", "\"", "$", "#", "|", "(", ")", "\"" };

        // Ham tra ve gia tri nho hon trong hai so 
        public static int getMin(int a, int b)
        {
            return a <= b ? a : b;
        }

        // Ham go bo nhung ky tu dac biet trong mot chuoi 
        public static  string removeOddLetter(string str)
        {
            // go bo khoang trang hai dau 
            str = str.Trim();

            // go bo cac ky tu dac biet 
            for(int i=0; i<listOddLetters.Count; i++)
            {
                while (str.Contains(listOddLetters[i]))
                {
                    str = str.Replace(listOddLetters[i], " ");
                }
            }

            // go bo 2 khoang trang thanh mot khoang trang 
            while(str.Contains("  "))
            {
                str = str.Replace("  ", " ");
            }

            // go bo dau " ' " trong chuoi dau vao neu dau khong co y nghia 

            /*
            while (str.Contains("'"))
            {
                int index = str.IndexOf("'");
                if(str[index-1] == ' ' || str[index+1] ==' ')
                {
                    string temp = str.Substring(index+1);
                    str = str.Substring(0, index);
                    str = string.Concat(str, temp);

                }
            }

    */
            return str;
        }

        // chuan hoa chuoi ve dang cac tu cach nhau boi dau '-'

        public static string standardziedString (string str)
        {
            str = removeOddLetter(str);
            while(str.Contains(" "))
            {
                str = str.Replace(" ", "-");
            }
            return str;
        }

        // go bo khoang trang cua mot chuoi
        public static string removeSpace(string str)
        {
            str = str.Trim();
            while(str.Contains(" "))
            {
                str = str.Replace(" ", "");
            }
            return str;
        }

        // ham tra ve vi tri xh cuoi cung cua mot chuoi dich trong chuoi nguon 
        public static int indexLastSubString(string s1, string s2)
        {
            if (s2.Contains(s1))
            {
                return s2.LastIndexOf(s1);
            }
            else
            {
                return -1;
            }
        }
    }

    public class LCS
    {
        // ham tra ve chuoi con chung dai nhat giua hai chuoi dau vao ( 1 o dang string la chuoi nguon va mot o dang list ) 
        public static List<string> getLCS(string[] arr1, string arr2)
        {
            List<string> primaryList = new List<string>();
            for(int j= arr1.Length-1; j>=0; j--)
            {
                List<string> tempList = new List<string>();
                string tempString = arr2;
                int lastIndex = 10000000;
                for (int i =j; i >= 0; i--)
                {
                    int temp = functionClass.indexLastSubString(arr1[i],tempString);
                    if(temp != -1)
                    {
                        // Chỉ lấy kết quả chuỗi nằm ở vị trí đứng trước vị trí đã được đánh dâu. Và mỗi lần phải resize lại chuỗi so sánh 
                        if(temp < lastIndex)
                        {
                            lastIndex = temp;
                            tempString = tempString.Substring(0, lastIndex);
                            tempList.Add(arr1[i]);
                        }
                    }
                }
                if (primaryList.Count <= tempList.Count)
                {
                    primaryList = tempList;
                }
            }
            return primaryList;
        }
    }
   

    public class MyClass
    {
      
        public static void Main(string[] args)
        {   

            // chuong trinh Test
              string content = "Bob picks up the ball. He throws the ball. Bill catches the ball. Bill throws the ball back. Bob catches the ball. Bob throws the ball to Bill. Bill drops the ball. Bill picks it up. He throws it over Bob's head. Bob runs back. He jumps up. He catches the ball";
            content = functionClass.removeOddLetter(content);
            content = content.ToLower();
              string input= "Bob picks up the ball. He throws the ball. Bill catches the ball. Bill throws the ball back. Bob catches the ball. Bob throws the ball to Bill. Bill drops the ball. Bill picks it up. He throws it over Bob's head. Bob runs back. He jumps up. He catches the ball";
            Console.WriteLine("Input :");
            Console.WriteLine(input);
            input = functionClass.removeOddLetter(input);
            input = input.ToLower();
            string[] arrayString = input.Split(new char[] { ' ' });
            List<string> result = LCS.getLCS(arrayString, content);
            Console.WriteLine("Content : ");
            Console.WriteLine(content);
            content = functionClass.standardziedString(content);
            Console.WriteLine("Content 2 : ");
            input = functionClass.removeSpace(input);
            Console.WriteLine(content);
            Console.WriteLine("Result : ");
            for (int i = result.Count-1; i>=0 ; i--)
            {
                Console.Write(result[i]);
                Console.Write(" ");
            }
            Console.ReadKey();
        }
    }
}

/*
Note : giải thuật cho bài toán tìm chuỗi con có độ dài lớn nhất giữa hai chuỗi 
    Ta nhận thấy bài toán là bài toán quy hoạch động 
    Việc nhận được lời giải bằng cách chia bài toán bằng cách giải quyết các bài toán con tương nhỏ có mức độ nhỏ hơn 
    Bài toán : Giả sử chuỗi con thu được S. S sẽ thu được từ bài toán S = maxSub(S1, S2); với S1 là chuỗi nhỏ nhất 
    S2 là chuỗi S1 cộng thêm một phần tử (" Từ ") .... cho đến khi chuỗi S2 thu được là chuỗi đầu vào. 

    Việc giải bài toán được xét từ chuỗi con nhỏ nhất của 1 chuỗi đầu vào (từ phải sang trái) ... ứng với mỗi chuỗi con ta thu được 1 chuỗi con dài nhất ... Từ 
    đây ta so sánh các chuỗi con dài nhất thu được rồi dẫn ra chuỗi dài nhất .

    */