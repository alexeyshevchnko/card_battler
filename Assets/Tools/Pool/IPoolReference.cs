using UnityEngine;

namespace Tools.Pool
{
    public interface IPoolReference<T> where T : Component
    {
        void SetParent(ObjectPool<T> parentPool);
    }
}