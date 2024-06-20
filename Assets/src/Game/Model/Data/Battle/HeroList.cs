using System.Collections.Generic;
using Game.Model.Data.Base;
using Game.Model.Data.ReadOnly;
using UnityEngine;

namespace Game.Model.Data.Battle{

    [System.Serializable]
    public class HeroList : ISerialization, IHeroList {
        [SerializeField] List<Hero> _heroes;

        public IReadOnlyList<IHero> Heroes => _heroes;

        public HeroList(List<Hero> data) {
            _heroes = data;
        }

        public string GetJson() {
            return JsonUtility.ToJson(this);
        }
    }

}
