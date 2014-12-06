using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Toolkit.Graphics;

namespace FieryBlade.Util
{
    public static class TextureUtil
    {
        public static Rectangle GetRectangle(this Texture2D texture2D, Vector2 pos)
        {
            return new Rectangle((int) pos.X, (int) pos.Y, texture2D.Width, texture2D.Height);
        }

        public static Rectangle GetRectangle(this Texture2D texture2D, int x, int y)
        {
            return GetRectangle(texture2D, new Vector2(x, y));
        }
    }
}
