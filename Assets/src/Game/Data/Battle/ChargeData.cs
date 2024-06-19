using Game.Data.Battle.ReadOnly;
using UnityEngine;

namespace Game.Data.Battle {

    [System.Serializable]
    public class ChargeData : IChargeData {
        [SerializeField] private int _attack;
        public int Attack => _attack;

        public ChargeData() { }

        public ChargeData(int attack) {
            _attack = attack;
        }
    }

}

