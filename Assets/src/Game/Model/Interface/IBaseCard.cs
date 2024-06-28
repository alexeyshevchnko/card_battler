
using Game.Model.Type;

namespace Game.Model.Interface{

    public interface IBaseCard {
        string Name { get; }
        CardType CardType { get; }
        CardMechanicType CardMechanicType{ get; }
        PlayerType PlayerType { get; }
    }
}
