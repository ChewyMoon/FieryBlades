#region

using FieryBlade.Util;

#endregion

namespace FieryBlade
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Logger.Log("Booting up..");
            using (var game = new FieryBlade())
            {
                game.Run();
            }
        }
    }
}