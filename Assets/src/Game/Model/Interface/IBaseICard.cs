
using Game.Model.Type;

namespace Game.Model.Interface{

    public interface IBaseICard {
        string Name { get; }
        CardType CardType { get; }
        CardMechanicType CardMechanicType{ get; }
    }
}
