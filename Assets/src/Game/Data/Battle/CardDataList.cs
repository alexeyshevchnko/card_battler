using System.Collections.Generic;
using UnityEngine;

namespace Game.Data.Battle {

    [System.Serializable]
    public class CardDataList {
        [SerializeField] List<CardData> _cards;

        public IReadOnlyList<CardData> Cards => _cards;

        public CardDataList(List<CardData> data) {
            _cards = data;
        }
    }

}
