using Game.Model.Data.ReadOnly;
using Game.Model.Data.Type;
using UnityEngine;

namespace Game.Model.Data.Battle{

    [System.Serializable]
    public class Card : ICard {
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