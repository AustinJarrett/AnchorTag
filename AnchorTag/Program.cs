using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AnchorTag
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string str = File.ReadAllText("input.txt");
            File.WriteAllText("output.txt", "");

            while (str.IndexOf("http") != -1)
            {
                string url = getURL(str);
                string text = getText(url);
                string output = string.Format("<a href=\"{0}\"> {1} <\\a> ", url, text);
                using (StreamWriter sw = File.AppendText("output.txt"))
                {
                    sw.WriteLine(output);
                }
                    
                str = str.Substring(str.IndexOf("http")+1);
            }
            
        }

        private static string getText(string url)
        {
            string text = url;
            int index = 0;
            int check = 0;

            if (text.IndexOf("facebook") != -1 || text.IndexOf("twitter") != -1 || text.IndexOf("instagram") != -1)
            {
                text = text.Substring(text.IndexOf("www"));
                text = text.Substring(4);
                text = string.Format("Find us on {0}", text);
                return text;
            }
            
            while (check != -1)
            {
                index = check;
                index++;
                if (index != text.Length)
                {
                    text = text.Substring(index);
                }
                else
                {
                    text = text.Substring(0, text.Length - 1);
                    break;
                }
                
                check = text.IndexOf("/");
            }
            if (text.IndexOf("www") != -1)
            {
                text = text.Substring(4);
                text = string.Format("Find us on {0}", text);
            }

            return UppercaseWords(cleanText(text));
        }

        private static string getURL(string str)
        {
            string url = "";
            int index = str.IndexOf("http");
            url = str.Substring(index);
            if (url.IndexOf(" ") < url.IndexOf("\n") && url.IndexOf(" ") != -1)
            {
                url = url.Substring(0, url.IndexOf(" "));
            }
            if (url.IndexOf("\n") != -1)
            {
                url = url.Substring(0, url.IndexOf("\n"));
            }
            else
            {
                url = url.Substring(0);
            }
            

            
            return url;
        }

        private static string cleanText(string input)
        {
            if (input == null)
            {
                return "none";
            }
            else
            {
                return Regex.Replace(input, "-", " ");
            }

        }

        static string UppercaseWords(string value)
        {
            char[] array = value.ToCharArray();
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }
    }
}
