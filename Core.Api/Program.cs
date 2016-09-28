﻿using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace MvcEfSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                        .UseKestrel()
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseIISIntegration()
                        .UseStartup<Startup>()
                        .UseUrls("http://127.0.0.1:3002")
                        .Build();

            host.Run();
        }
    }
}
