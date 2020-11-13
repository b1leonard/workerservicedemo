using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using static System.Console;
using System.IO;

namespace WSDemo
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                while(IsFileThere())
                {    
                        WriteLine("A file is here!");
                        await Task.Delay(10000, stoppingToken);
                }
                WriteLine("A file is not here...  I will wait for one to show up");
                await Task.Delay(10000, stoppingToken);
            }
        }

        public static bool IsFileThere()
        {
            
            // for information about the Directory call below refer to https://dotnetfiddle.net/KyO37p
            if (Directory.EnumerateFiles("In/", "*.json").Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
