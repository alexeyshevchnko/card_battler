using Game.Data.Battle.ReadOnly;
using Game.Data.Types;
using UnityEngine;

namespace Game.Data.Battle {

    [System.Serializable]
    public class EffectData : IEffectData {
        [SerializeField] private EffectType _effectType;
        [SerializeField] private int _value;

        public EffectType EffectType => _effectType;
        public int Value => _value;
    }

}
