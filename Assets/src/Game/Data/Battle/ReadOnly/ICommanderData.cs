using Game.Data.Base;

namespace Game.Data.Battle.ReadOnly {
    public interface ICommanderData : IBaseICardData, IDeserializationData {
        int Health { get; }
    }
}
