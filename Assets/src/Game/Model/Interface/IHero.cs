using System.Collections.Generic;
namespace Game.Model.Interface{

    public interface IHero : ICard, IDeserialization {
        int Health { get; }
        void ResetFullAttack(int val);
        IReadOnlyList<ICharge> Charges { get; }
        int FullAttack { get; }
    }

}
