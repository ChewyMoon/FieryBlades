namespace FieryBlade.Engine
{
    public class SceneManager
    {
        private static Scene _scene;

        public static Scene Scene
        {
            get { return _scene; }
            set
            {
                _scene.Dispose();
                _scene = value;
                _scene.Load();
            }
        }
    }
}