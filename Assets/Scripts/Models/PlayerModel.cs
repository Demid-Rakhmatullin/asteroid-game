using Messages;
using System;
using System.Linq;
using UniRx;

namespace Models
{
    public class PlayerModel : BaseModel
    {
        public float NextShotTime { get; set; }

        public IReactiveProperty<int> CurrentHp { get; private set; }
        public IReadOnlyReactiveProperty<bool> IsDead { get; private set; }

        public IObservable<Unit> OnDamaged { get; private set; }


        public PlayerModel(int initialHp)
        {
            CurrentHp = new ReactiveProperty<int>(initialHp);
            IsDead = CurrentHp
                .Select(hp => hp <= 0)
                .ToReactiveProperty();
            OnDamaged = CurrentHp
                .Where(_ => !IsDead.Value)
                .Buffer(2, 1)               
                .Where(b => b[0] > b[1])
                .AsUnitObservable();

            CurrentHp
                .Subscribe(hp =>
                {
                    MessageBroker.Default.Publish(
                        new PlayerHpChangedMessage { Value = hp });
                });

        }
    }
}
