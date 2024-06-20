using System.Collections.Generic;
using System.Linq;
using Game.Model.Data;
using Game.Model.Interface;
using UnityEngine;

namespace Game.Model.Manager{

    [System.Serializable]
    public class ActionCards : ISerialization, IActionList {
        [SerializeField] List<CardAction> _cardAction;

        public IReadOnlyList<ICardAction> CardsData => _cardAction;
        public List<ICardAction> CardsReadList => _cardAction.Cast<ICardAction>().ToList();

        internal List<CardAction> CardsList => _cardAction;

        public ActionCards(List<ICardAction> data) {
            _cardAction = ConvertToCardActions(data);
        }

        public ActionCards(List<CardAction> data) {
            _cardAction = data;
        }

        private List<CardAction> ConvertToCardActions(List<ICardAction> data) {
            var cardActions = new List<CardAction>();
            foreach (var item in data) {
                if (item is CardAction cardAction) {
                    cardActions.Add(cardAction);
                }
            }

            return cardActions;
        }

        public string GetJson() {
            return JsonUtility.ToJson(this);
        }
    }

}
