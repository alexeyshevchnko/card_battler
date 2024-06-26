using System.Globalization;
using Game.Model.Type;
using TMPro;
using UnityEngine;

namespace Game.View.Battle{

    public abstract class BaseAliveCard : MonoBehaviour, IAliveCard {
        [SerializeField] protected Transform _center;
        [SerializeField] protected AliveEntity _aliveEntity;
        [SerializeField] protected TMP_Text _hpTxt;

        public IAliveEntity AliveEntity => _aliveEntity;

        private bool _isSubscribedEvents;

        public abstract CardMechanicType CardMechanicType();
        public abstract Transform GetRootTransform();

        protected virtual void SubscribeEvents() {
            if (!_isSubscribedEvents) {
                AliveEntity.DeathEvent += OnDeath;
                AliveEntity.DamageEvent += OnDamage;
                _isSubscribedEvents = true;
            }
        }

        protected virtual void UnsubscribeEvents() {
            if (_isSubscribedEvents) {
                AliveEntity.DeathEvent -= OnDeath;
                AliveEntity.DamageEvent -= OnDamage;
                _isSubscribedEvents = false;
            }
        }

        protected abstract void OnDeath(Transform killer);

        protected virtual void OnDamage(float damage) {
            _hpTxt.text = _aliveEntity.Health.ToString(CultureInfo.InvariantCulture);
            ScreenTextView.Instance.ShowDamage(_center.position, damage);
        }

        private void OnDestroy() {
            UnsubscribeEvents();
        }
    }

}
