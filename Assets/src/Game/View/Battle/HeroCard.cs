using Game.Model.Interface;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace Game.View.Battle{

    public abstract class HeroCard : MonoBehaviour {
        [SerializeField] protected Renderer _renderer;
        [SerializeField] private TMP_Text _nameTxt;
        [SerializeField] private TMP_Text _hpTxt;
        [SerializeField] private ChargeSlots _chargeSlots;
        [SerializeField] private AliveEntity _aliveEntity;

        public IAliveEntity AliveEntity => _aliveEntity;

        private IHero _data;

        internal Vector3 GetRootWorldPosition() {
            return _renderer.transform.position;
        }

        public void Init(IHero data) {
            _data = data;
            _aliveEntity.SetMaxHealth(data.Health);
            _aliveEntity.SetAlive();
            UpdateView();
        }

        private void UpdateView() {
            _nameTxt.text = _data.Name;
            _hpTxt.text = _data.Health.ToString();
            _chargeSlots.Init(_data.Charges);
        }

        internal void Unselect(Material unselectMat) {
            _renderer.material = unselectMat;
        }

        internal abstract bool TrySelect(ICardAction actionCardData, Material selectMat);

        internal void PlayAttackOnHero(Vector3 wPos, int count = 1, float dur = 0.2f) {
            wPos.z -= 0.2f;
            var startPos = GetRootWorldPosition();
            Debug.Log($"play anim Hero {gameObject.name} fromPos = {startPos} toPos = {wPos}");

            Sequence attackSequence = DOTween.Sequence();
            var rTrans = _renderer.transform;
            for (int i = 0; i < count; i++)
            {
                attackSequence.Append(rTrans.DOMove(wPos, dur));
                attackSequence.AppendCallback(() => Debug.Log("Attack"));
                attackSequence.Append(rTrans.DOMove(startPos, dur));
            }

            attackSequence.Play();
        }

        internal void PlayAttackOnCommander(Vector3 wPos, int count = 1, float dur = 0.2f) {
            wPos.z -= 0.2f;
            var startPos = GetRootWorldPosition();
            var startRotation = _renderer.transform.eulerAngles;
            Debug.Log($"play anim Commander {gameObject.name} fromPos = {startPos} toPos = {wPos}");

            Sequence attackSequence = DOTween.Sequence();
            var rTrans = _renderer.transform;
            for (int i = 0; i < count; i++) {
                attackSequence.Append(rTrans.DOMove(wPos, dur));
                attackSequence.Join(rTrans.DORotate(new Vector3(startRotation.x, startRotation.y, -180), dur));
                attackSequence.AppendCallback(() => Debug.Log("Attack"));
                attackSequence.Append(rTrans.DOMove(startPos, dur));
                attackSequence.Join(rTrans.DORotate(startRotation, dur));
            }

            attackSequence.Play();
        }
    }

}
