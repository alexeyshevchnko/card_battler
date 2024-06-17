using System.Collections.Generic;
using UnityEngine;

namespace Game.Data.Battle{
    [System.Serializable]
    public class HeroDataList : ISerializationData
    {
        [SerializeField] List<HeroData> _hero;

        public IReadOnlyList<HeroData> HeroList => _hero;

        public HeroDataList(List<HeroData> data)
        {
            _hero = data;
        }

        public string  GetJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}
