using Game.Model.Interface;
using Game.Model.Type;
using TMPro;
using UnityEngine;

namespace Game.View.Battle{

    public class CommanderCard : BaseAliveCard {
        [SerializeField] protected Transform _root;
        [SerializeField] protected Renderer _renderer;
        [SerializeField] TMP_Text _nameTxt;

        public void Init(ICommander data) {
            _hpTxt.text = data.Health.ToString();
            _nameTxt.text = data.Name;
            _aliveEntity.SetMaxHealth(data.Health);
            _aliveEntity.SetAlive();
            SubscribeEvents();
        }

        protected override void OnDeath(Transform killer) {
            _root.gameObject.SetActive(false);
            UnsubscribeEvents();
        }

        public override CardMechanicType CardMechanicType() {
            return Model.Type.CardMechanicType.Commander;
        }

        internal Vector3 GetRootWorldPosition() {
            return _renderer.transform.position;
        }

        public override Transform GetRootTransform() {
            return _root;
        }
    }

}
