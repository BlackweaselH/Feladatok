using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Feladatok
{
    internal class Program
    {

        public const int GENERATE_RANDOM_NUMBERS = 10;
        public const string TEXT_PATH = @"text.txt";


        //beolvasás
        public static List<string> GetStringFromFile()
        {
            return File.Exists(TEXT_PATH) ? File.ReadAllText(TEXT_PATH).Split(' ').ToList() : null;
        }

        //2. feladat írásjelek eltávolítása
        //3. feladat írásjelek eltávolítása
        //4. feladat írásjelek eltávolítása
        public static List<string> RemoveAllStave(List<string> text)
        {
            var local = new List<string>();
            foreach (var element in text)
            {
                element.ToLower();
                if (!(element.Contains(',') || element.Contains('.')))
                {
                    local.Add(element);
                }
                else
                {
                    string build = null;
                    for (var i = 0; i < element.ToCharArray().Length; i++)
                    {

                        if (!(element.ToCharArray()[i] == ',' || element.ToCharArray()[i] == '.'))
                            build += element.ToCharArray()[i];
                    }
                    local.Add(build);
                }
                
            }
            return local;
        }

        //4. Feladat karakter alapján 2 részre osztás
        public static string[] GetTwoElementFromString(string word, char character)
        {
            var local = new string[2];
            string[] elements = word.Split(character).ToArray();
            if (elements.Length > 2)
            {
                return null;
            }
            local[0] = elements[0];
            local[1] = elements[1];
            return local;
        }


        private static void Main(string[] args)
        {
            //Feladat 1
            var numbers = new List<int>();
            numbers.GetRandomNumbers<int>(GENERATE_RANDOM_NUMBERS);
            var array = numbers.ToArray();

            //Feladat 2
            //var input = Console.ReadLine();
            Console.WriteLine("2. Feladat --------");
            var input = "ipsum";
            var valami = RemoveAllStave(GetStringFromFile()).GetContainsPosition(input.ToLower());
            foreach (var i in valami)
                WriteLine(i);
            Console.WriteLine("-------------------");

            //Feladat 3
            //input = Console.ReadLine();
            input = "i?sum";
            Console.WriteLine("3. Feladat --------");
            valami = RemoveAllStave(GetStringFromFile()).GetContainsWithQuestionMark(input.ToLower());
            foreach (var i in valami)
                WriteLine(i);
            Console.WriteLine("-------------------");


            //Feladat 4
            input = "i*sum";
            Console.WriteLine("4. Feladat --------");
            valami =
                (List<string>)
                    RemoveAllStave(GetStringFromFile())
                        .Where(
                            x =>
                                x.Contains(GetTwoElementFromString(input, '*')[0]) &&
                                x.Contains(GetTwoElementFromString(input, '*')[1]))
                        .ToList();
            foreach (var i in valami)
            {
                WriteLine(i + "\tszóban megtalálható");
            }
            Console.WriteLine("-------------------");

            ReadKey();
        }
    }

    public static class Extenssion
    {
        //1. feladat véletlen számok generálása
        public static void GetRandomNumbers<T>(this List<int> list, int number)
        {
            var rnd = new Random();
            for (var i = 0; i < number; i++)
                list.Add(rnd.Next(-10, 10));
        }


        //2. feladat pozició keresése
        public static List<string> GetContainsPosition(this List<string> list, string search)
        {
            var local = new List<string>();
            if (search == null) return null;
            int count = 0;
            foreach (var word in list)
            {
                count++;
                if(word == search)
                    local.Add(count + ".\tszó a szövegben!");
            }
            return local;
        }

        //3. Feladat megkeresi a ?-es szavakat
        public static List<string> GetContainsWithQuestionMark(this List<string> list, string search)
        {
            var local = new List<string>();
            if (search == null) return null;
            int count = 0;
            var searchArray = search.ToCharArray();
            foreach (var word in list)
            {
                count++;
                var wordArray = word.ToCharArray();
                if (wordArray.Length == searchArray.Length)
                {
                    char[] questionMark = searchArray;
                    questionMark[GetQuestionMarkIndex(search)] = wordArray[GetQuestionMarkIndex(search)];
                    string valami = new string(questionMark);
                    if (word == valami)
                    {
                        local.Add(count + ".\t szó a szövegben!");
                    }
                }
            }
            return local;
        }

        //3. Feladat megkeresi hol van a ? jel
        public static int GetQuestionMarkIndex(this string word)
        {
            var local = word.ToCharArray();
            for (int i = 0; i < local.Length; i++)
            {
                if (local[i] == '?') return i;
            }
            return 0;
        }

       
    }

}
