using UnityEngine;

namespace Game.Data.Battle
{
    [System.Serializable]
    public class ChargeData : IChargeData
    {
        [SerializeField] private int _attack;

        public ChargeData() { }

        public ChargeData(int attack)
        {
            _attack = attack;
        }

        public int GetAttack() => _attack;
    }
}

