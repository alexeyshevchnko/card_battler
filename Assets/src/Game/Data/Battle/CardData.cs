using Game.Data.Battle.ReadOnly;
using Game.Data.Types;
using UnityEngine; 

namespace Game.Data.Battle
{
    [System.Serializable]
    public class CardData : ICardData
    {
        [SerializeField] protected string _name;
        [SerializeField] protected int _stars;
        [SerializeField] protected int _level;
        [SerializeField] protected CardType _cardType;
        [SerializeField] protected CardMechanicType _cardMechanicType;

        public string GetName() => _name;
        public int GetLevel() => _level;
        public int GetStars() => _stars;
        public CardType GetCardType() => _cardType;
        public CardMechanicType GetCardMechanicType() => _cardMechanicType;
    }
}