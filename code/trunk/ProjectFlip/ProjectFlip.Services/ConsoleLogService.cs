using System;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.Services
{
    public class ConsoleLogService : ILogService 
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

  
}
