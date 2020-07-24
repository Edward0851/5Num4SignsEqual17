using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCA
{
    class Program
    {
        static List<string> signmix = new List<string> { };
        static void Main(string[] args)
        {
            List<double> numbers= new List<double> {3,5,6,7,9 };
            List<char> givensigns = new List<char>{ '+', '-', '*', '/' };
            int count = 0; //count whether all possible combinations are examined
            //List<string> signmix = new List<string> { };
            SignGenerator("", givensigns, ref signmix);//generate all possible combination of signs

            Permutation(new List<double> { },numbers, ref count);
            Console.WriteLine(count);
            Console.Read();
            
        }

        //"fix" is used to store determined numbers, "numbers" are undetermined numbers
        static void Permutation(List<double> fix,List<double> numbers,ref int count)
        {
            
            foreach (double a in numbers)
            {
                fix.Add(a);
                if (numbers.Count > 1)
                {
                    List<double> transfer = new List<double>(numbers);
                    transfer.Remove(a);
                    Permutation(fix,transfer,ref count);
                }
                else
                {
                    if (Calculate(fix, signmix) != "")
                    {
                        Console.WriteLine(Calculate(fix, signmix));
                    }
                    count++;
                }
                fix.Remove(a);
            }
        }

        static void SignGenerator(string fix, List<char> signs,ref List<string> combsigns)
        {
            foreach(char a in signs)
            {
                fix = fix + a.ToString();
                if (signs.Count > 1)
                {
                    List<char> transfer = new List<char>(signs);
                    transfer.Remove(a);
                    SignGenerator(fix, transfer, ref combsigns);
                }
                else
                {
                    combsigns.Add(fix);
                }
                fix = fix.Remove(fix.Length-1);
            }
        }

        static string Calculate(List<double> Calnumbers,List<string> signs)
        {
            double aim = 18;
            foreach(string sign in signs)
            {
                double result = 0;
                List<double> temp = new List<double>(Calnumbers);
                CalculateEach(temp, sign,ref result);
                //Console.WriteLine(Calnumbers[0].ToString() + sign[0] + Calnumbers[1].ToString() + sign[1] + Calnumbers[2].ToString() + sign[2] + Calnumbers[3].ToString() + sign[3] + Calnumbers[4].ToString() + "=" + result);
                if (result == aim)
                {
                    Console.WriteLine(Calnumbers[0].ToString() + sign[0] + Calnumbers[1].ToString() + sign[1] + Calnumbers[2].ToString() + sign[2] + Calnumbers[3].ToString() + sign[3] + Calnumbers[4].ToString() + "=" + result);
                }
            }
            return "";
        }

        static void CalculateEach(List<double> Calnumbers, string sign,ref double x)
        {
            if (sign.IndexOf('*') >= 0 && (sign.IndexOf('*')< sign.IndexOf('/') || sign.IndexOf('/')<0))
            {
                int pointofcal = sign.IndexOf('*');
                x = Calnumbers[pointofcal] * Calnumbers[pointofcal + 1];
                Calnumbers.RemoveRange(pointofcal, 2);
                Calnumbers.Insert(pointofcal, x);
                sign = sign.Remove(pointofcal, 1);
                CalculateEach(Calnumbers, sign,ref x);
            }
            else if (sign.IndexOf('/') >= 0 && (sign.IndexOf('/') < sign.IndexOf('*') || sign.IndexOf('*')<0))
            {
                int pointofcal = sign.IndexOf('/');
                x = Calnumbers[pointofcal] / Calnumbers[pointofcal + 1];
                Calnumbers.RemoveRange(pointofcal, 2);
                Calnumbers.Insert(pointofcal, x);
                sign = sign.Remove(pointofcal, 1);
                CalculateEach(Calnumbers, sign, ref x);
            }
            else if(sign.IndexOf('+') >= 0 && (sign.IndexOf('+') < sign.IndexOf('-') || sign.IndexOf('-') < 0))
            {
                int pointofcal = sign.IndexOf('+');
                x = Calnumbers[pointofcal] + Calnumbers[pointofcal + 1];
                Calnumbers.RemoveRange(pointofcal, 2);
                Calnumbers.Insert(pointofcal, x);
                sign = sign.Remove(pointofcal, 1);
                CalculateEach(Calnumbers, sign, ref x);
            }
            else if (sign.IndexOf('-') >= 0 && (sign.IndexOf('-') < sign.IndexOf('+') || sign.IndexOf('+') < 0))
            {
                int pointofcal = sign.IndexOf('-');
                x = Calnumbers[pointofcal] - Calnumbers[pointofcal + 1];
                Calnumbers.RemoveRange(pointofcal, 2);
                Calnumbers.Insert(pointofcal, x);
                sign = sign.Remove(pointofcal, 1);
                CalculateEach(Calnumbers, sign, ref x);
            }
            else
            {
                return;
            }
            
        }
    }
}
