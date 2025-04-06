using UnityEngine;

namespace YaraTools.Patterns.Creational
{
    public abstract class MonoSingleton : MonoBehaviour
    {
        [SerializeField] private bool persistBetweenScenes = true;
        
        protected virtual void AwakeBase()
        {
            if (Application.isPlaying && persistBetweenScenes)
                DontDestroyOnLoad(gameObject);
        }

        protected virtual void OnDestroyBase() { }
    }

    public class MonoSingleton<T> : MonoSingleton where T : MonoSingleton<T>
    {
        private static T _instance;
        private static readonly object Lock = new();

        public static T Instance
        {
            get
            {
                lock (Lock)
                {
                    if (_instance == null)
                    {
                        _instance = FindFirstObjectByType<T>();
                        if (_instance == null)
                        {
                            var singletonObject = new GameObject(typeof(T).Name);
                            _instance = singletonObject.AddComponent<T>();
                        }
                    }
                    return _instance;
                }
            }
        }

        protected virtual void Awake()
        {
            base.AwakeBase();
            
            lock (Lock)
            {
                if (_instance != null && _instance != this)
                {
                    Destroy(gameObject);
                    return;
                }
                _instance = this as T;
            }
            
            Initialize();
        }

        protected virtual void Initialize() { }

        protected virtual void OnDestroy()
        {
            base.OnDestroyBase();
            if (_instance == this)
                _instance = null;
        }
    }
}