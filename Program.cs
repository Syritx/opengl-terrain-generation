using System;
using OpenTK;

namespace terrain_generation
{
    class MainClass
    {
        static Game game;
        public static void Main(string[] args) {
            game = new Game(1000, 720);
        }
    }
}
