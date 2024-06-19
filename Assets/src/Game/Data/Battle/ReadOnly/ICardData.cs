using Game.Data.Base;

namespace Game.Data.Battle.ReadOnly {
    public interface ICardData : IBaseICardData {
        int Level { get; }
        int Stars { get; }
    }
}
