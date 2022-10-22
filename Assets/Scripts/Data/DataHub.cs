using UniRx;

namespace Data
{
    public static class DataHub
    {
        public static ReactiveProperty<GameState> GameState = new ReactiveProperty<GameState>(global::GameState.Stopped);
    }
}
