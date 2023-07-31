using System;
using SDL2;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDL2project
{
    class MeshRenderer : Component
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;

        public int SpirteSize = 40;

        protected MeshFilter meshFilter;
        public MeshRenderer()
        {

        }
        public MeshRenderer(byte inR, byte inG, byte inB, byte inA)
        {
            R = inR;
            G = inG;
            B = inB;
            A = inA;
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
            //Console.SetCursorPosition(transform.x, transform.y);
            //Console.WriteLine(meshFilter.Shape);

            //Fill Rect
            SDL.SDL_Rect myRect = new SDL.SDL_Rect();
            myRect.x = transform.x * SpirteSize;
            myRect.y = transform.y * SpirteSize;
            myRect.w = SpirteSize;
            myRect.h = SpirteSize;
            SDL.SDL_SetRenderDrawColor(Engine.GetInstance().myRenderer, R, G, B, A);
            SDL.SDL_RenderFillRect(Engine.GetInstance().myRenderer, ref myRect);
        }
    }
}
