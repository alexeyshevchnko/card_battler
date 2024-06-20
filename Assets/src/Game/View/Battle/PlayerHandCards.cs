using System.Collections.Generic;
using Game.Model.Data.ReadOnly;
using UnityEngine;

namespace Game.View.Battle{

    public class PlayerHandCards : MonoBehaviour {
        [SerializeField] GameObject _playerCardPref;
        [SerializeField] Transform _rootHand;
        [SerializeField] Transform _rootDeck;
        [SerializeField] Transform _rootDiscardPile;

        private List<DeckCard> _deckRoot;
        private List<DeckCard> _handRoot;
        private List<DeckCard> _discardPileRoot;

        private int _handCartCount;
        private IReadOnlyList<ICardAction> _deckData;

        public void Init(IReadOnlyList<ICardAction> deckData,int handCartCount) {
            _handCartCount = handCartCount;
            _deckData = deckData;
            InitView();
        }

        private void InitView() {
            _deckRoot = new List<DeckCard>();
            _handRoot = new List<DeckCard>();
            _discardPileRoot = new List<DeckCard>();

            int num = 0;
            foreach (var cardData in _deckData) {
                var isHand = num < _handCartCount;
                var root = isHand ? _rootHand : _rootDeck;
                var go = Instantiate(_playerCardPref, root);
                var view = go.GetComponent<DeckCard>();
                view.Init(cardData, this);

                if (isHand) {
                    _handRoot.Add(view);
                }
                else {
                    _deckRoot.Add(view);
                }

                num++;
            }
        }

        internal void DiscardPile(DeckCard card) {
            _handRoot.Remove(card);
            _discardPileRoot.Add(card);
        }

        void ReshuffleDiscardPile() { }
    }
}
