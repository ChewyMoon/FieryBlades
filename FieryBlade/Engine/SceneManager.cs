using FieryBlade.Util;

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
                Logger.Log("Scene being changed to " + value.GetType().Name);

                if (_scene != null)
                {
                    _scene.Dispose();
                    Logger.Log("Disposed " + _scene.GetType().Name);                    
                }
                    
                _scene = value;
                _scene.Load();
                Logger.Log(_scene.GetType().Name + " loaded");
            }
        }
    }
}