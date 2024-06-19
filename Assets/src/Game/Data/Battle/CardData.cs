using Game.Data.Battle.ReadOnly;
using Game.Data.Types;
using UnityEngine;

namespace Game.Data.Battle {

    [System.Serializable]
    public class CardData : ICardData {
        [SerializeField] protected string _name;
        [SerializeField] protected int _stars;
        [SerializeField] protected int _level;
        [SerializeField] protected CardType _cardType;
        [SerializeField] protected CardMechanicType _cardMechanicType;

        public string Name => _name;
        public int Level => _level;
        public int Stars => _stars;
        public CardType CardType => _cardType;
        public CardMechanicType CardMechanicType => _cardMechanicType;
    }

}