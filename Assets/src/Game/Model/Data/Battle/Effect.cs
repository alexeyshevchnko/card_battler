using Game.Model.Data.ReadOnly;
using Game.Model.Data.Type;
using UnityEngine;

namespace Game.Model.Data.Battle{

    [System.Serializable]
    public class Effect : IEffect {
        [SerializeField] private EffectType _effectType;
        [SerializeField] private int _value;

        public EffectType EffectType => _effectType;
        public int Value => _value;
    }

}
