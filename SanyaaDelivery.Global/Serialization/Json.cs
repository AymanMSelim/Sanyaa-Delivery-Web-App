using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Global.Serialization
{
    public static class Json
    {
        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T Deserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
