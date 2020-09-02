using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using Google.Protobuf.Reflection;
using Newtonsoft.Json;

namespace WebApplication1.Model
{
    class whiteListAdd
    {
        public static void Main(Guid uuid, string name)
        {
            string str = @"D:\server\whitelist.json";
            string josnString = File.ReadAllText(str, Encoding.Default);
            JArray jo = JArray.Parse(josnString);
            jo.Add(new JObject(
                new JProperty("uuid", uuid),
                new JProperty("name", name)
            ));
        }
    }
}
