using App.Global.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Global.Translation
{
    public class Translation
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public int LangId { get; set; }
    }
    public class Translator
    {
        public static List<Translation> TranslationList { get; set; }

        public string Tranlate(string key)
        {
            return STranlate(key);
        }

        public static string STranlate(string key)
        {
            if (TranslationList.IsEmpty())
            {
                return key;
            }
            var t = TranslationList.FirstOrDefault(d => d.Key.ToLower() == key.ToLower());
            if (t.IsNull())
            {
                return key;
            }
            return t.Value;
        }
    }
}
