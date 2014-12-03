using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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
