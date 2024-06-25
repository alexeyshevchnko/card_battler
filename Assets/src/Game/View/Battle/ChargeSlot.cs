using Game.Model.Interface;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Game.View.Battle{

    public class ChargeSlot : MonoBehaviour {
        [SerializeField] TMP_Text _countTxt;
        [SerializeField] Image _bg;
        [SerializeField] Image _icon;

        private ICharge _data;

        internal void Init(ICharge data) {
            _data = data;
            UpdateView();
        }

        internal void UpdateView() {
            if (_data == null) {
                Deactivate();
            }
            else {
                _countTxt.text = _data.Attack.ToString();
            }
        }

        internal void Deactivate() {
            _countTxt.text = string.Empty;
            _bg.color = Color.gray;
            _icon.gameObject.SetActive(false);
        }

        internal void PlayAttackAnimation(float delay,  float dur = 0.2f) {
            var par = _icon.transform.parent;
            par.DOShakeScale(dur, 0.5f).SetDelay(delay);
            //_countTxt.transform.DOShakeScale(dur).SetDelay(delay);
        }
    }

}
