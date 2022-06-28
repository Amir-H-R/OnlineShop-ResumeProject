using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleAppTest
{

    public class EnemExa : IEnumerable
    {
        public void test()
        {
            List<string> lst = new List<string>();

            for (int i = 0; i < 11; i++)
            {
                lst.Add("item" + i.ToString());
            }

            IEnumerator<string> myEnumerator = lst.GetEnumerator();
          
        }



        public IEnumerator GetEnumerator()
        {
            string[] coureses = { "course1", "course2", "course3", "course4", "course5" };
            foreach (var item in coureses)
            {
                yield return item;
            }
        }
    }  

    public class Program
    {
        public Program()
        {

        }
        public string exam()
        {
            EnemExa enem = new EnemExa();
            enem.GetEnumerator();
            enem.test();
            return enem.ToString();
        }

        //public static string[] guidcreator()
        //{
        //    string[] guid = new string[101];
        //    for (int i = 0; i <= 100; i++)
        //    {
        //        guid[i] = Guid.NewGuid().ToString();

        //    }
        //    return guid;
        //}
        //public static bool validate()
        //{
        //    var s = Program.guidcreator();
        //    string[] f = new string[9999999];

        //    for (int k = 0; k < s.Length; k++)
        //    {
        //        var hj = s[k].Split("-");
        //        //foreach (var item in s)
        //        //{
        //       //var hjghg = item.Split("-");
        //        for (int i = 0; i < 1; i++)
        //        {
        //            f[k] = hj[4];
        //        }
        //        //}
        //    }

        //    foreach (var item in s)
        //    {
        //        var hjghg = item.Split("-");
        //        for (int i = 0; i <= 1; i++)
        //        {
        //            f[i] = hjghg[4];
        //        }

        //    }


        //    return true;
        //}
        public static void Main(string[] args)
        {
            Program program = new Program();
            program.exam(); 
            //var d = Program.guidcreator();
            //var b = Program.validate();
            //string s = "Amir dd sa dd-gg";
            //char[] c = s.ToCharArray();
            //string reverse = String.Empty;
            //for (int i = c.Length - 1; i > -1; i--)
            //{
            //    reverse += c[i];
            //}


            //int[] arr1= { 1, 2, 3 ,54};
            //int[] arr2= {6,2,7,1,4};
            //int[] arr3= {6,3,7,1,23,6,8};
            //int total = 0;
            //for (int i = 0; i < arr1.Length; i++)
            //{
            //    total += arr1[i];
            //}
            //for (int i = 0; i < arr2.Length; i++)
            //{
            //    total += arr2[i];
            //}
            //for (int i = 0; i < arr3.Length; i++)
            //{
            //    total += arr3[i];
            //}


            //  Console.WriteLine(total);
        }

    }
}
