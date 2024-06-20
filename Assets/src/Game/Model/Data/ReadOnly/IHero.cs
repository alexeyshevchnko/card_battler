using System.Collections.Generic;
using Game.Model.Data.Base;

namespace Game.Model.Data.ReadOnly{

    public interface IHero : ICard, IDeserialization {
        int Health { get; }
        void ResetFullAttack(int val);
        IReadOnlyList<ICharge> Charges { get; }
        int FullAttack { get; }
    }

}
