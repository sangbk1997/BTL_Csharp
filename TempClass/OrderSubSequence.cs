using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempClass
{

    public class LCS
    {
        public static int getMin(int a, int b)
        {
            return a <= b ? a : b;
        }
        public  static int indexLastSubString(string s1, string s2)
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
        public static ArrayList getLCS(string[] arr1, string arr2)
        {
            ArrayList primaryList = new ArrayList();
            for(int j= arr1.Length-1; j>=0; j--)
            {
                ArrayList tempList = new ArrayList();
                int lastIndex = 10000000;
                for (int i =j; i >= 0; i--)
                {
                    int temp = LCS.indexLastSubString(arr1[i], arr2);
                    if(temp != -1)
                    {
                        if(temp < lastIndex)
                        {
                            lastIndex = temp;
                            tempList.Add(arr1[i]);
                        }
                    }
                }
                if (primaryList.Count < tempList.Count)
                {
                    primaryList = tempList;
                }
            }
            return primaryList;
        }
    }
   

    public class OrderSubSequence
    {
        public static List<string> listStr = new List<string>() {",",".","!","?","-","=","\"","$","#","|","(",")" };
        public static string formatString(string str)
        {
            str = str.Trim();
            for(int i=0; i < listStr.Count; i++)
            {
                while (str.Contains(listStr[i]))
                {
                    str = str.Replace(listStr[i], " ");
                }
            }

            while(str.Contains("  "))
            {
                str = str.Replace("  ", " ");
            }
            return str;
        }
        public static void Main(string[] args)
        {
            //string str,str1 = null;
            //str = "It is time to go to school. He puts his books into his backpack. He puts his arms through the straps. He puts his backpack on his back. He walks outside. He goes to the bus stop. He stands at the bus stop. He waits for the bus. He sees the bus ! # |";

            //str1 = "It is bk in my hear time to go He puts his backpack fantastic on his back. He walks hello i'm sangbk outside. He goes to great ideal the bus stop. He stands at the bus stop. He waits for the bus";
            //str = formatString(str);
            //str1 = formatString(str1);
            ////Console.WriteLine(str);
            //string[] result = str.Split(new char[] { ' ' });
            //string[] result1 = str1.Split(new char[] { ' ' });
            //for(int i=0; i<result.Length; i++)
            //{
            //    Console.WriteLine(result[i]);
            //}
            //for (int i = 0; i < result1.Length; i++)
            //{
            //    Console.WriteLine(result1[i]);
            //}

            
              string str = "Hello the world. My name world is Sang";
              str = formatString(str);
              string str2 = "Sang My hello name kaka hello Sang";
              str2 = formatString(str2);
              string[] arrayString = str2.Split(new char[] { ' ' });
              ArrayList result = LCS.getLCS(arrayString, str);
            Console.WriteLine(str);
            Console.WriteLine(str2);
            for (int i = result.Count-1; i>=0 ; i--)
            {
                Console.WriteLine(result[i]);
            }
            /*
            string s1 = "Hello sang how are you to day sang";
            string str2 = "sang name kaka hello Sang";
            str2 = formatString(str2);
            string[] arrayString = str2.Split(new char[] { ' ' });
            int result = LCS.indexLastSubString(arrayString[0], s1);
            Console.WriteLine(result);
            Console.WriteLine(arrayString[arrayString.Length-1]);*/

            /*
            ArrayList list1 = new ArrayList();
            ArrayList list2 = new ArrayList();
            list1.Add(1);
            list1.Add(3);
            list2.Add(2);
            if(list2.Count < list1.Count)
            {
                list2 = list1;
            }
            for(int i=0; i < list2.Count; i++)
            {
                Console.WriteLine(list2[i]);
            }*/
            Console.ReadKey();
        }
    }
}
