using Game.Model.Data.Types;

namespace Game.Model.Data.ReadOnly{

    public interface IEffect {
        EffectType EffectType { get; }
        int Value { get; }
    }

}
