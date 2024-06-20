using Game.Model.Interface;
using UnityEngine;

namespace Shared.Common{
    
    public static class LevelInfo {
        private static int _level;
        private static ILocalSource _startData;

        public static int Level => _level;
        public static ILocalSource StartData => _startData;

        public static void LevelStart(int lvl, ILocalSource startData) {
            _level = lvl;
            _startData = startData;

            Debug.Log($"LevelStart {lvl}");
            GameEvents.LevelStartEvent?.Invoke();
        }

    }
}