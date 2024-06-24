using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game.View.Battle {

    internal class AliveEntity : MonoBehaviour, IAliveEntity {
        [SerializeField] private float _maxHealth = _DefaultHealth;
        [SerializeField] private bool _debugMode = false;

        public float MaxHealth => _maxHealth;
        public float Health { get; private set; } = _DefaultHealth;
        public float HealthPercent => Health / MaxHealth * 100f;
        public bool IsAlive { get; private set; }
        public float Armor => _armor;

        public event UnityAction<Transform> DeathEvent;
        public event UnityAction AliveEvent;
        public event UnityAction<float, float> HealthUpdateEvent;
        public event UnityAction<float> DamageEvent;

        private const float _DefaultHealth = 100f;
        protected float _armor = 0;

        public void SetMaxHealth(float val) {
            float coef = val / _maxHealth - 1f;
            _maxHealth = val;
            float hpBonus = Health * coef;
            Health += hpBonus;

            HealthUpdateEvent?.Invoke(Health, -hpBonus);
        }

        public void SetArmor(float val) {
            _armor = val;
        }

        public virtual void ApplyDamage(float damage, Transform damageSource) {
            if (IsAlive) {
                damage *= (1 - _armor); // armor protection 
                Health = Mathf.Max(0, Health - damage);

                if (_debugMode)
                    Debug.Log($"{name} damage {damage} from {damageSource.name}", damageSource.gameObject);

                HealthUpdateEvent?.Invoke(Health, damage);
                DamageEvent?.Invoke(damage);
                if (Health <= 0f)
                    SetDead(killer: damageSource);
            }
        }

        public void Regeneration(float val) {
            float regenValue = MaxHealth * val;
            if (Health + regenValue > MaxHealth) {
                regenValue = MaxHealth - Health;
            }

            Health += regenValue;
            HealthUpdateEvent?.Invoke(Health, -regenValue);
        }

        public void SetAlive() {
            Health = _maxHealth;
            IsAlive = true;
            AliveEvent?.Invoke();
            HealthUpdateEvent?.Invoke(Health, -_maxHealth);
        }

        private void SetDead(Transform killer) {
            Health = 0f;
            IsAlive = false;
            DeathEvent?.Invoke(killer);
        }
    }

}
