using UnityEngine;
using UnityEngine.Events;

namespace Game.View.Battle {

    public interface IAliveEntity {
        float MaxHealth { get; }
        float Health { get; }
        float HealthPercent { get; }
        bool IsAlive { get; }
        float Armor { get; }

        event UnityAction<Transform> DeathEvent;
        event UnityAction AliveEvent;
        event UnityAction<float, float> HealthUpdateEvent;
        event UnityAction<float> DamageEvent;

        //void SetMaxHealth(float val);
        void SetArmor(float val);
        void ApplyDamage(float damage, Transform damageSource, bool showDamage = true);
        void Regen(float regen);
        //void SetAlive();
    }

}
