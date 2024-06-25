using System.Collections.Generic;
using Game.Model.Interface;
using UnityEngine;

namespace Game.View.Battle{

    public class ChargeSlots : MonoBehaviour {
        [SerializeField] List<ChargeSlot> _slots;

        private IReadOnlyList<ICharge> _data;

        public void Init(IReadOnlyList<ICharge> data) {
            _data = data;
            UpdateView();
        }

        private void UpdateView()
        {
            for (int i = 0; i < _slots.Count; i++) {
                var data = i >= _data.Count ? null : _data[i];
                var slot = _slots[i];
                slot.Init(data);
            } 
        }


        internal void  PlayAttackAnimation(int slotIndex, float delay,  float dur = 0.2f) {
            _slots[slotIndex].PlayAttackAnimation(delay, dur);
        }

    }

}
