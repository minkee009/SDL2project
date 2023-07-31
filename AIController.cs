using SDL2project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace SDL2project
{
    class AIController : Controller
    {
        public AIController() { }

        public AIController(Transform[] setChasePoint) 
        {
            for(int i = 0; i < patrolPoint.Length; i++)
            {
                patrolPoint[i] = setChasePoint[i];
            }
        }
        ~AIController() { }

        private int patrolIndex = 0;
        private int patrolCount = 0;
        private double actFrame = 0;

        public Transform[] patrolPoint = new Transform[3];

        public enum AIStates { Patrol = 0, Chase }

        AIStates currentState = AIStates.Patrol;

        public override void Update()
        {
            GameObject player = Engine.Find("player");

            if (player.transform.x == transform.x &&
                player.transform.y == transform.y)
            {
                Console.WriteLine("실패");
                Engine.Quit();
                return;
            }
            
            var currentTick = SDL.SDL_GetTicks();
            Random random = new Random();
            int direction = random.Next(0, 4);

            //transition
            if (Math.Abs(player.transform.x - transform.x) <= 3
                && Math.Abs(player.transform.y - transform.y) <= 3)
            {
                currentState = AIStates.Chase;
            }
            else
            {
                currentState = AIStates.Patrol;
            }

            //act

            actFrame += Engine.GetInstance().deltaTime;

            double setTimer = currentState == AIStates.Patrol ? 0.1 : 0.23;

            if(actFrame >= setTimer)
            {
                switch (currentState)
                {
                    case AIStates.Patrol:
                        var DirX = patrolPoint[patrolIndex].x - transform.x;
                        var DirY = patrolPoint[patrolIndex].y - transform.y;

                        var MoveX = DirX > 0 ? Math.Min(1, DirX) : Math.Max(-1, DirX);
                        var MoveY = DirY > 0 ? Math.Min(1, DirY) : Math.Max(-1, DirY);


                        if (Math.Abs(DirX) <= 0 && Math.Abs(DirY) <= 0)
                        {
                            transform.x = patrolPoint[patrolIndex].x;
                            transform.y = patrolPoint[patrolIndex].y;
                            patrolCount++;
                        }
                        else
                        {
                            if (Math.Abs(DirX) > Math.Abs(DirY))
                            {
                                if (PredictMove(transform.x + MoveX, transform.y))
                                {
                                    transform.Translate(MoveX, 0);
                                }
                            }
                            else
                            {
                                if (PredictMove(transform.x, transform.y + MoveY))
                                {
                                    transform.Translate(0, MoveY);
                                }
                            }
                        }
     
                        if (patrolCount > 12)
                        {
                            patrolIndex++;
                            patrolIndex %= 3;
                            patrolCount = 0;
                        }
                        break;
                    case AIStates.Chase:
                        patrolCount = 0;
                        var toPlayerDirX = player.transform.x - transform.x;
                        var toPlayerDirY = player.transform.y - transform.y;

                        var toPlayerMoveX = toPlayerDirX > 0 ? Math.Min(1, toPlayerDirX) : Math.Max(-1, toPlayerDirX);
                        var toPlayerMoveY = toPlayerDirY > 0 ? Math.Min(1, toPlayerDirY) : Math.Max(-1, toPlayerDirY);

                        if (Math.Abs(toPlayerDirX) > Math.Abs(toPlayerDirY))
                        {
                            if (PredictMove(transform.x + toPlayerMoveX, transform.y))
                            {
                                transform.Translate(toPlayerMoveX, 0);
                            }
                        }
                        else
                        {
                            if (PredictMove(transform.x , transform.y + toPlayerMoveY))
                            {
                                transform.Translate(0, toPlayerMoveY);
                            }
                        }
                        break;
                }
/*
                if (direction == 0)
                {
                    if (PredictMove(transform.x, transform.y - 1))
                    {
                        transform.Translate(0, -1);
                    }
                }
                if (direction == 1)
                {
                    if (PredictMove(transform.x, transform.y + 1))
                    {
                        transform.Translate(0, 1);
                    }
                }
                if (direction == 2)
                {
                    if (PredictMove(transform.x - 1, transform.y))
                    {
                        transform.Translate(-1, 0);
                    }
                }
                if (direction == 3)
                {
                    if (PredictMove(transform.x + 1, transform.y))
                    {
                        transform.Translate(1, 0);
                    }
                }*/
                actFrame = 0;
            }
            
        }
    }
}
