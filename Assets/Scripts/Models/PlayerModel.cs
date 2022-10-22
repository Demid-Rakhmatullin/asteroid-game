using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Models
{
    public class PlayerModel : BaseModel
    {
        public float NextShotTime { get; set; }

        public IReactiveProperty<long> CurrentHp { get; private set; }
        public IReadOnlyReactiveProperty<bool> IsDead { get; private set; }

        public IObservable<Unit> OnDamaged { get; private set; }


        public PlayerModel(int initialHp)
        {
            CurrentHp = new ReactiveProperty<long>(initialHp);
            IsDead = CurrentHp
                .Select(hp => hp <= 0)
                .ToReactiveProperty();
            OnDamaged = CurrentHp
                //.Where(_ => !IsDead.Value)
                .Buffer(2, 1)               
                .Where(b => b[0] > b[1])
                .AsUnitObservable();
        }
    }
}
