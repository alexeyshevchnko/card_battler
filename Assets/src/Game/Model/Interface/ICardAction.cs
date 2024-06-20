using System.Collections.Generic;

namespace Game.Model.Interface{
    public interface ICardAction : ICard, IDeserialization {
        IReadOnlyList<IEffect> Effects { get; }
        IEffect FirstEffect { get; }
    }
}
