using System;
using SDL2;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace SDL2project
{
    class MeshRenderer : Component
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;

        //SDL.SDL_Color colorKey;
        public bool isSprite;

        public string textureName;
        IntPtr mySurface;
        IntPtr myTexture;

        public int SpirteSize = 40;

        protected MeshFilter meshFilter;

        public int indexX = 0;
        public int indexY = 3;

        double animationTimer = 0;

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
        public MeshRenderer(byte inR, byte inG, byte inB, byte inA, string inTextureName, bool inIsSprite = false)
        {
            R = inR;
            G = inG;
            B = inB;
            A = inA;
            textureName = inTextureName;
            isSprite = inIsSprite;

            //릴리즈할 땐 사용하지않기 
            //mySurface = SDL.SDL_LoadBMP("Data/" + textureName); 로 변경
            string projectFolder = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;

            unsafe
            { 
                mySurface = SDL.SDL_LoadBMP(projectFolder + "/Data/" + textureName);
                SDL.SDL_Surface* surface = (SDL.SDL_Surface*)mySurface;
                if (!isSprite)
                {
                    SDL.SDL_SetColorKey(mySurface, 1,
                        SDL.SDL_MapRGB(surface->format, 255, 255, 255));
                }
                else
                {
                    SDL.SDL_SetColorKey(mySurface, 1,
                        SDL.SDL_MapRGB(surface->format, 255, 0, 255));
                }
                
                myTexture = SDL.SDL_CreateTextureFromSurface(Engine.GetInstance().myRenderer, mySurface);
            }
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

            unsafe
            {
                SDL.SDL_Rect source = new SDL.SDL_Rect();
                SDL.SDL_Surface* surface = (SDL.SDL_Surface*)mySurface;

                if (!isSprite)
                {
                    source.x = 0;
                    source.y = 0;
                    source.w = surface->w;
                    source.h = surface->h;
                }
                else
                {
                    int sizeX = surface->w / 5;
                    int sizeY = surface->h / 5;

                    source.x = indexX * sizeX;
                    source.y = indexY * sizeY;
                    source.w = sizeX;
                    source.h = sizeY;

                    animationTimer += Engine.GetInstance().deltaTime;
                  
                    if (animationTimer > 0.1)
                    {
                        indexX++;
                        indexX %= 5;

                        animationTimer = 0;
                    }
                    
                }

                SDL.SDL_Rect destination = new SDL.SDL_Rect();
                destination.x = transform.x * SpirteSize;
                destination.y = transform.y * SpirteSize;
                destination.w = SpirteSize;
                destination.h = SpirteSize;

                SDL.SDL_RenderCopy(Engine.GetInstance().myRenderer,
                    myTexture,
                    ref source,
                    ref destination);
            }

            //Animation

        }
    }
}
