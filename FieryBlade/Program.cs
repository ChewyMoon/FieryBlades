using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FieryBlade.Util;

namespace FieryBlade
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Log("Booting up..");
            using (var game = new FieryBlade())
            {
                game.Run();
            }
        }
    }
}
