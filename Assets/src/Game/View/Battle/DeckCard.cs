using System.Collections;
using System.Collections.Generic;
using Game.Model.Interface;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View.Battle{

    public class DeckCard : MonoBehaviour {
        [SerializeField] TMP_Text _effectValTxt;
        [SerializeField] TMP_Text _nameTxt;
        [SerializeField] List<Image> _starsIcons;
        [SerializeField] TMP_Text _lvlTxt;
        private ICardAction _data;
        private PlayerHandCards _playerHandCards;

        public void Init(ICardAction data, PlayerHandCards playerHandCards) {
            _playerHandCards = playerHandCards;
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

        internal ICardAction GetData() {
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
