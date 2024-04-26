using Unity.Entities;

namespace Assets.Core.DOTS.Components
{
    public struct UnitSpawnTimerComponent : IComponentData
    {
        public float TimeToNextSpawn;
        public float Timer;
    }
}