using System.Collections.Generic;
using Game.Data.Battle.ReadOnly;
using UnityEngine;

namespace Game.Data.Battle{
    [System.Serializable]
    public class HeroListData : ISerializationData, IReadOnlyHeroListData
    {
        [SerializeField] List<HeroData> _heroList;

        public IReadOnlyList<IHeroData> HeroList => _heroList;

        public HeroListData(List<HeroData> data)
        {
            _heroList = data;
        }

        public string  GetJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}
