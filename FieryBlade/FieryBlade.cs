using System;
using System.IO;
using FieryBlade.Engine;
using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Diagnostics;
using SharpDX.Toolkit.Graphics;
using Logger = FieryBlade.Util.Logger;

namespace FieryBlade
{
    class FieryBlade : Game
    {
       
        public static FieryBlade Instance { private set; get; }

        private GraphicsDeviceManager graphicsDeviceManager;

        public FieryBlade()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1024,
                PreferredBackBufferHeight = 768
            };
            Logger.Log("Created GDR.");

            Instance = this;
        }

        protected override void Initialize()
        {
            Content.RootDirectory = "Content";
            Logger.Log("Core Initialized");
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color4.Black);
        }
    }
}
