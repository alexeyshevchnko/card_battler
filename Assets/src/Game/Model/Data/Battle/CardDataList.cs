using System.Collections.Generic;
using UnityEngine;

namespace Game.Model.Data.Battle{

    [System.Serializable]
    public class CardDataList {
        [SerializeField] List<Card> _cards;

        public IReadOnlyList<Card> Cards => _cards;

        public CardDataList(List<Card> data) {
            _cards = data;
        }
    }

}
