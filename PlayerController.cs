using SDL2;
using SDL2project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SDL2project
{
    class PlayerController : Controller
    {
        public PlayerController() { }
        ~PlayerController() { }

        public override void Update()
        {
            if (Input.GetKeyDown(SDL.SDL_Keycode.SDLK_w))
            {
                if (PredictMove(transform.x, transform.y - 1))
                {
                    transform.Translate(0, -1);
                }
            }
            if (Input.GetKeyDown(SDL.SDL_Keycode.SDLK_s))
            {
                if (PredictMove(transform.x, transform.y + 1))
                {
                    transform.Translate(0, 1);
                }
            }
            if (Input.GetKeyDown(SDL.SDL_Keycode.SDLK_a))
            {
                if (PredictMove(transform.x - 1, transform.y))
                {
                    transform.Translate(-1, 0);
                }
            }
            if (Input.GetKeyDown(SDL.SDL_Keycode.SDLK_d))
            {
                if (PredictMove(transform.x + 1, transform.y))
                {
                    transform.Translate(1, 0);
                }
            }
        }


    }
}
