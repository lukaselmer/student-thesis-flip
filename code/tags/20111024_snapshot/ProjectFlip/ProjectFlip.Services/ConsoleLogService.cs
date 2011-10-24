using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
