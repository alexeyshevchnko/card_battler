using Game.Model.Interface;
using Game.Model.Type;
using UnityEngine;

namespace Game.Model.Data{

    [System.Serializable]
    public class Effect : IEffect
    {
        [SerializeField] private EffectType _effectType;
        [SerializeField] private int _value;

        public EffectType EffectType => _effectType;
        public int Value => _value;

        public string ValueText {
            get {
                var prefix = _effectType == EffectType.Healing ? "+" : "-";
                return $"{prefix}{_value}";
            }
        }
    }

}
