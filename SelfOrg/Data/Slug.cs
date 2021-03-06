﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Text;
namespace SelfOrg.Data
{
    /// <summary>
    /// Класс для создания url-slugs - нормализованных транслитерированных ссылок
    /// </summary>

    public static class Slugifier 
    {
        public static string Transliterate(string text) //транслитерация по словарю с предварительным понижением регистра
        {
            Dictionary<string, string> transdict = new Dictionary<string, string>();  //словарь для перевода
        transdict.Add("Є", "EH");
            transdict.Add("І", "I");
            transdict.Add("і", "i");
            transdict.Add("№", "#");           
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
            //Каждый символ во входной строке заменяет по словарю - транслитерация
            foreach (KeyValuePair<string, string> key in transdict)
            {
                output = output.Replace(key.Key, key.Value);
            }
            return output;
        }
       
        public static string Slugify (string value)
        {
            //Замена пробелов
            value = Regex.Replace(value, @"\s", "-", RegexOptions.Compiled);

            //Удаление некорректных символов
            value = Regex.Replace(value, @"[^a-z0-9\s-_]", "", RegexOptions.Compiled);

            //Удаление - и _ из конца
            value = value.Trim('-', '_');

            //Удаление нескольких - и _ подряд
            value = Regex.Replace(value, @"([-_]){2,}", "$1", RegexOptions.Compiled);
            return value;
        }

       

           
       // }
    }
}

