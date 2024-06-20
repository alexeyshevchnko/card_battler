using Game.Model.Source;
using UnityEngine;

namespace Shared.Common{
    
    public static class LevelInfo {
        private static int _level;
        private static IConfigs _startData;

        public static int Level => _level;
        public static IConfigs StartData => _startData;

        public static void LevelStart(int lvl, IConfigs startData) {
            _level = lvl;
            _startData = startData;

            Debug.Log($"LevelStart {lvl}");
            GameEvents.LevelStartEvent?.Invoke();
        }

    }
}