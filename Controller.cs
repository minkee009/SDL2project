using SDL2project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDL2project
{
    class Controller : Component
    {
        public Controller()
        {
        }

        ~Controller()
        {
        }

        protected virtual bool PredictMove(int newX, int newY)
        {
            foreach (var gameObject in Engine.GetInstance().GetAllGameObjects())
            {
                //다음 갈곳 오브젝트 구하기
                if (gameObject.transform.x == newX &&
                    gameObject.transform.y == newY)
                {
                    //모든 컴포넌트 가져오기
                    foreach (var component in gameObject.components)
                    {
                        if (component is Collider)
                        {
                            Collider checkCopmponent = component as Collider;
                            if (checkCopmponent.isCollide)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }
    }
}
