using Tools.Pool;
using UnityEngine;

namespace Game.View.Battle{
    
    public class ScreenTextView : MonoBehaviour
    {
        internal static ScreenTextView Instance { get; private set; }
        [System.Serializable]
        class TextPool : ObjectPool<ScreenText> { }
        [SerializeField] TextPool _pool;

        [SerializeField] private Color _colorDamage;
        [SerializeField] private Color _colorAdd;

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
            var damageText = _pool.GetOne(pos, _spawnRot);
            damageText.Init(Mathf.RoundToInt(damage), _colorDamage);
        }

        internal void ShowAddHp(Vector3 pos, float val)
        {
            var damageText = _pool.GetOne(pos, _spawnRot);
            damageText.Init(Mathf.RoundToInt(val), _colorAdd);
        }

    }

}
