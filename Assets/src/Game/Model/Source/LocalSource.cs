using System.Collections.Generic;
using Game.Model.Data;
using Game.Model.Interface;
using Game.Model.Manager;
using Game.Model.Type;
using UnityEngine;

namespace Game.Model.Source{

    public class LocalSource : ILocalSource { 

        public ICommander PlayerCommander => _playerCommander;
        public ICommander EnemyCommander => _enemyCommander;

        public IReadOnlyList<ICardAction> PlayerDeck => _playerDeck.CardsData;
        public IReadOnlyList<ICardAction> EnemyDeck => _enemyDeck.CardsData;
       
        public IReadOnlyList<ICardAction> PlayerHead => _playerHead.CardsData;
        public IReadOnlyList<ICardAction> EnemyHead => _enemyHead.CardsData;

        public IHeroManager PlayerHeroes => _playerHeroes;
        public IHeroManager EnemyHeroes => _enemyHeroes;

        private readonly Commander _playerCommander;
        private readonly Commander _enemyCommander;

        private readonly HeroManager _playerHeroes;
        private readonly HeroManager _enemyHeroes;

        private readonly ActionCardManager _playerDeck;
        private readonly ActionCardManager _enemyDeck;

        private readonly ActionCardManager _playerHead;//
        private readonly ActionCardManager _enemyHead;//


        public LocalSource() {
            Debug.Log($"generate configs.....");

            var countHero = Random.Range(1, Settings.MAX_HERO_COUNT + 1);
            _playerHeroes = GenerateHeroes(countHero);
            _enemyHeroes = GenerateHeroes(countHero);

            _playerCommander = GenerateCommander();
            _enemyCommander = GenerateCommander();

            var countDeck = Random.Range(5, 11);
            _playerDeck = GenerateDeck(countDeck);
            _enemyDeck = GenerateDeck(countDeck);

            _playerHead = GenerateHead(_playerDeck, Settings.HAND_CART_COUNT);
            _enemyHead = GenerateHead(_enemyDeck, Settings.HAND_CART_COUNT);

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
            int cardTypeValue = Random.Range(Settings.COMMANDER_MIN_ID, Settings.COMMANDER_MAX_ID + 1);
            var json = @"{
                ""_name"": """ + (CardType) cardTypeValue + @""",
                ""_level"": -1,
                ""_health"": " + Settings.COMMANDER_HP + @",
                ""_cardType"": " + cardTypeValue + @",
                ""_cardMechanicType"": " + (byte) CardMechanicType.Commander + @"
            }";
            rez.SetJson(json);
            return rez;
        }

        HeroManager GenerateHeroes(int count) {
            var list = new List<Hero>();

            for (var i = 0; i < count; i++) {
                var temp = new Hero();
                var cardTypeValue = Random.Range(Settings.HERO_MIN_ID, Settings.HERO_MAX_ID + 1);
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

            return new HeroManager(list);
        }

        ActionCardManager GenerateDeck(int count) {
            var list = new List<CardAction>();

            for (var i = 0; i < count; i++) {
                var effectValue = Random.Range(0, 100) > 50 ? Random.Range(-10, -4) : Random.Range(5, 11);
                var temp = new CardAction();
                var cardTypeValue = Random.Range(Settings.EFFECT_MIN_ID, Settings.EFFECT_MAX_ID + 1);
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

            return new ActionCardManager(list);
        }


        public ActionCardManager GenerateHead(ActionCardManager deck, int count)
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

            return new ActionCardManager(selectedCards);
        }
    }
}
