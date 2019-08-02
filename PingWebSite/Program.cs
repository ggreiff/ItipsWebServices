using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingWebSite
{
    public class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            p.Run();
        }

        public void Run()
        {
            var webClient = new System.Net.WebClient();
            foreach (var url in Properties.Settings.Default.URLS)
            {
               var webString =  webClient.DownloadString(url);
               System.Console.WriteLine(url);
            }
        }
    }
}
