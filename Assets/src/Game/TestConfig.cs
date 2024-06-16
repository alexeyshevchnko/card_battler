using System.Collections.Generic;
using Game.Data.Battle;
using UnityEngine;

namespace Game{
    
    public class TestConfig
    {
        IReadOnlyList<CardData> _heroes = new List<CardData>();

        private string _testJson = @"{
                ""_cards"": [
                    {
                        ""_name"": ""Hero1"",
                        ""_stars"": 5,
                        ""_level"": 10,
                        ""_health"": " +  Random.Range(30, 41)  + @",
                        ""_fullAttack"": " + Random.Range(5, 16) + @",
                        ""_cardType"": 1,
                        ""_cardMechanicType"": ""1"",
                     " + "  " // ""_charges"": [ { ""_attack"": 20 }, { ""_attack"": 30 } ]
                    +@"
                    },
                    {
                        ""_name"": ""Hero2"",
                        ""_stars"": 4,
                        ""_level"": 8,
                        ""_health"": " + Random.Range(30, 41) + @",
                        ""_fullAttack"":  " + Random.Range(5, 16) + @",
                        ""_cardType"": 2,
                        ""_cardMechanicType"": ""1"",
                           " + "  " // ""_charges"": [ { ""_attack"": 20 }, { ""_attack"": 30 } ]
                    +@"
                    },
                     {
                        ""_name"": ""Buff1"",
                        ""_stars"": 3,
                        ""_level"": 6,
                        ""_health"": 60,
                        ""_fullAttack"": " + (Random.Range(0, 100) > 50 ? Random.Range(-7, 0) : Random.Range(1, 8)) + @",
                        ""_cardType"": 3,
                        ""_cardMechanicType"": ""2"",
                           " + "  " // ""_charges"": [ { ""_attack"": 20 }, { ""_attack"": 30 } ]
                    + @"
                    },
                    {
                        ""_name"": ""Buff2"",
                        ""_stars"": 2,
                        ""_level"": 4,
                        ""_health"": 40,
                        ""_fullAttack"": " + (Random.Range(0, 100) > 50 ? Random.Range(-7, 0) : Random.Range(1, 8)) + @",
                        ""_cardType"": 4,
                        ""_cardMechanicType"": ""2"",
                           " + "  " // ""_charges"": [ { ""_attack"": 20 }, { ""_attack"": 30 } ]
                    + @"
                    },
                    {
                        ""_name"": ""Debuff1"",
                        ""_stars"": 1,
                        ""_level"": 2,
                        ""_health"": 20,
                        ""_fullAttack"": " + (Random.Range(0, 100) > 50 ? Random.Range(-7, 0) : Random.Range(1, 8)) + @",
                        ""_cardType"": 5,
                        ""_cardMechanicType"": ""3"",
                           " + "  " // ""_charges"": [ { ""_attack"": 20 }, { ""_attack"": 30 } ]
                    + @"
                    },
                    {
                        ""_name"": ""Debuff2"",
                        ""_stars"": 1,
                        ""_level"": 1,
                        ""_health"": 10,
                        ""_fullAttack"": " + (Random.Range(0, 100) > 50 ? Random.Range(-7, 0) : Random.Range(1, 8)) + @",
                        ""_cardType"": 6,
                        ""_cardMechanicType"": ""3"",
                           " + "  " // ""_charges"": [ { ""_attack"": 20 }, { ""_attack"": 30 } ]
                    + @"
                    },
                    {
                        ""_name"": ""Buff3"",
                        ""_stars"": 4,
                        ""_level"": 8,
                        ""_health"": 80,
                        ""_fullAttack"": " + (Random.Range(0, 100) > 50 ? Random.Range(-7, 0) : Random.Range(1, 8)) + @",
                        ""_cardType"": 7,
                        ""_cardMechanicType"": ""2"",
                           " + "  " // ""_charges"": [ { ""_attack"": 20 }, { ""_attack"": 30 } ]
                    + @"
                    },
                    {
                        ""_name"": ""Debuff3"",
                        ""_stars"": 2,
                        ""_level"": 5,
                        ""_health"": 50,
                        ""_fullAttack"": " + (Random.Range(0, 100) > 50 ? Random.Range(-7, 0) : Random.Range(1, 8)) + @",
                        ""_cardType"": 8,
                        ""_cardMechanicType"": ""3"", " + "  " 
                    + @"
                    }
                ]
            }";
         

        public TestConfig()
        {
            _heroes = JsonUtility.FromJson<CardDataList>(_testJson).Cards;
            Debug.LogError(_heroes.Count);
            foreach (var hero in _heroes)
            {
                Debug.Log($"dMechanicType: {hero.GetCardMechanicType()} ");
                          //", Stars: {hero.GetStars()}, Health: {hero.GetHealth()}");
                foreach (var charge in hero.GetCharges())
                {
                  //  Debug.Log($"Charge Attack: {charge.GetAttack()}");
                }
            }
        }
    }
    
}
