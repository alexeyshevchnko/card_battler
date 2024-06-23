using Game.Model.Interface;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View.Battle{

    public class ChargeSlot : MonoBehaviour {
        [SerializeField] TMP_Text _countTxt;
        [SerializeField] Image _bg;
        [SerializeField] Image _icon;

        private ICharge _data;

        public void Init(ICharge data) {
            _data = data;
            UpdateView();
        }

        public void UpdateView() {
            if (_data == null) {
                Deactivate();
            }
            else {
                _countTxt.text = _data.Attack.ToString();
            }
        }

        public void Deactivate() {
            _countTxt.text = string.Empty;
            _bg.color = Color.gray;
            _icon.gameObject.SetActive(false);
        }
    }

}
