﻿using Game.Model.Interface;
using Game.Model.Type;
using UnityEngine;

namespace Game.View.Battle {

    public class EnemyHeroCard : HeroCard {

        internal override bool TrySelect(ICardAction actionCardData, Material selectMat) {
            var atk = actionCardData.FirstEffect.Value;
            if (atk <= 0) {
                _renderer.material = selectMat;
                return true;
            }

            return false;
        }
    }

}
