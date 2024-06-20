using Game.Model.Data.Types;

namespace Game.Model.Data.Base{

    public interface IBaseICard {
        string Name { get; }
        CardType CardType { get; }
        CardMechanicType CardMechanicType{ get; }
    }
}
