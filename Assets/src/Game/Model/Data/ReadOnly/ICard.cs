using Game.Model.Data.Base;

namespace Game.Model.Data.ReadOnly{
    public interface ICard : IBaseICard {
        int Level { get; }
        int Stars { get; }
    }
}
