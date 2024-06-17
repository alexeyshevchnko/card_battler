
namespace Game.Data.Types{
    public enum CardType 
    {
        //EFFECTS
        //buff
        Clubs1 = 1,
        Clubs2 = 2,
        Clubs3 = 3,
        Clubs4 = 4,
        Clubs5 = 5,
        //debuff
        Diamonds1 = 6,
        Diamonds2 = 7,
        Diamonds3 = 8,
        Diamonds4 = 9,
        Diamonds5 = 10,

        //COMMANDER
        Hearts1 = 11,
        Hearts2 = 12,
        Hearts3 = 13,
        Hearts4 = 14,
        Hearts5 = 15,

        //COMMANDER
        Commander1 = 16,
        Commander2 = 17,
        Commander3 = 18,
        Commander4 = 19,
        Commander5 = 20,
    }


    public static class DataConfig
    {
        public const int COMMANDER_MIN_ID = 16;
        public const int COMMANDER_MAX_ID = 23;

        public const int HERO_MIN_ID = 11;
        public const int HERO_MAX_ID = 15;

        public const int EFFECT_MIN_ID = 1;
        public const int EFFECT_MAX_ID = 10;
    } 
}
