#region

using System.Collections.Generic;

#endregion

namespace FieryBlade.Engine
{
    public abstract class Scene
    {
        public List<IEntity> Entities;

        public Scene()
        {
            Entities = new List<IEntity>();
        }

        public virtual void Dispose()
        {
        }

        public virtual void Load()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Draw()
        {
        }
    }
}