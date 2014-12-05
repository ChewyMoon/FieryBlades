using System;
using System.IO;
using FieryBlade.Engine;
using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;

namespace FieryBlade
{
    class FieryBlade : Game
    {
        private SpriteBatch spriteBatch;
        private Texture2D bundoofTexture2D;

        private GraphicsDeviceManager graphicsDeviceManager;

        public FieryBlade()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            bundoofTexture2D = Content.Load<Texture2D>("bundoof.png");
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color4.Black);

            spriteBatch.Begin();
            spriteBatch.Draw(bundoofTexture2D, new Vector2(10, 10), Color.White);
            spriteBatch.End();
        }
    }
}
