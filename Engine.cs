using System;
using System.Collections.Generic;
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
        }

        ~Engine()
        {

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
                ProcessInput();
                AllGameObjectinComponents_Update();
                AllGameObjectinMeshRenderer_Render();
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
