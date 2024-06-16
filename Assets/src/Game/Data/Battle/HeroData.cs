using System.Collections.Generic;
using UnityEngine;

namespace Game.Data.Battle
{
    [System.Serializable]
    public class HeroData : IHero
    {
        [SerializeField] private string _name;
        [SerializeField] private int _stars;
        [SerializeField] private int _level;
        [SerializeField] private int _health;
        [SerializeField] private int _fullAttack;
        [SerializeField] HeroType _heroType;
        [SerializeField] List<ChargeData> _charges;

        public string GetName() => _name;
        public int GetLevel() => _level;
        public int GetStars() => _stars;

        public int GetHealth() => _health;
        public string GetFullAttack() => _fullAttack.ToString();
        public HeroType GetHeroType() => _heroType;
        //public ICharge[] GetCharges => _charges.Cast<ICharge>().ToArray();
    
        //TODO
        public ICharge[] GetCharges()
        {
            ICharge[] charges = new ICharge[_charges.Count];
            for (int i = 0; i < _charges.Count; i++)
            {
                charges[i] = _charges[i];
            }

            return charges;
        }
    }
}