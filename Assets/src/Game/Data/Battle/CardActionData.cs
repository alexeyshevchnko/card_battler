using System.Collections.Generic;
using Game.Data.Battle.ReadOnly;
using UnityEngine;

namespace Game.Data.Battle {
    [System.Serializable]
    public class CardActionData : CardData, ICardActionData {
        [SerializeField] List<EffectData> _effects;
        public IReadOnlyList<IEffectData> Effects => _effects;
        public IEffectData FirstEffect => _effects[0];

        public void SetJson(string val) {
            var temp = JsonUtility.FromJson<CardActionData>(val);
            _name = temp._name;
            _stars = temp._stars;
            _level = temp._level;
            _cardType = temp._cardType;
            _cardMechanicType = temp._cardMechanicType;
            _effects = temp._effects;
        }
    }
}
