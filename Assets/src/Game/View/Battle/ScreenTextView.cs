using Tools.Pool;
using UnityEngine;

namespace Game.View.Battle{
    
    public class ScreenTextView : MonoBehaviour
    {
        internal static ScreenTextView Instance { get; private set; }
        [System.Serializable]
        class TextPool : ObjectPool<ScreenText> { }
        [SerializeField] TextPool _pool;

        [SerializeField] private Color _color;
        
        //[SerializeField] private float _randomX = 0.1f;

        private Quaternion _spawnRot;

        void Awake()
        {
            Instance = this;
            _spawnRot = Camera.main.transform.rotation;
            _pool.Initialize(10, transform);
        }

        internal void ShowDamage(Vector3 pos, float damage)
        {
           // pos.x += Random.Range(0, _randomX) * (Random.Range(0, 100) > 50 ? -1 : 1);

            var damageText = _pool.GetOne(pos, _spawnRot);
            damageText.Init(Mathf.RoundToInt(damage), _color);
        }

    }

}
