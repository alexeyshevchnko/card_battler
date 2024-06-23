using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Model.Interface; 
using Game.Model.Source;

namespace Game.Controller {

    public class BattleController {
        public ICommander PlayerCommander => _source.PlayerCommander;
        public ICommander EnemyCommander => _source.EnemyCommander;
        public IReadOnlyList<ICardAction> PlayerDeck => _source.PlayerDeck;
        public IReadOnlyList<ICardAction> EnemyDeck => _source.EnemyDeck;
        public IReadOnlyList<ICardAction> PlayerHead => _source.PlayerHead;
        public IReadOnlyList<ICardAction> EnemyHead => _source.EnemyHead;
        public IReadOnlyList<IHero> PlayerHeroes => _source.PlayerHeroes.Heroes;
        public IReadOnlyList<IHero> EnemyHeroes => _source.EnemyHeroes.Heroes;
        public bool IsConnect => _source.IsConnect;

        private ISource _source;

        public BattleController() {
            _source = new LocalSource();
        }

        public bool TryConnect(MonoBehaviour monoBehaviour) {
            try {
                var cor = monoBehaviour.StartCoroutine(_source.Connect());
                return true;
            }
            catch (Exception ex) {
                Debug.LogError($"Connect unsuccessful: {ex.Message}");
                return false;
            }
        }
    }
}
