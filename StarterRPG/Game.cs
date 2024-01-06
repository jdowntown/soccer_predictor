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
        Random rng = new Random();
        int mPlayerX;
        int mPlayerY;

        int mEnemyX;
        int mEnemyY;

        int mGold = 0;

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

            EnterMap();

            while (!mQuit)
            {
                Console.Clear();
                switch(mState)
                {
                    case State.Map:
                        MapDraw();
                        MapInput();
                        break;
                    case State.Battle:
                        BattleDraw();
                        BattleInput();
                        break;
                }
            }
            Console.Write("See you next time!");
        }

        void EnterMap()
        {
            mState = State.Map;
            mPlayerX = 0;
            mPlayerY = 0;

            mEnemyX = 4;
            mEnemyY = 4;
        }

        private void MapDraw()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("-------");
            for(int i = 0; i < 5; i++) 
            {
                StringBuilder line = new StringBuilder("|     |");
                if(i == mPlayerY)
                {
                    line[1+mPlayerX] = '☺';
                }
                if (i == mEnemyY)
                {
                    line[1 + mEnemyX] = '@';
                }
                Console.WriteLine(line);
            }
            Console.WriteLine("-------");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("You have " + mGold + " gold.");
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

            NormalizePosition(ref mPlayerX, ref mPlayerY);

            if(mPlayerX == mEnemyX && mPlayerY == mEnemyY) 
            {
                EnterBattle();
                return;
            }

            int move = rng.Next(0, 99);
            if(move < 15)
            {
                mEnemyX++;
            }
            else if (move < 30)
            {
                mEnemyX--;
            }
            else if (move < 45)
            {
                mEnemyY++;
            }
            else if (move < 60)
            {
                mEnemyY--;
            }

            NormalizePosition(ref mEnemyX, ref mEnemyY);

            if (mPlayerX == mEnemyX && mPlayerY == mEnemyY)
            {
                EnterBattle();
                return;
            }
        }

        int mPlayerHP;
        int mEnemyHP;

        private void EnterBattle()
        {
            mState = State.Battle;
            mPlayerHP = 5;
            mEnemyHP = 5;
        }

        private void BattleDraw()
        {
            Console.WriteLine("---BATTLE---");

            Console.WriteLine(mName + "'s HP: " +  mPlayerHP);
            Console.WriteLine("Dragon's HP: " + mEnemyHP);
        }

        private void BattleInput()
        {
            Console.WriteLine("");
            Console.Write("Space Attack!  Escape - Quit");
            ConsoleKeyInfo rawInput = Console.ReadKey();

            switch (rawInput.Key)
            {
                case ConsoleKey.Spacebar:
                    Attack();
                    break;

                case ConsoleKey.Escape:
                    mQuit = true;
                    break;
            }
        }

        private void Attack()
        {
            Console.WriteLine("");
            int val = rng.Next(1, 21);
            Console.WriteLine("Your Attack roll: " + val);
            if(val <= 9)
            {
                Console.WriteLine("Miss");
            }
            else
            {
                int dam = rng.Next(1, 3);
                Console.WriteLine("Hit for " + dam + " damage");
                mEnemyHP -= dam;
                if(mEnemyHP <= 0)
                {
                    Console.WriteLine("You win! +1 Gold.");
                    mGold++;
                    EnterMap();
                    Console.ReadKey();
                    return; // Enemy does not get to act
                }
            }

            val = rng.Next(1, 20);
            Console.WriteLine("Dragon Attack roll: " + val);
            if (val <= 9)
            {
                Console.WriteLine("Miss");
            }
            else
            {
                int dam = rng.Next(1, 3);
                Console.WriteLine("Hit for " + dam + " damage");
                mPlayerHP -= dam;
                if (mPlayerHP <= 0)
                {
                    Console.WriteLine("You lost");
                    EnterMap();
                }
            }
            Console.ReadKey();
        }

        void NormalizePosition(ref int x, ref int y)
        {
            if (x < 0)
            {
                x = 0;
            }
            if (x > 4)
            {
                x = 4;
            }
            if (y < 0)
            {
                y = 0;
            }
            if (y > 4)
            {
                y = 4;
            }
        }
    }
}
