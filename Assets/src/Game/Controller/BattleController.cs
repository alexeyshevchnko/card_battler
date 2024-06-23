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
        public IHeroManager PlayerHeroes => _source.PlayerHeroes;
        public IHeroManager EnemyHeroes => _source.EnemyHeroes;
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
