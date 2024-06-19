using Game.Data.Types;

namespace Game.Data.Base{

    public interface IBaseICardData {
        string Name { get; }
        CardType CardType { get; }
        CardMechanicType CardMechanicType{ get; }
    }
}
