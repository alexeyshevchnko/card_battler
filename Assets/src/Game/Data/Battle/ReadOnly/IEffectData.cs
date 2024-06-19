using Game.Data.Types;

namespace Game.Data.Battle.ReadOnly {

    public interface IEffectData {
        EffectType EffectType { get; }
        int Value { get; }
    }

}
