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

        public void Dispose()
        {
        }

        public void Load()
        {
        }

        public void Update()
        {
        }
    }
}