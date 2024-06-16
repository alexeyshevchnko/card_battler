using System.Collections.Generic;
using UnityEngine;

namespace Game.Data.Battle
{
    [System.Serializable]
    public class CardData : ICard
    {
        [SerializeField] private string _name;
        [SerializeField] private int _stars;
        [SerializeField] private int _level;
        [SerializeField] private int _health;
        [SerializeField] private int _fullAttack;
        [SerializeField] CardType _cardType;
        [SerializeField] List<ChargeData> _charges;
        [SerializeField] CardMechanicType _cardMechanicType;

        public string GetName() => _name;
        public int GetLevel() => _level;
        public int GetStars() => _stars;

        public int GetHealth() => _health;
        public string GetFullAttack() => _fullAttack.ToString();
        public CardType GetCardType() => _cardType;
        public CardMechanicType GetCardMechanicType() => _cardMechanicType;

        //TODO Size build
        //public ICharge[] GetCharges => _charges.Cast<ICharge>().ToArray();
        public List<ChargeData> GetCharges()
        {
            return _charges;
            // ICharge[] charges = new ICharge[_charges.Count];
            // for (int i = 0; i < _charges.Count; i++)
            // {
            //     charges[i] = _charges[i];
            // }
            //
            // return charges;
        }

        public void SetLhargeData(List<ChargeData> data)
        {
            _charges = data;
        }

    }
}