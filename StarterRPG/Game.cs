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
        int mPlayerX = 0;
        int mPlayerY = 0;

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
            Console.WriteLine("-------");
            for(int i = 0; i < 5; i++) 
            {
                StringBuilder line = new StringBuilder("|     |");
                if(i == mPlayerY)
                {
                    line[1+mPlayerX] = '☺';
                }
                Console.WriteLine(line);
            }
            Console.WriteLine("-------");
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

                case ConsoleKey.W:
                    mPlayerY--;
                    break;
                case ConsoleKey.A:
                    mPlayerX--;
                    break;
                case ConsoleKey.S:
                    mPlayerY++;
                    break;
                case ConsoleKey.D:
                    mPlayerX++;
                    break;
            }

            if(mPlayerX < 0)
            {
                mPlayerX = 0;
            }
            if(mPlayerX > 4)
            {
                mPlayerX = 4;
            }
            if (mPlayerY < 0)
            {
                mPlayerY = 0;
            }
            if (mPlayerY > 4)
            {
                mPlayerY = 4;
            }
        }
    }
}
