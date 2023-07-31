using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDL2project
{
    class MeshRenderer : Component
    {
        protected MeshFilter meshFilter;
        public MeshRenderer()
        {

        }
        ~MeshRenderer() { }

        public override void Start()
        {
            foreach (var component in gameObject.components)
            {
                if (component is MeshFilter)
                {
                    meshFilter = (component as MeshFilter);
                }
            }
        }

        public virtual void Render()
        {
            Console.SetCursorPosition(transform.x, transform.y);
            Console.WriteLine(meshFilter.Shape);
        }
    }
}
