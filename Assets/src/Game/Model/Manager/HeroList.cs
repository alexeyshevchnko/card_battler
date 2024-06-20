using System.Collections.Generic;
using Game.Model.Data;
using Game.Model.Interface;
using UnityEngine;

namespace Game.Model.Manager{

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
