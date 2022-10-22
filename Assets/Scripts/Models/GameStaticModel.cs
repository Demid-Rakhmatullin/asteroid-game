using UniRx;

namespace Models
{
    public static class GameStaticModel
    {
        public static ReactiveProperty<GameState> State = new ReactiveProperty<GameState>(GameState.Paused);
    }
}
