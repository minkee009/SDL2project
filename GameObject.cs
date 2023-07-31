using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDL2project
{
    /// <summary>
    /// 자료관리, 이름에 대한 책임(Entity), 컴포넌트 검색
    /// </summary>
    class GameObject
    {
        public GameObject()
        {
            components = new List<Component>();
            name = String.Empty;
            transform = new Transform();
            AddComponent(transform);
        }
        ~GameObject()
        {
            components.Clear();
        }

        public List<Component> components;
        public string name;
        public Transform transform;
        public void AddComponent(Component newComponent)
        {
            newComponent.transform = transform;
            newComponent.gameObject = this;
            components.Add(newComponent);
        }

        public void RemoveComponent(Component removeComponent)
        {
            components.Remove(removeComponent);
        }

    }
}
