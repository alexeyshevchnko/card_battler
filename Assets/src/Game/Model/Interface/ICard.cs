namespace Game.Model.Interface{ 
    public interface ICard : IBaseCard {
        int Level { get; }
        int Stars { get; }
    }
}
