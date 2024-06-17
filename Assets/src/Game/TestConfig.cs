using System.Collections.Generic;
using Game.Data;
using Game.Data.Battle;
using UnityEngine;

namespace Game{
    
    public class TestConfig
    {
        private const int MAX_HERO_COUNT = 5;
        private const int MAX_COMMANDER_HP = 30;

        private HeroDataList _playerHeroes;
        private HeroDataList _enemyHeroes;
        private CommanderData _playerCommander;
        private CommanderData _enemyCommander;

        public TestConfig()
        { 
            var countHero = Random.Range(1, MAX_HERO_COUNT + 1);
            _playerHeroes = GenerateHeroList(countHero);
            _enemyHeroes = GenerateHeroList(countHero);

            _playerCommander = GenerateCommander();
            _enemyCommander = GenerateCommander();
           
            Debug.LogError(_playerHeroes.GetJson());

            Debug.LogError($"_playerCommander = {_playerCommander.GetJson()} _enemyCommander = {_enemyCommander.GetJson()}");
        }

        CommanderData GenerateCommander()
        {
            var rez = new CommanderData();
            int cardTypeValue = Random.Range(DataConfig.COMMANDER_MIN_ID, DataConfig.COMMANDER_MAX_ID + 1);
            var json = @"{
                ""_name"": """ + (CardType)cardTypeValue + @""",
                ""_level"": -1,
                ""_health"": " + MAX_COMMANDER_HP + @",
                ""_cardType"": " + cardTypeValue + @",
                ""_cardMechanicType"": " + (byte)CardMechanicType.Commander + @"
            }";
            rez.SetJson(json);
            return rez;

        }

        HeroDataList GenerateHeroList(int count)
        {
            var list = new List<HeroData>();

            for (var i = 0; i < count; i++)
            {
                var temp = new HeroData();
                int cardTypeValue = Random.Range(DataConfig.HEARTS_MIN_ID, DataConfig.HEARTS_MAX_ID + 1);
                var json = @"{
                                ""_name"": ""Hero_" + (CardType)cardTypeValue + @""",
                                ""_stars"":" + Random.Range(1, 6) + @",
                                ""_level"": -1,
                                ""_health"": " + Random.Range(30, 41) + @",
                                ""_fullAttack"":  " + Random.Range(5, 16) + @",
                                ""_cardType"": " + cardTypeValue + @",
                                ""_cardMechanicType"": " + (byte)CardMechanicType.Hero + @"
                            }";
                temp.SetJson(json);
                list.Add(temp); 
            }

            return new HeroDataList(list);
        } 

    }
    
}
