using System.Collections.Generic;
using Game.Model.Interface;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View.Battle {
    public class HandCard : MonoBehaviour {
        [SerializeField] protected Transform _root;
        [SerializeField] TMP_Text _effectValTxt;
        [SerializeField] TMP_Text _nameTxt;
        [SerializeField] List<Image> _starsIcons;
        [SerializeField] TMP_Text _lvlTxt;

        protected ICardAction _data;

        protected virtual void Awake() { }

        public void Init(ICardAction data) {
            _data = data;
            UpdateView();
        }

        private void UpdateView() {
            _effectValTxt.text = _data.FirstEffect.Value.ToString();
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
