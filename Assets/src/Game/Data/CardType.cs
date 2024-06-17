
namespace Game.Data{
    public enum CardType 
    {
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

        Hearts1 = 11,
        Hearts2 = 12,
        Hearts3 = 13,
        Hearts4 = 14,
        Hearts5 = 15,

        //test
        TstCard1 = 16,
        TstCard2 = 17,
        TstCard3 = 18,

        //Commander
        Commander1 = 19,
        Commander2 = 20,
        Commander3 = 21,
        Commander4 = 22,
        Commander5 = 23,
    }


    public static class DataConfig
    {
        public const int COMMANDER_MIN_ID = 19;
        public const int COMMANDER_MAX_ID = 23;

        public const int HEARTS_MIN_ID = 11;
        public const int HEARTS_MAX_ID = 15;
        public const int BUFF_ID = 1;
        public const int DEBUFF_ID = 6;
        public const int TSTCARD_ID = 16;
    } 
}
