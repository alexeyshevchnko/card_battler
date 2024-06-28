using System.Collections.Generic;
using Game.Model.Interface;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View.Battle {
    public class HandCard : MonoBehaviour, ISelectedCard {
        public int Index => _index;
        public ICardAction Data => _data;
        public Transform myTrans => transform;

        [SerializeField] protected Transform _root;
        [SerializeField] TMP_Text _effectValTxt;
        [SerializeField] TMP_Text _nameTxt;
        [SerializeField] List<Image> _starsIcons;
        [SerializeField] TMP_Text _lvlTxt;

        [SerializeField] Image _effectIon;
        [SerializeField] private Sprite _effectHealingSprite;
        [SerializeField] private Sprite _effectAttackSprite;

        protected ICardAction _data;
        private int _index;

        protected virtual void Awake() { }

        public void Init(ICardAction data, int index) {
            _data = data;
            UpdateView();
        }

        private void UpdateView() {
            var effect = _data.FirstEffect;
            _effectIon.sprite = effect.EffectType == Model.Type.EffectType.Healing
                ? _effectHealingSprite
                : _effectAttackSprite;

            _effectValTxt.text = effect.ValueText;
            _nameTxt.text = _data.Name;
            _lvlTxt.text = _data.Level.ToString();

            for (int i = 0; i < _starsIcons.Count; i++) {
                var isEnable = i <= _data.Stars;
                _starsIcons[i].enabled = isEnable;
            }
        }

        internal void SetRootWorldPos(Vector3 pos) {
            _root.position = pos;
        }
    }

}
