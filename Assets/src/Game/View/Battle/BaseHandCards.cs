using System.Collections.Generic;
using Game.Model.Interface;
using Game.View.Helper;
using UnityEngine;
using DG.Tweening;

namespace Game.View.Battle {

    public class BaseHandCards : MonoBehaviour {
        [SerializeField] private GameObject _cardPref;
        [SerializeField] private Transform _root;
        [SerializeField] private Transform _discardPile;
        [SerializeField] private Transform _deck;
        [SerializeField] private HorizontalLayout3D _horizontalLayout3D;

        private IReadOnlyList<ICardAction> _data;
        private List<HandCard> _cards;

        public void Init(IReadOnlyList<ICardAction> data) {
            _data = data;
            StartInit();
        }

        protected virtual void StartInit() {
            ResetView();
            PlayAnimationAllFromDeck();
        }

        protected void CreateCard() {
            _cards = new List<HandCard>();
            foreach (var cardData in _data)
            {
                var go = Instantiate(_cardPref, _root);
                go.transform.position = _deck.position;
                var view = go.GetComponent<HandCard>();
                view.Init(cardData);
                _cards.Add(view);
            }
        }

        protected virtual void ResetView() {
            for (int i = _root.childCount - 1; i >= 0; i--) {
                Destroy(_root.GetChild(i).gameObject);
            }

            CreateCard();
        }

        private void PlayAnimationAllFromDeck() {
            for (var i = 0; i < _cards.Count; i++) {
                var endPos = _horizontalLayout3D.GetSlotPosition(i);
                _cards[i].transform.DOMove(endPos, 1);
            }
        }
    }

}
