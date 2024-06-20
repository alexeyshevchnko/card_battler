using System.Collections.Generic;
using Game.Model.Interface;
using UnityEngine;

namespace Game.Model.Data{
    [System.Serializable]
    public class CardAction : Card, ICardAction {
        [SerializeField] List<Effect> _effects;
        public IReadOnlyList<IEffect> Effects => _effects;
        public IEffect FirstEffect => _effects[0];

        public void SetJson(string val) {
            var temp = JsonUtility.FromJson<CardAction>(val);
            _name = temp._name;
            _stars = temp._stars;
            _level = temp._level;
            _cardType = temp._cardType;
            _cardMechanicType = temp._cardMechanicType;
            _effects = temp._effects;
        }
    }
}
