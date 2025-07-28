using UnityEngine;
using System.Collections.Generic;

namespace YaraTools.Patterns.Creational
{
    public class objectPooling <T> where T : MonoBehaviour
    {
        private List<T> inactivePool = new List<T>();
        private List<T> activePool = new List<T>();
        private T prefab;

        public objectPooling(T prefab, int quantity = 1)
        {
            this.prefab = prefab;
            prewarm(quantity);
        }

        public T addInstance()
        {
            T x = Object.Instantiate(prefab);
            inactivePool.Add(x);

            x.gameObject.SetActive(false);
            return x;
        }

        public void addInstance(int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                T x = Object.Instantiate(prefab);
                x.gameObject.SetActive(false);
                inactivePool.Add(x);
            }
        }

        public T pullInstance(Vector3 pos, Quaternion rot)
        {
            T x = null;

            if (inactivePool.Count > 0)
            {
                x = inactivePool[0];
                inactivePool.RemoveAt(0);
            }
            else
            {
                x = addInstance();
                inactivePool.Remove(x);
            }

            activePool.Add(x);
            x.transform.position = pos;
            x.transform.rotation = rot;
            x.gameObject.SetActive(true);
            return x;
        }

        public void deactivateInstance(T instance)
        {
            instance.gameObject.SetActive(false);
            activePool.Remove(instance);
            inactivePool.Add(instance);
        }

        public int getActiveCount()
        {
            return activePool.Count;
        }

        public T[] getActive()
        {
            return activePool.ToArray();
        }

        public int getInactiveCount()
        {
            return inactivePool.Count;
        }

        public T[] getInactive()
        {
            return inactivePool.ToArray();
        }

        public void clearPool()
        {
            foreach (T instance in activePool)
            {
                MonoBehaviour.Destroy(instance.gameObject);
            }
            foreach (T instance in inactivePool)
            {
                MonoBehaviour.Destroy(instance.gameObject);
            }

            activePool.Clear();
            inactivePool.Clear();
        }

        public void deactivateAll()
        {
            foreach (T instance in activePool)
            {
                instance.gameObject.SetActive(false);
                inactivePool.Add(instance);
            }
            activePool.Clear();
        }

        public void prewarm(int count)
        {
            for (int i = 0; i < count; i++)
            { addInstance(); }
        }
    }

}