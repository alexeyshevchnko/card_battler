using Game.Model.Interface;
using UnityEngine;

namespace Game.View.Battle {

    public class PlayerHeroCard : HeroCard {

        internal override bool TrySelect(ICardAction actionCardData, Material selectMat)
        {
            var atk = actionCardData.FirstEffect.Value;
            if (atk > 0) {
                _renderer.material = selectMat;
                return true;
            }

            return false;
        }
    }

}
