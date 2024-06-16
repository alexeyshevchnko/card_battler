using UnityEngine;

namespace Game.Data.Battle
{
    [System.Serializable]
    public class ChargeData : ICharge
    {
        [SerializeField] private int _attack;

        public int GetAttack() => _attack;
    }
}

