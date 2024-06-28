using Game.Model.Interface;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Game.Model.Type;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Game.View.Battle {

    public abstract class HeroCard : BaseAliveCard, IDropHandler {
        [SerializeField] protected Transform _root;
        [SerializeField] protected Renderer _renderer;
        [SerializeField] private TMP_Text _nameTxt;
        [SerializeField] private ChargeSlots _chargeSlots;

        private IHero _data;
        private int _index;

        internal Vector3 GetRootWorldPosition() {
            return _renderer.transform.position;
        }

        public override Transform GetRootTransform() {
            return _root;
        }

        public override CardMechanicType CardMechanicType() {
            return Model.Type.CardMechanicType.Hero;
        }

        public void Init(IHero data, int index) {
            _data = data;
            _index = index;
            _aliveEntity.SetMaxHealth(data.Health);
            _aliveEntity.SetAlive();
            SubscribeEvents();
            UpdateView();
        }

        protected override void OnDeath(Transform killer) {
            _root.gameObject.SetActive(false);
            UnsubscribeEvents();
        }

        private void UpdateView() {
            _nameTxt.text = _data.Name;
            _hpTxt.text = _data.Health.ToString();
            _chargeSlots.Init(_data.Charges);
        }

        internal void Unselect(Material unselectMat) {
            _renderer.material = unselectMat;
        }

        internal virtual bool IsApply(ICardAction actionCardData) {
            var heroType = _data.PlayerType;
            var cardActionType = actionCardData.PlayerType;
            var effectType = actionCardData.FirstEffect.EffectType;

            if (cardActionType == PlayerType.Player) {
                if (heroType == PlayerType.Player && effectType == EffectType.Attack) {
                    return false;
                }

                if (heroType == PlayerType.Player && effectType == EffectType.Healing) {
                    return true;
                }

                if (heroType == PlayerType.Enemy && effectType == EffectType.Healing) {
                    return false;
                }

                if (heroType == PlayerType.Enemy && effectType == EffectType.Attack) {
                    return true;
                }
            }

            /////

            if (cardActionType == PlayerType.Enemy) {
                if (heroType == PlayerType.Player && effectType == EffectType.Attack) {
                    return true;
                }

                if (heroType == PlayerType.Player && effectType == EffectType.Healing) {
                    return false;
                }

                if (heroType == PlayerType.Enemy && effectType == EffectType.Healing) {
                    return true;
                }

                if (heroType == PlayerType.Enemy && effectType == EffectType.Attack) {
                    return false;
                }
            }

            return false;
        }

        internal virtual void Apply(ISelectedCard selectedCard) {
            var actionCardData = selectedCard.Data;
            var effectType = actionCardData.FirstEffect.EffectType;
            var effectValue = actionCardData.FirstEffect.Value;

            if (effectType == EffectType.Attack)
            {
                AliveEntity.ApplyDamage(effectValue, selectedCard.myTrans);
            }

            if (effectType == EffectType.Healing)
            {
                AliveEntity.ApplyDamage(-effectValue, selectedCard.myTrans);
                //AliveEntity.Regeneration()
            }
        }

        internal virtual void TrySelect(ICardAction actionCardData, Material selectMat) {
            if(IsApply(actionCardData))
                _renderer.material = selectMat;
        }

        internal Sequence PlayAttackOnHero(Vector3 wPos, int slotStart = 0, int slotEnd = 0,
                                           UnityAction onAttack = null, float dur = 0.2f) {
            wPos.z -= 0.2f;
            var startPos = GetRootWorldPosition();
            Debug.Log($"play anim Hero {gameObject.name} fromPos = {startPos} toPos = {wPos}");
            var delay = dur;
            Sequence attackSequence = DOTween.Sequence();
            var rTrans = _renderer.transform;
            for (int i = slotStart; i <= slotEnd; i++) {
                _chargeSlots.PlayAttackAnimation(i, delay, dur);
                attackSequence.Append(rTrans.DOMove(wPos, dur));
                attackSequence.AppendCallback(() => onAttack?.Invoke());
                attackSequence.Append(rTrans.DOMove(startPos, dur));
                delay += 2 * dur;
            }

            attackSequence.Play();

            return attackSequence;
        }

        internal Sequence PlayAttackOnCommander(Vector3 wPos, int slotStart = 0, int slotEnd = 0,
                                                UnityAction onAttack = null, float dur = 0.2f) {
            wPos.z -= 0.2f;
            var startPos = GetRootWorldPosition();
            var startRotation = _renderer.transform.eulerAngles;
            Debug.Log($"play anim Commander {gameObject.name} fromPos = {startPos} toPos = {wPos}");

            Sequence attackSequence = DOTween.Sequence();
            var rTrans = _renderer.transform;
            var delay = dur;
            for (int i = slotStart; i <= slotEnd; i++) {
                _chargeSlots.PlayAttackAnimation(i, delay, dur);
                attackSequence.Append(rTrans.DOMove(wPos, dur));
                attackSequence.Join(rTrans.DORotate(new Vector3(startRotation.x, startRotation.y, -180), dur));
                attackSequence.AppendCallback(() => onAttack?.Invoke());
                attackSequence.Append(rTrans.DOMove(startPos, dur));
                attackSequence.Join(rTrans.DORotate(startRotation, dur));
                delay += 2 * dur;
            }

            attackSequence.Play();
            return attackSequence;
        }


        public void OnDrop(PointerEventData eventData) {
            var selectedCard = MatchController.Instance.SelectedCard;
            var actionCardData = selectedCard.Data;
            MatchController.Instance.SetSelectCard(null);
            if (IsApply(actionCardData)) {
                Debug.LogError("OnDrop " + gameObject.name + " index = " + _index);
                Apply(selectedCard);
            }
        }

    }

}
