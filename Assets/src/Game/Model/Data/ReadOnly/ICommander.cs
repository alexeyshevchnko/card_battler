using Game.Model.Data.Base;

namespace Game.Model.Data.ReadOnly{
    public interface ICommander : IBaseICard {
        int Health { get; }
    }
}
