using Game.Model.Data.Type;

namespace Game.Model.Data.ReadOnly{

    public interface IEffect {
        EffectType EffectType { get; }
        int Value { get; }
    }

}
