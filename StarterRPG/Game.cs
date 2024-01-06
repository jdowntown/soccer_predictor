using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StarterRPG
{
    internal class Game
    {
        string mName;
        bool mQuit = false;
        private enum State
        {
            Map,
            Battle,
            Menu
        };
        State mState = State.Map;

        public Game() { }
        public void Play()
        {
            Console.WriteLine("Welcome to SimpleRPG");
            Console.Write("Please Enter your Name:");
            mName = Console.ReadLine();

            while(!mQuit)
            {
                Console.Clear();
                switch(mState)
                {
                    case State.Map:
                        MapDraw();
                        MapInput();
                        break;
                }
            }
            Console.Write("See you next time!");
        }

        private void MapDraw()
        {

        }

        private void MapInput()
        {
            Console.WriteLine("");
            Console.Write("WASD to move, Escape - Quit");
            ConsoleKeyInfo rawInput = Console.ReadKey();

            switch(rawInput.Key)
            {
                case ConsoleKey.Escape:
                    mQuit = true;
                    break;
                
                
            }
        }
    }
}
