using System.Collections.Generic;
using Game.Data.Battle.ReadOnly;
using Game.Data.Types;
using UnityEngine;

namespace Game.Data.Battle{
    
    public class GenerateConfigs: IReadOnlyGenerateConfigs
    {
        private const int MAX_HERO_COUNT = 5;
        private const int COMMANDER_HP = 30;

        public ICommanderData PlayerCommander => _playerCommander;
        public ICommanderData EnemyCommander => _enemyCommander;
        
        public IReadOnlyHeroListData PlayerHeroes => _playerHeroesListList;
        public IReadOnlyHeroListData EnemyHeroes => _enemyHeroesListList;

        public IReadOnlyCardActionList PlayerCardAction => _playerCardAction;
        public IReadOnlyCardActionList EnemyCardAction => _enemyCardAction;

        private readonly CommanderData _playerCommander;
        private readonly CommanderData _enemyCommander;

        private readonly HeroListData _playerHeroesListList;
        private readonly HeroListData _enemyHeroesListList;

        private readonly CardActionListData _playerCardAction;
        private readonly CardActionListData _enemyCardAction;


        public GenerateConfigs()
        {
            var countHero = Random.Range(1, MAX_HERO_COUNT + 1);
            _playerHeroesListList = GenerateHeroList(countHero);
            _enemyHeroesListList = GenerateHeroList(countHero);


            _playerCommander = GenerateCommander();
            _enemyCommander = GenerateCommander();

            var countCardAction = Random.Range(5, 11);
            _playerCardAction = GenerateCardAction(countCardAction);
            _enemyCardAction = GenerateCardAction(countCardAction);

            // Debug.LogError($"{_playerCardAction.GetJson()}");
            //
            // Debug.LogError(PlayerCardAction.CardActionList[0].GetEffectData()[0].GetValue());
            //
            // Debug.LogError(_playerHeroesListList.GetJson());
            //
            // Debug.LogError($"PlayerCommander = {_playerCommander.GetJson()} EnemyCommander = {_enemyCommander.GetJson()}");
        }


        CommanderData GenerateCommander()
        {
            var rez = new CommanderData();
            int cardTypeValue = Random.Range(DataConfig.COMMANDER_MIN_ID, DataConfig.COMMANDER_MAX_ID + 1);
            var json = @"{
                ""_name"": """ + (CardType)cardTypeValue + @""",
                ""_level"": -1,
                ""_health"": " + COMMANDER_HP + @",
                ""_cardType"": " + cardTypeValue + @",
                ""_cardMechanicType"": " + (byte)CardMechanicType.Commander + @"
            }";
            rez.SetJson(json);
            return rez;

        }

        HeroListData GenerateHeroList(int count)
        {
            var list = new List<HeroData>();

            for (var i = 0; i < count; i++)
            {
                var temp = new HeroData();
                var cardTypeValue = Random.Range(DataConfig.HERO_MIN_ID, DataConfig.HERO_MAX_ID + 1);
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

            return new HeroListData(list);
        }

        CardActionListData GenerateCardAction(int count)
        {
            var list = new List<CardActionData>();

            for (var i = 0; i < count; i++)
            {
                var effectValue = Random.Range(0, 100) > 50 ? Random.Range(-10, -4) : Random.Range(5, 11);
                var temp = new CardActionData();
                var cardTypeValue = Random.Range(DataConfig.EFFECT_MIN_ID, DataConfig.EFFECT_MAX_ID + 1);
                var mechanicTypeType =
                    (effectValue > 0 ? (byte)CardMechanicType.Buff : (byte)CardMechanicType.DeBuff);
                var effectType = (effectValue > 0 ? (byte)EffectType.Healing : (byte)EffectType.Attack);
                var json = @"{
                    ""_name"": """ + (CardType)cardTypeValue + @""",
                    ""_stars"": " + Random.Range(1, 6) + @",
                    ""_level"": -1,
                    ""_cardType"": " + cardTypeValue + @",
                    ""_cardMechanicType"": " + mechanicTypeType + @",
                    ""_effects"": [
                        {
                            ""_effectType"": " + effectType + @",
                            ""_value"": " + effectValue + @"
                        }
                    ]
                }";
                temp.SetJson(json);
                list.Add(temp);
            }

            return new CardActionListData(list);
        }

    }
}
