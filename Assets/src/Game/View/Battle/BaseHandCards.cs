using System.Collections;
using System.Collections.Generic;
using Game.Model.Interface;
using UnityEngine;

namespace Game.View.Battle {

    public class BaseHandCards : MonoBehaviour {
        [SerializeField] private GameObject _cardPref;
        [SerializeField] private Transform _root;

        private IReadOnlyList<ICardAction> _data;
        private List<HandCard> _cards;

        public void Init(IReadOnlyList<ICardAction> data) {
            _data = data;
            StartInit();
        }

        protected virtual void StartInit() {
            ResetView();
        }

        protected void CreateCard() {
            _cards = new List<HandCard>();
            foreach (var cardData in _data)
            {
                var go = Instantiate(_cardPref, _root);
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
    }

}
