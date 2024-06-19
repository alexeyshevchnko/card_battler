using System.Collections.Generic;
using System.Linq;
using Game.Data.Base;
using Game.Data.Battle.ReadOnly;
using UnityEngine;

namespace Game.Data.Battle {

    [System.Serializable]
    public class CardActionListData : ISerializationData, IReadOnlyCardActionList {
        [SerializeField] List<CardActionData> _cardAction;

        public IReadOnlyList<ICardActionData> CardsData => _cardAction;
        public List<ICardActionData> CardsList => _cardAction.Cast<ICardActionData>().ToList();

        public CardActionListData(List<CardActionData> data) {
            _cardAction = data;
        }

        public string GetJson() {
            return JsonUtility.ToJson(this);
        }
    }

}
