using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieryBlade.Engine
{
    public interface IEntity
    {
        void Update();
        void Draw();
        void Dispose();
    }
}
