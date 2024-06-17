using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data.Battle{

    [System.Serializable]
    public class EffectData : IEffectData
    {
        [SerializeField] private EffectType _effectType;
        [SerializeField] private int _value;

        public EffectType GetEffectType() => _effectType;
        public int GetValue() => _value;
    }
    
}
