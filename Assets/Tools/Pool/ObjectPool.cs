using UnityEngine;
using System.Collections.Generic;
using System;

namespace Tools.Pool
{
    public class ObjectPool<T> where T : Component
    {
        [SerializeField] protected T _prefab;
        [SerializeField] protected bool _allowResize = true;

        private LinkedList<T> _active;
        private Queue<T> _hidden;
        private Transform _parentTransform;

        public IEnumerable<T> ActiveEnum => _active;

        public ObjectPool<T> Configure(T prefab, bool allowResize)
        {
            _prefab = prefab;
            _allowResize = allowResize;

            return this;
        }

        public ObjectPool<T> Initialize(int count, Transform parent)
        {
            _active = new LinkedList<T>();
            _hidden = new Queue<T>();
            _parentTransform = parent;

            for (int i = 0; i < count; i++)
            {
                T item = CreateOne();
                item.gameObject.SetActive(false);
                _hidden.Enqueue(item);
            }

            return this;
        }

        public void ForeachPooled(Action<T> action)
        {
            foreach (T item in _hidden)
                action(item);
        }

        public T GetOne()
        {
            T result = GetNext();
            while (!result)
            {
                if (result == null)
                    Debug.LogWarning("Error, pulled object was destroyed");
                else
                    Debug.LogWarning("Error, pulled object was destroyed: " + result.name, result.gameObject);
                if (_hidden.Count > 0 || _allowResize)
                    result = GetNext();
                else
                    Debug.LogError("Error, no any objects in pull and nothing will be shown.");
            }

            result.gameObject.SetActive(true);
            return result;
        }

        public T GetOne(Vector3 localPosition, Quaternion localRotation)
        {
            T result = GetNext();

            result.transform.localPosition = localPosition;
            result.transform.localRotation = localRotation;
            result.gameObject.SetActive(true);
            return result;
        }

        private T GetNext()
        {
            T result;
            if (_hidden.Count > 0)
            {
                result = _hidden.Dequeue();
            }
            else if (_allowResize)
            {
                result = CreateOne();
            }
            else
            {
                result = _active.First.Value;
                _active.RemoveFirst();
                result.gameObject.SetActive(false);
            }
            _active.AddLast(result);

            return result;
        }

        public void Return(T toRemove)
        {
            toRemove.gameObject.SetActive(false);
            _active.Remove(toRemove);
            _hidden.Enqueue(toRemove);
        }

        public void ReturnAll()
        {
            while (_active.Count > 0)
            {
                Return(_active.Last.Value);
            }
        }

        private T CreateOne()
        {
            T item = MonoBehaviour.Instantiate(_prefab, _parentTransform);
            if (item is IPoolReference<T>)
                (item as IPoolReference<T>).SetParent(this);

            return item;
        }

        public bool IsSame(T otherPrefab)
        {
            return _prefab == otherPrefab;
        }
    }
}