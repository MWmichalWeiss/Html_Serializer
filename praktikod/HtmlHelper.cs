using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace praktikod
{
    public class HtmlHelper
    {

        private readonly static HtmlHelper _instance=new HtmlHelper();

        public static HtmlHelper Instance => _instance;

        //המערך של כל סוגי התגיות
        public string[] AllTags { get; set; }

        //מערך של החד תגיות
        public string[] OneTag { get; set; }
        private HtmlHelper()
        {

            // קרא את קובץ ה-JSON
            string jsonText1 = File.ReadAllText("jsonFiles/AllTags.json");
            string jsonText2 = File.ReadAllText("jsonFiles/oneTags.json");


            // המר את טקסט ה-JSON למערך String
            AllTags = JsonSerializer.Deserialize<string[]>(jsonText1);
            OneTag = JsonSerializer.Deserialize<string[]>(jsonText2);

           

        }
    }
}
