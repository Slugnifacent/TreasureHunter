using System;

namespace TreasureHunter
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (TreasureHunter game = new TreasureHunter())
            {
                game.Run();
            }
        }
    }
#endif
}

