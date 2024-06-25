using TMPro;
using Tools.Pool;
using UnityEngine;

namespace Game.View.Battle
{
    public class ScreenText : MonoBehaviour, IPoolReference<ScreenText>
    {
        [SerializeField] private TextMeshPro text;
        [SerializeField] AnimationCurve _scaleCurve;
        [SerializeField] AnimationCurve _alphaCurve;
        [SerializeField] AnimationCurve _upCurve;
        [SerializeField] float _duration = 0.5f;

        private ObjectPool<ScreenText> _parentPool;
        private Transform _textTrans;
        private float _startTime;
        private float _removeTime;

        void Awake()
        {
            _textTrans = text.transform;
        }

        public void Init(int val, Color color)
        {
            text.color = color;
            text.text = val.ToString();
            _startTime = Time.time;
            _removeTime = Time.time + _duration;
            Update();
        }

        public void SetParent(ObjectPool<ScreenText> parentPool)
        {
            _parentPool = parentPool;
        }

        private void InPool()
        {
            _parentPool.Return(this);
        }

        void Update()
        {
            var time_0_1 = Mathf.InverseLerp(_startTime, _removeTime, Time.time); 

            var scale = _scaleCurve.Evaluate(time_0_1);
            var up = _upCurve.Evaluate(time_0_1);
            var alpha = _alphaCurve.Evaluate(time_0_1);


            text.alpha = alpha;
            _textTrans.localScale = new Vector3(scale, scale, scale);
            _textTrans.localPosition = new Vector3(0, up, 0);


            if (Time.time > _removeTime)
            {
                InPool();
            }
        }

    }
}