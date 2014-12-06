#region

using FieryBlade.Util;
using SharpDX;
using SharpDX.Toolkit;

#endregion

namespace FieryBlade
{
    internal class FieryBlade : Game
    {
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

        public static FieryBlade Instance { private set; get; }

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