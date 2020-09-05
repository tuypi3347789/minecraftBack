using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using Google.Protobuf.Reflection;
using Newtonsoft.Json;
using System.Diagnostics;

namespace WebApplication1.Model
{
    class whiteListAdd
    {
        public static void Main(string uuid, string name)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            //是否顯示視窗
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            process.StandardInput.WriteLine(@"cd bin/Debug/netcoreapp3.1");
            process.StandardInput.WriteLine(@"py dumpJson.py " + uuid +" "+ name + "");

            //讀取 命令提示字元 上內容
            while (!process.StandardOutput.EndOfStream)
            {
                Console.WriteLine(process.StandardOutput.ReadLine());
            }

            process.WaitForExit();
            process.Close();
        }
    }
}
