namespace Game.Model.Interface{ 
    public interface ICard : IBaseICard {
        int Level { get; }
        int Stars { get; }
    }
}
