using System.Collections.Generic;
using Game.Data.Battle.ReadOnly;
using UnityEngine;

namespace Game.Data.Battle {
    [System.Serializable]
    public class CardActionListData : ISerializationData, IReadOnlyCardActionList
    {
        [SerializeField] List<CardActionData> _cardAction;

        public IReadOnlyList<ICardActionData> CardActionList => _cardAction;

        public CardActionListData(List<CardActionData> data)
        {
            _cardAction = data;
        }

        public string GetJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
    
}
