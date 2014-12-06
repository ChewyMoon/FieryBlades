#region

using FieryBlade.Engine;
using FieryBlade.Scenes.MainMenu;
using FieryBlade.Util;
using SharpDX;
using SharpDX.Toolkit;

#endregion

namespace FieryBlade
{
    internal class FieryBlade : Game
    {
        private GraphicsDeviceManager _graphicsDeviceManager;

        public FieryBlade()
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
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

            SceneManager.Scene = new MainMenuScene();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color4.Black);

            var currentScene = SceneManager.Scene;
            currentScene.Draw();

            foreach (var entity in currentScene.Entities)
            {
                entity.Draw();
            }
        }

        protected override void Update(GameTime gameTime)
        {
            var currentScene = SceneManager.Scene;
            currentScene.Update();

            foreach (var entity in currentScene.Entities)
            {
                entity.Update();
            }
        }
    }
}