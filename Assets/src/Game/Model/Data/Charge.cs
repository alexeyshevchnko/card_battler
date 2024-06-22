    using Game.Model.Interface;
using UnityEngine;

namespace Game.Model.Data{

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