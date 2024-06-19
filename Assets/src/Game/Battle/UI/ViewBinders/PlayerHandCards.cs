using System.Collections.Generic;
using Game.Data.Battle.ReadOnly;
using UnityEngine;

namespace Game.Battle.UI.ViewBinders {

    public class PlayerHandCards : MonoBehaviour {
        [SerializeField] GameObject _playerCardPref;
        [SerializeField] Transform _rootHand;
        [SerializeField] Transform _rootDeck;
        [SerializeField] Transform _rootDiscardPile;

        private List<DeckCard> _deckCards;
        private List<DeckCard> _handCards;
        private List<DeckCard> _discardPile;

        private int _handCartCount;
        private List<ICardActionData> _deckData;

        public void Init(IReadOnlyCardActionList data, int handCartCount = 5) {
            _handCartCount = handCartCount;
            _deckData = data.CardsList;
            InitView();
        }

        private void InitView() {
            _deckCards = new List<DeckCard>();
            _handCards = new List<DeckCard>();
            _discardPile = new List<DeckCard>();

            int num = 0;
            foreach (var cardData in _deckData) {
                var isHand = num < _handCartCount;
                var root = isHand ? _rootHand : _rootDeck;
                var go = Instantiate(_playerCardPref, root);
                var view = go.GetComponent<DeckCard>();
                view.Init(cardData, this);

                if (isHand) {
                    _handCards.Add(view);
                }
                else {
                    _deckCards.Add(view);
                }

                num++;
            }
        }

        internal void DiscardPile(DeckCard card) {
            _handCards.Remove(card);
            _discardPile.Add(card);
        }

        void ReshuffleDiscardPile() { }
    }
}
