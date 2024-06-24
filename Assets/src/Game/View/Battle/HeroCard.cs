using Game.Model.Interface;
using TMPro;
using UnityEngine;

namespace Game.View.Battle{

    public abstract class  HeroCard : MonoBehaviour {
        [SerializeField] protected Renderer _renderer;
        [SerializeField] private TMP_Text _nameTxt;
        [SerializeField] private TMP_Text _hpTxt;
        [SerializeField] private ChargeSlots _chargeSlots;

        private IHero _data;

        public void Init(IHero data) {
            _data = data;
            UpdateView();
        }

        private void UpdateView() {
            _nameTxt.text = _data.Name;
            _hpTxt.text = _data.Health.ToString();
            _chargeSlots.Init(_data.Charges);
        }

        internal void Unselect(Material unselectMat) {
            _renderer.material = unselectMat;
        }

        internal abstract bool TrySelect(ICardAction actionCardData, Material selectMat);

    }

}
