using System;
using UnityEngine;
using Game.Model.Interface;
using Game.Model.Source; 

namespace Game.Controller{

    public class BattleController {
        private ISource _source;
        public ISource Source => _source;

        public BattleController() {
            _source = new LocalSource();
        }

        public bool TryConnect(MonoBehaviour monoBehaviour) {
            try {
                var cor = monoBehaviour.StartCoroutine(_source.Connect());
                return true;
            }
            catch (Exception ex) {
                UnityEngine.Debug.LogError($"Connect unsuccessful: {ex.Message}");
                return false;
            }
        } 
}
    
}
