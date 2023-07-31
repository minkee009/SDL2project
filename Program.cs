using SDL2;
using SDL2project;
using System;
using static SDL2.SDL;

namespace SDL2project
{
    //class Program
    //{
    //    public static void Main(string[] args)
    //    {
    //        SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);

    //        IntPtr myWindow = SDL.SDL_CreateWindow("Game", 100, 100, 640, 480, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
    //        IntPtr myRenderer = SDL.SDL_CreateRenderer(myWindow, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED
    //            | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC
    //            | SDL.SDL_RendererFlags.SDL_RENDERER_TARGETTEXTURE);
    //        SDL.SDL_Event myEvent;
    //        bool isRunning = true;
    //        while(isRunning)
    //        {
    //            SDL.SDL_PollEvent(out myEvent);
    //            switch(myEvent.type)
    //            {
    //                case SDL.SDL_EventType.SDL_QUIT:
    //                    isRunning = false;
    //                    break;
    //            }

    //            //Clear Screen
    //            SDL.SDL_SetRenderDrawColor(myRenderer, 0, 0, 0, 0);
    //            SDL.SDL_RenderClear(myRenderer);

    //            //Fill Rect
    //            SDL.SDL_Rect myRect = new SDL.SDL_Rect();
    //            myRect.x = 100;
    //            myRect.y = 100;
    //            myRect.w = 100;
    //            myRect.h = 100;
    //            SDL.SDL_SetRenderDrawColor(myRenderer, 255, 0, 0, 0);
    //            SDL.SDL_RenderFillRect(myRenderer, ref myRect);

    //            //Present
    //            SDL.SDL_RenderPresent(myRenderer);
    //        }

    //        SDL.SDL_DestroyRenderer(myRenderer);
    //        SDL.SDL_DestroyWindow(myWindow);

    //        SDL.SDL_Quit();
    //    }
    //}


    class Program
    {
        static int[,] map = {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
            };
        static void Main(string[] args)
        {
            Engine myEngine = Engine.GetInstance();

            for (int y = 0; y < 10; ++y)
            {
                for (int x = 0; x < 10; ++x)
                {
                    if (map[y, x] == 1)
                    {
                        GameObject floor = new GameObject();
                        floor.name = "floor";
                        floor.transform.x = x;
                        floor.transform.y = y;
                        floor.AddComponent(new MeshFilter(' '));
                        floor.AddComponent(new MeshRenderer(255, 255, 255, 0));
                        myEngine.Instanciate(floor);

                        GameObject wall = new GameObject();
                        wall.name = "wall";
                        wall.transform.x = x;
                        wall.transform.y = y;
                        wall.AddComponent(new MeshFilter('*'));
                        wall.AddComponent(new MeshRenderer(0, 0, 255, 0));
                        wall.AddComponent(new Collider());
                        myEngine.Instanciate(wall);
                    }
                    else if (map[y, x] == 0)
                    {
                        GameObject floor = new GameObject();
                        floor.name = "floor";
                        floor.transform.x = x;
                        floor.transform.y = y;
                        floor.AddComponent(new MeshFilter(' '));
                        floor.AddComponent(new MeshRenderer(255, 255, 255, 0));
                        myEngine.Instanciate(floor);
                    }
                }

            }

            GameObject goal = new GameObject();
            goal.name = "goal";
            goal.transform.x = 8;
            goal.transform.y = 8;
            goal.AddComponent(new MeshFilter('G'));
            goal.AddComponent(new MeshRenderer(0, 255, 255, 0));
            goal.AddComponent(new GoalIn());
            myEngine.Instanciate(goal);

            GameObject monster = new GameObject();
            monster.name = "monster";
            monster.AddComponent(new MeshFilter('M'));
            monster.AddComponent(new MeshRenderer(0, 255, 0, 255));
            Transform[] patrolPoints = new Transform[3];
            patrolPoints[0] = new Transform();
            patrolPoints[0].x = 6;
            patrolPoints[0].y = 2;
            patrolPoints[1] = new Transform();
            patrolPoints[1].x = 4;
            patrolPoints[1].y = 5;
            patrolPoints[2] = new Transform();
            patrolPoints[2].x = 7;
            patrolPoints[2].y = 7;
            monster.transform.x = 7;
            monster.transform.y = 7;
            monster.AddComponent(new AIController(patrolPoints));
            myEngine.Instanciate(monster);

            GameObject player = new GameObject();
            player.name = "player";
            player.transform.x = 1;
            player.transform.y = 1;
            player.AddComponent(new MeshFilter('P'));
            player.AddComponent(new MeshRenderer(255, 0, 0, 0));
            player.AddComponent(new PlayerController());
            myEngine.Instanciate(player);

            myEngine.Run();
        }
    }
}
