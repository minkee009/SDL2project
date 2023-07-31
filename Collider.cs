using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDL2project
{
    class Collider : Component
    {
        public Collider()
        {
            isCollide = true;
        }

        ~Collider()
        {

        }

        public bool isCollide;
    }
}
