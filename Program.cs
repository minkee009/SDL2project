using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDL2;
using System.Threading.Tasks;

namespace SDL2project
{
    class Program
    {
        static void Main(string[] args)
        {
            SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);

            IntPtr myWindow = SDL.SDL_CreateWindow("Game", 100, 100, 640, 480, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
            IntPtr myRenderer =  SDL.SDL_CreateRenderer(myWindow, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED 
                | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC
                | SDL.SDL_RendererFlags.SDL_RENDERER_TARGETTEXTURE);
            SDL.SDL_Event myEvent;
            bool isRunnig = true;
            while (isRunnig)
            {
                SDL.SDL_PollEvent(out myEvent);
                switch (myEvent.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:
                        isRunnig = false;
                        break;
                }

                //Command Queue
                SDL.SDL_SetRenderDrawColor(myRenderer, 0, 0, 0, 0);
                SDL.SDL_RenderClear(myRenderer);
                SDL.SDL_RenderPresent(myRenderer);
            }

            SDL.SDL_DestroyRenderer(myRenderer);
            SDL.SDL_DestroyWindow(myWindow);

            SDL.SDL_Quit();
        }
    }
}
