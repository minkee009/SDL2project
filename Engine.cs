using SDL2;
using System;
using System.Collections.Generic;
using SDL2;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDL2project
{
    class Engine
    {
        protected Engine()
        {
            gameObjects = new List<GameObject>();
            isRunning = true;

            Init();
        }

        ~Engine()
        {
            Term();
        }

        public IntPtr myWindow;
        public IntPtr myRenderer;
        public SDL.SDL_Event myEvent;

        public void Init()
        {
            SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);

            myWindow = SDL.SDL_CreateWindow("Game", 100, 100, 640, 480, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
            myRenderer = SDL.SDL_CreateRenderer(myWindow, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED
                | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC
                | SDL.SDL_RendererFlags.SDL_RENDERER_TARGETTEXTURE);
        }

        public void Term()
        {
            SDL.SDL_DestroyRenderer(myRenderer);
            SDL.SDL_DestroyWindow(myWindow);

            SDL.SDL_Quit();
        }

        protected static Engine instance;

        public static Engine GetInstance()
        {
            if (instance == null)
            {
                instance = new Engine();
            }

            return instance;
        }

        UInt64 Now = SDL.SDL_GetPerformanceCounter();
        UInt64 Last = 0;
        public double deltaTime = 0;

        public void Instanciate(GameObject newGameObject)
        {
            gameObjects.Add(newGameObject);
        }

        public List<GameObject> GetAllGameObjects()
        {
            return gameObjects;
        }

        protected List<GameObject> gameObjects;

        public void Run()
        {
            GameLoop();
        }

        protected void GameLoop()
        {

            AllGameObjectinComponents_Start();
            while (isRunning)
            {
                SDL.SDL_PollEvent(out myEvent);
                switch (myEvent.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:
                        isRunning = false;
                        break;
                }

                AllGameObjectinComponents_Update();

                Last = Now;
                Now = SDL.SDL_GetPerformanceCounter();

                deltaTime = (double)((Now - Last) / (double)SDL.SDL_GetPerformanceFrequency());

                //Clear Screen
                SDL.SDL_SetRenderDrawColor(myRenderer, 0, 0, 0, 0);
                SDL.SDL_RenderClear(myRenderer);

                AllGameObjectinMeshRenderer_Render();

                //Present
                SDL.SDL_RenderPresent(myRenderer);
            }
        }

        protected void ProcessInput()
        {
            ConsoleKeyInfo info = Console.ReadKey();
            Input.key = info.Key;
        }

        protected void AllGameObjectinComponents_Start()
        {
            foreach (var gameObject in gameObjects)
            {
                foreach (var component in gameObject.components)
                {
                    component.Start();
                }
            }
        }

        protected void AllGameObjectinComponents_Update()
        {
            foreach (var gameObject in gameObjects)
            {
                foreach (var component in gameObject.components)
                {
                    component.Update();
                }
            }
        }

        protected void AllGameObjectinMeshRenderer_Render()
        {
            foreach (var gameObject in gameObjects)
            {
                foreach (var component in gameObject.components)
                {
                    bool result = component is MeshRenderer;
                    if (result)
                    {
                        MeshRenderer temp = component as MeshRenderer;
                        temp.Render();
                    }
                }
            }
        }

        protected static bool isRunning;

        public static void Quit()
        {
            isRunning = false;
        }

        public static GameObject Find(string name)
        {
            foreach (var gameObject in GetInstance().gameObjects)
            {
                if (gameObject.name.Equals(name))
                {
                    return gameObject;
                }
            }

            return null;
        }
    }
}
