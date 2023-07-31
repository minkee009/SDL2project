using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SDL2;
namespace SDL2project
{
    class Input
    {
        public Input()
        {
        }

        ~Input()
        {
        }

        public static ConsoleKey key;

        public static bool GetKeyDown(ConsoleKey checkKey)
        {
            if (checkKey == key)
            {
                return true;
            }

            return false;
        }
        public static bool GetKeyDown(SDL.SDL_Keycode keycode)
        {
            if (Engine.GetInstance().myEvent.key.keysym.sym == keycode)
            {
                return true;
            }

            return false;
        }
    }
}
