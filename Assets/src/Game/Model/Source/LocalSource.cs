using System.Collections;
using System.Collections.Generic; 
using Game.Model.Data;
using Game.Model.Interface;
using Game.Model.Manager;
using Game.Model.Type;
using Shared.Settings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Model.Source{

    public class LocalSource : ISource { 

        public ICommander PlayerCommander => _playerCommander;
        public ICommander EnemyCommander => _enemyCommander;

        public IReadOnlyList<ICardAction> PlayerDeck => _playerDeck.CardsData;
        public IReadOnlyList<ICardAction> EnemyDeck => _enemyDeck.CardsData;
       
        public IReadOnlyList<ICardAction> PlayerHead => _playerHead.CardsData;
        public IReadOnlyList<ICardAction> EnemyHead => _enemyHead.CardsData;

        public IHeroManager PlayerHeroes => _playerHeroes;
        public IHeroManager EnemyHeroes => _enemyHeroes;
        public bool IsConnect => _isConnect;

        private Commander _playerCommander;
        private Commander _enemyCommander;

        private HeroManager _playerHeroes;
        private HeroManager _enemyHeroes;

        private ActionCardManager _playerDeck;
        private ActionCardManager _enemyDeck;

        private ActionCardManager _playerHead;
        private ActionCardManager _enemyHead;
        private bool _isConnect = false;


        public IEnumerator Connect() {
            // try {
                Debug.Log("Connect.....");

                float waitTime = Application.isEditor ? 0.2f : 3;

                yield return new WaitForSeconds(waitTime);
                // Замените на HelperTask.WaitForSeconds(3), если это работает

                Debug.Log("generate configs.....");

                var countHero = Random.Range(1, Settings.MAX_HERO_COUNT + 1);
                _playerHeroes = GenerateHeroes(countHero, PlayerType.Player);
                _enemyHeroes = GenerateHeroes(countHero, PlayerType.Enemy);

                _playerCommander = GenerateCommander(PlayerType.Player);
                _enemyCommander = GenerateCommander(PlayerType.Enemy);

                var countDeck = Random.Range(5, 11);
                _playerDeck = GenerateDeck(countDeck, PlayerType.Player);
                _enemyDeck = GenerateDeck(countDeck, PlayerType.Enemy);

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

                _isConnect = true;
        //     }
        //     catch (Exception ex) {
        //         yield return null;
        //         Debug.LogError($"Connect unsuccessful: {ex.Message}");
        //     }
        }

        Commander GenerateCommander(PlayerType playerType) {
            var rez = new Commander();
            int cardTypeValue = Random.Range(Settings.COMMANDER_MIN_ID, Settings.COMMANDER_MAX_ID + 1);
            var json = @"{
                ""_name"": """ + (CardType) cardTypeValue + @""",
                ""_playerType"": """ + (byte)playerType + @""",
                ""_level"": -1,
                ""_health"": " + Settings.COMMANDER_HP + @",
                ""_cardType"": " + cardTypeValue + @",
                ""_cardMechanicType"": " + (byte) CardMechanicType.Commander + @"
            }";
            rez.SetJson(json);
            return rez;
        }

        HeroManager GenerateHeroes(int count, PlayerType playerType) {
            var list = new List<Hero>();

            for (var i = 0; i < count; i++) {
                var temp = new Hero();
                var cardTypeValue = Random.Range(Settings.HERO_MIN_ID, Settings.HERO_MAX_ID + 1);
                var json = @"{
                            ""_name"": """ + (CardType)cardTypeValue + @""",
                            ""_playerType"": """ + (byte)playerType + @""",
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

        ActionCardManager GenerateDeck(int count, PlayerType playerType) {
            var list = new List<CardAction>();

            for (var i = 0; i < count; i++) {
                var effectType = Random.Range(0, 100) > 50 ? (byte) EffectType.Healing : (byte) EffectType.Attack;
                var effectValue = Random.Range(5, 11);
                var temp = new CardAction();
                var cardTypeValue = Random.Range(Settings.EFFECT_MIN_ID, Settings.EFFECT_MAX_ID + 1);
                var mechanicTypeType =
                    (effectValue > 0 ? (byte) CardMechanicType.Buff : (byte) CardMechanicType.DeBuff);

                var json = @"{
                    ""_name"": """ + (CardMechanicType)mechanicTypeType + cardTypeValue.ToString() + @""",
                    ""_playerType"": """ + (byte)playerType + @""",
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

        ActionCardManager GenerateHead(ActionCardManager deck, int count)
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
