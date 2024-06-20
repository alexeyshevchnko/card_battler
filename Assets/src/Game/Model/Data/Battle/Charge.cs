using Game.Model.Data.ReadOnly;
using UnityEngine;

namespace Game.Model.Data.Battle{

    [System.Serializable]
    public class Charge : ICharge {
        [SerializeField] private int _attack;
        public int Attack => _attack;

        public Charge() { }

        public Charge(int attack) {
            _attack = attack;
        }
    }

}

