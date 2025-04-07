using UnityEngine;
using System.Collections.Generic;

namespace YaraTools.Patterns.Creational
{
    public class objectPooling <T> where T : MonoBehaviour
    {
        private List<T> pool = new List<T>();
        private T prefab;

        public objectPooling(T prefab, int quantity = 1)
        {
            this.prefab = prefab;
            prewarm(quantity);
        }
        
        public T addInstance()
        {
            T x = Object.Instantiate(prefab);
            pool.Add(x);

            x.gameObject.SetActive(false);
            return x;
        }

        public void addInstance(int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                T x = Object.Instantiate(prefab);
                x.gameObject.SetActive(false);
                pool.Add(x);
            }
        }

        public T pullInstance(Vector3 pos, Quaternion rot)
        {
            T x = null;

            for (int i = 0; i < pool.Count; i++)
            {
                if (!pool[i].gameObject.activeSelf)
                {
                    x = pool[i];
                    x.transform.position = pos;
                    x.transform.rotation = rot;
                    x.gameObject.SetActive(true);
                    return x;
                }
            }

            if (pool.Count == 0) { return null; }
            
            x = addInstance();
            x.transform.position = pos;
            x.transform.rotation = rot;
            x.gameObject.SetActive(true);
            return x;
        }

        public int getActiveCount()
        {
            int count = 0;
            foreach (T i in pool)
            { if (i.gameObject.activeSelf) { count++; } }

            return count;
        }

        public int getInactiveCount()
        {
            int count = 0;
            foreach (T i in pool)
            { if (!i.gameObject.activeSelf) { count++; } }

            return count;
        }

        public void clearPool()
        {
            for (int i = 0; i < pool.Count; i++)
            { MonoBehaviour.Destroy(pool[i].gameObject); }

            pool.Clear();
        }

        public void deactivateAll()
        {
            for (int i = 0; i < pool.Count; i++)
            { pool[i].gameObject.SetActive(false); }
        }

        public void prewarm(int count)
        {
            for (int i = 0; i < count; i++)
            { addInstance(); }
        }
    }

}