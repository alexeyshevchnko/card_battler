using System.Collections;
using System.Collections.Generic;
using Game.Data.Battle.ReadOnly;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Battle.UI.ViewBinders {

    public class DeckCard : MonoBehaviour {
        [SerializeField] Text _effectValTxt;
        [SerializeField] Text _nameTxt;
        [SerializeField] List<Image> _starsIcons;
        [SerializeField] Text _lvlTxt;
        private ICardActionData _data;
        private PlayerHandCards _playerHandCards;

        public void Init(ICardActionData data, PlayerHandCards playerHandCards) {
            _playerHandCards = playerHandCards;
            _data = data;
            UpdateView();
        }

        private void UpdateView() {
            _effectValTxt.text = _data.FirstEffect.Value.ToString();
            _nameTxt.text = _data.GetName();
            _lvlTxt.text = _data.GetLevel().ToString();

            for (int i = 0; i < _starsIcons.Count; i++) {
                var isEnable = i <= _data.GetStars();
                _starsIcons[i].enabled = isEnable;
            }
        }

        internal ICardActionData GetData() {
            return _data;
        }

        internal void MakeMove() {
            StartCoroutine(MakeMoveAnimation());
        }

        IEnumerator MakeMoveAnimation() {
            yield return new WaitForSeconds(1);
            _playerHandCards.DiscardPile(this);
        }
    }

}
