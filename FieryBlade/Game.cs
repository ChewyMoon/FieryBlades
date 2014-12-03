using System;
using FieryBlade.Engine;
using SFML.Graphics;
using SFML.Window;

namespace FieryBlade
{
    class Game
    {
        public static RenderWindow Window { private set; get; }
        public void Start()
        {
            Window = new RenderWindow(new VideoMode(1280, 768), "FieryBlade");
            
            while (Window.IsOpen())
            {
                Window.DispatchEvents();
                Window.Clear();

                var scene = SceneManager.Scene;
                if (scene == null)
                {
                    #if DEBUG
                        Console.WriteLine("[{0}] The scene is null!", DateTime.Now);
                    #endif
                    continue;
                }

                scene.Update();
                foreach (var entity in scene.Entities)
                {
                    entity.Update();
                    entity.Draw();
                }

                Window.Display();
            }
        }
    }
}
