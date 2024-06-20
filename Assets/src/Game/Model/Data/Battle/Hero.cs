using System.Collections.Generic;
using Game.Model.Data.ReadOnly;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Model.Data.Battle{

    [System.Serializable]
    public class Hero : Card, IHero {
        [SerializeField] List<Charge> _charges;
        [SerializeField] protected int _health;
        [SerializeField] protected int _fullAttack;

        protected event UnityAction<int> ChangedFullAttackEvent;
        public IReadOnlyList<ICharge> Charges => _charges;
        public int Health => _health;
        public int FullAttack => _fullAttack;

        public Hero() {
            ChangedFullAttackEvent += OnChangedFullAttack;
        }

        ~Hero() {
            ChangedFullAttackEvent -= OnChangedFullAttack;
        }

        private void OnChangedFullAttack(int val) {
            var countSlots = Random.Range(1, 6);
            _charges = GenerateChargesList(val, countSlots);
        }

        public void SetJson(string val) {
            Hero temp = JsonUtility.FromJson<Hero>(val);
            _name = temp._name;
            _stars = temp._stars;
            _level = temp._level;
            _health = temp._health;
            _fullAttack = temp._fullAttack;
            _cardType = temp._cardType;
            _cardMechanicType = temp._cardMechanicType;

            ChangedFullAttackEvent?.Invoke(_fullAttack);
        }

        public void ResetFullAttack(int val) {
            _fullAttack = val;
            ChangedFullAttackEvent?.Invoke(_fullAttack);
        }

        public List<Charge> GenerateChargesList(int fullAttack, int countSlots) {
            var sign = (int) Mathf.Sign(fullAttack);
            fullAttack = Mathf.Abs(fullAttack);

            List<Charge> charges = new List<Charge>();
            int initialAttack = fullAttack / countSlots;
            int remainder = fullAttack % countSlots;

            for (int i = 0; i < countSlots; i++) {
                int attackToAdd = initialAttack + (i == 0 ? remainder : 0);
                charges.Add(new Charge(attackToAdd * sign));
            }

            return charges;
        }
    }

}
