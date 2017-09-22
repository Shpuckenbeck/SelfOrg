using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Text;
namespace SelfOrg.Data
{
    

    public static class Slugifier //для создания url-slugs - нормализованных транслитерированных ссылок
    {
    

   
      
        public static string Transliterate(string text) //транслитерация по словарю с предварительным понижением регистра
        {
            Dictionary<string, string> transdict = new Dictionary<string, string>();  //словарь для перевода
        transdict.Add("Є", "EH");
            transdict.Add("І", "I");
            transdict.Add("і", "i");
            transdict.Add("№", "#");
            //transdict.Add("є", "eh");
            //transdict.Add("А", "A");
            //transdict.Add("Б", "B");
            //transdict.Add("В", "V");
            //transdict.Add("Г", "G");
            //transdict.Add("Д", "D");
            //transdict.Add("Е", "E");
            //transdict.Add("Ё", "JO");
            //transdict.Add("Ж", "ZH");
            //transdict.Add("З", "Z");
            //transdict.Add("И", "I");
            //transdict.Add("Й", "JJ");
            //transdict.Add("К", "K");
            //transdict.Add("Л", "L");
            //transdict.Add("М", "M");
            //transdict.Add("Н", "N");
            //transdict.Add("О", "O");
            //transdict.Add("П", "P");
            //transdict.Add("Р", "R");
            //transdict.Add("С", "S");
            //transdict.Add("Т", "T");
            //transdict.Add("У", "U");
            //transdict.Add("Ф", "F");
            //transdict.Add("Х", "KH");
            //transdict.Add("Ц", "C");
            //transdict.Add("Ч", "CH");
            //transdict.Add("Ш", "SH");
            //transdict.Add("Щ", "SHH");
            //transdict.Add("Ъ", "'");
            //transdict.Add("Ы", "Y");
            //transdict.Add("Ь", "");
            //transdict.Add("Э", "EH");
            //transdict.Add("Ю", "YU");
            //transdict.Add("Я", "YA");
            transdict.Add("а", "a");
            transdict.Add("б", "b");
            transdict.Add("в", "v");
            transdict.Add("г", "g");
            transdict.Add("д", "d");
            transdict.Add("е", "e");
            transdict.Add("ё", "e");
            transdict.Add("ж", "zh");
            transdict.Add("з", "z");
            transdict.Add("и", "i");
            transdict.Add("й", "j");
            transdict.Add("к", "k");
            transdict.Add("л", "l");
            transdict.Add("м", "m");
            transdict.Add("н", "n");
            transdict.Add("о", "o");
            transdict.Add("п", "p");
            transdict.Add("р", "r");
            transdict.Add("с", "s");
            transdict.Add("т", "t");
            transdict.Add("у", "u");

            transdict.Add("ф", "f");
            transdict.Add("х", "kh");
            transdict.Add("ц", "c");
            transdict.Add("ч", "ch");
            transdict.Add("ш", "sh");
            transdict.Add("щ", "sh");
            transdict.Add("ъ", "");
            transdict.Add("ы", "y");
            transdict.Add("ь", "");
            transdict.Add("э", "eh");
            transdict.Add("ю", "yu");
            transdict.Add("я", "ya");
            text = text.ToLowerInvariant();
            string output = text;         
            foreach (KeyValuePair<string, string> key in transdict)
            {
                output = output.Replace(key.Key, key.Value);
            }
            return output;
        }
       
        public static string Slugify (string value)
        {
            //Replace spaces
            value = Regex.Replace(value, @"\s", "-", RegexOptions.Compiled);

            //Remove invalid chars
            value = Regex.Replace(value, @"[^a-z0-9\s-_]", "", RegexOptions.Compiled);

            //Trim dashes from end
            value = value.Trim('-', '_');

            //Replace double occurences of - or _
            value = Regex.Replace(value, @"([-_]){2,}", "$1", RegexOptions.Compiled);
            return value;
        }

       //public Slugifier()
       // {
       //     transdict.Add("Є", "EH");
       //     transdict.Add("І", "I");
       //     transdict.Add("і", "i");
       //     transdict.Add("№", "#");
       //     //transdict.Add("є", "eh");
       //     //transdict.Add("А", "A");
       //     //transdict.Add("Б", "B");
       //     //transdict.Add("В", "V");
       //     //transdict.Add("Г", "G");
       //     //transdict.Add("Д", "D");
       //     //transdict.Add("Е", "E");
       //     //transdict.Add("Ё", "JO");
       //     //transdict.Add("Ж", "ZH");
       //     //transdict.Add("З", "Z");
       //     //transdict.Add("И", "I");
       //     //transdict.Add("Й", "JJ");
       //     //transdict.Add("К", "K");
       //     //transdict.Add("Л", "L");
       //     //transdict.Add("М", "M");
       //     //transdict.Add("Н", "N");
       //     //transdict.Add("О", "O");
       //     //transdict.Add("П", "P");
       //     //transdict.Add("Р", "R");
       //     //transdict.Add("С", "S");
       //     //transdict.Add("Т", "T");
       //     //transdict.Add("У", "U");
       //     //transdict.Add("Ф", "F");
       //     //transdict.Add("Х", "KH");
       //     //transdict.Add("Ц", "C");
       //     //transdict.Add("Ч", "CH");
       //     //transdict.Add("Ш", "SH");
       //     //transdict.Add("Щ", "SHH");
       //     //transdict.Add("Ъ", "'");
       //     //transdict.Add("Ы", "Y");
       //     //transdict.Add("Ь", "");
       //     //transdict.Add("Э", "EH");
       //     //transdict.Add("Ю", "YU");
       //     //transdict.Add("Я", "YA");
       //     transdict.Add("а", "a");
       //     transdict.Add("б", "b");
       //     transdict.Add("в", "v");
       //     transdict.Add("г", "g");
       //     transdict.Add("д", "d");
       //     transdict.Add("е", "e");
       //     transdict.Add("ё", "e");
       //     transdict.Add("ж", "zh");
       //     transdict.Add("з", "z");
       //     transdict.Add("и", "i");
       //     transdict.Add("й", "j");
       //     transdict.Add("к", "k");
       //     transdict.Add("л", "l");
       //     transdict.Add("м", "m");
       //     transdict.Add("н", "n");
       //     transdict.Add("о", "o");
       //     transdict.Add("п", "p");
       //     transdict.Add("р", "r");
       //     transdict.Add("с", "s");
       //     transdict.Add("т", "t");
       //     transdict.Add("у", "u");

       //     transdict.Add("ф", "f");
       //     transdict.Add("х", "kh");
       //     transdict.Add("ц", "c");
       //     transdict.Add("ч", "ch");
       //     transdict.Add("ш", "sh");
       //     transdict.Add("щ", "sh");
       //     transdict.Add("ъ", "");
       //     transdict.Add("ы", "y");
       //     transdict.Add("ь", "");
       //     transdict.Add("э", "eh");
       //     transdict.Add("ю", "yu");
       //     transdict.Add("я", "ya");

           
       // }
    }
}

