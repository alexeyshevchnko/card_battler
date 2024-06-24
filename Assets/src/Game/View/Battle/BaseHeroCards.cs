using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Model.Interface;
using Game.View.Helper;
using UnityEngine;

namespace Game.View.Battle {

    public class BaseHeroCards : MonoBehaviour {
        [SerializeField] private GameObject _cardPref;
        [SerializeField] private Transform _root;
        [SerializeField] private Transform _discardPile;
        [SerializeField] private Transform _deck;
        [SerializeField] private HorizontalLayout3D _horizontalLayout3D;
        [SerializeField] private Material _selectCardMat;
        [SerializeField] private Material _unselectCardMat;

        private IReadOnlyList<IHero> _data;
        private List<HeroCard> _cards;

        public void Init(IReadOnlyList<IHero> data) {
            _data = data;
            StartInit();
        }

        protected virtual void StartInit() {
            ResetView();
            PlayAnimationAllFromDeck();
        }

        protected void CreateCard() {
            _cards = new List<HeroCard>();
            foreach (var cardData in _data) {
                var go = Instantiate(_cardPref, _root);
                go.transform.position = _deck.position;
                var view = go.GetComponent<HeroCard>();
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

        internal void SelectEnableHero(ICardAction actionCardData) {
            foreach (var card in _cards) {
                card.TrySelect(actionCardData, _selectCardMat);
            }
        }

        internal void UnselectAllHero() {
            foreach (var card in _cards) {
                card.Unselect(_unselectCardMat);
            }
        }
    }

}
