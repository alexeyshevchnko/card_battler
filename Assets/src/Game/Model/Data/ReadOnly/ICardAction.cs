using System.Collections.Generic;
using Game.Model.Data.Base;

namespace Game.Model.Data.ReadOnly{
    public interface ICardAction : ICard, IDeserialization {
        IReadOnlyList<IEffect> Effects { get; }
        IEffect FirstEffect { get; }
    }
}
