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
    }

}
