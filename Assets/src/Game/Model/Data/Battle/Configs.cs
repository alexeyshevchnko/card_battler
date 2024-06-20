using System.Collections.Generic;
using Game.Model.Data.ReadOnly;
using Game.Model.Data.Type;
using UnityEngine;

namespace Game.Model.Data.Battle{

    public class Configs : IConfigs {
        public const int HAND_CART_COUNT = 5;
        private const int MAX_HERO_COUNT = 5;
        private const int COMMANDER_HP = 30;

        public ICommander PlayerCommander => _playerCommander;
        public ICommander EnemyCommander => _enemyCommander;
        public IReadOnlyList<ICardAction> PlayerDeck => _playerDeck.CardsData;
        public IReadOnlyList<ICardAction> EnemyDeck => _enemyDeck.CardsData;
        public IReadOnlyList<ICardAction> PlayerHead => _playerHead.CardsData;
        public IReadOnlyList<ICardAction> EnemyHead => _enemyHead.CardsData;

        public IHeroList PlayerHeroes => _playerHeroes;
        public IHeroList EnemyHeroes => _enemyHeroes;

        private readonly Commander _playerCommander;
        private readonly Commander _enemyCommander;

        private readonly HeroList _playerHeroes;
        private readonly HeroList _enemyHeroes;

        private readonly ActionCards _playerDeck;
        private readonly ActionCards _enemyDeck;

        private readonly ActionCards _playerHead;//
        private readonly ActionCards _enemyHead;//


        public Configs() {
            Debug.Log($"generate configs.....");

            var countHero = Random.Range(1, MAX_HERO_COUNT + 1);
            _playerHeroes = GenerateHeroes(countHero);
            _enemyHeroes = GenerateHeroes(countHero);

            _playerCommander = GenerateCommander();
            _enemyCommander = GenerateCommander();

            var countDeck = Random.Range(5, 11);
            _playerDeck = GenerateDeck(countDeck);
            _enemyDeck = GenerateDeck(countDeck);

            _playerHead = GenerateHead(_playerDeck, HAND_CART_COUNT);
            _enemyHead = GenerateHead(_enemyDeck, HAND_CART_COUNT);

            Debug.Log($"_playerCommander = {_playerCommander.GetJson()}");
            Debug.Log($"_enemyCommander = {_enemyCommander.GetJson()}");

            Debug.Log($"_playerDeck = {_playerDeck.GetJson()}");
            Debug.Log($"_enemyDeck = {_enemyDeck.GetJson()}");

            Debug.Log($"_playerHeroes = {_playerHeroes.GetJson()}");
            Debug.Log($"_enemyHeroes = {_enemyHeroes.GetJson()}");

            Debug.Log($"_playerHead = {_playerHead.GetJson()}");
            Debug.Log($"_enemyHead = {_enemyHead.GetJson()}"); 
            
        }


        Commander GenerateCommander() {
            var rez = new Commander();
            int cardTypeValue = Random.Range(DataConfig.COMMANDER_MIN_ID, DataConfig.COMMANDER_MAX_ID + 1);
            var json = @"{
                ""_name"": """ + (CardType) cardTypeValue + @""",
                ""_level"": -1,
                ""_health"": " + COMMANDER_HP + @",
                ""_cardType"": " + cardTypeValue + @",
                ""_cardMechanicType"": " + (byte) CardMechanicType.Commander + @"
            }";
            rez.SetJson(json);
            return rez;
        }

        HeroList GenerateHeroes(int count) {
            var list = new List<Hero>();

            for (var i = 0; i < count; i++) {
                var temp = new Hero();
                var cardTypeValue = Random.Range(DataConfig.HERO_MIN_ID, DataConfig.HERO_MAX_ID + 1);
                var json = @"{
                            ""_name"": ""Hero_" + (CardType) cardTypeValue + @""",
                            ""_stars"":" + Random.Range(1, 6) + @",
                            ""_level"": -1,
                            ""_health"": " + Random.Range(30, 41) + @",
                            ""_fullAttack"":  " + Random.Range(5, 16) + @",
                            ""_cardType"": " + cardTypeValue + @",
                            ""_cardMechanicType"": " + (byte) CardMechanicType.Hero + @"
                        }";
                temp.SetJson(json);
                list.Add(temp);
            }

            return new HeroList(list);
        }

        ActionCards GenerateDeck(int count) {
            var list = new List<CardAction>();

            for (var i = 0; i < count; i++) {
                var effectValue = Random.Range(0, 100) > 50 ? Random.Range(-10, -4) : Random.Range(5, 11);
                var temp = new CardAction();
                var cardTypeValue = Random.Range(DataConfig.EFFECT_MIN_ID, DataConfig.EFFECT_MAX_ID + 1);
                var mechanicTypeType =
                    (effectValue > 0 ? (byte) CardMechanicType.Buff : (byte) CardMechanicType.DeBuff);
                var effectType = (effectValue > 0 ? (byte) EffectType.Healing : (byte) EffectType.Attack);
                var json = @"{
                    ""_name"": """ + (CardType) cardTypeValue + @""",
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

            return new ActionCards(list);
        }


        public ActionCards GenerateHead(ActionCards deck, int count)
        {
            var listDeck = new List<ICardAction>(deck.CardsReadList);
            var selectedCards = new List<ICardAction>();

            // Fisher-Yates
            for (int i = listDeck.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                var temp = listDeck[i];
                listDeck[i] = listDeck[j];
                listDeck[j] = temp;
            }

            for (int i = 0; i < count && i < listDeck.Count; i++)
            {
                selectedCards.Add(listDeck[i]);
            }

            return new ActionCards(selectedCards);
        }
    }
}
