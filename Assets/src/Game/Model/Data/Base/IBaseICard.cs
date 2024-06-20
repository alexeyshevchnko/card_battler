
using Game.Model.Data.Type;

namespace Game.Model.Data.Base{

    public interface IBaseICard {
        string Name { get; }
        CardType CardType { get; }
        CardMechanicType CardMechanicType{ get; }
    }
}
