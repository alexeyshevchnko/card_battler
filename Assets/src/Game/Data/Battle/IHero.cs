
namespace Game.Data.Battle{
    public interface IHero
    {
        string GetName();
        int GetLevel();
        int GetStars();
        int GetHealth();
        string GetFullAttack();
        HeroType GetHeroType();
        ICharge[] GetCharges();
    }
}
