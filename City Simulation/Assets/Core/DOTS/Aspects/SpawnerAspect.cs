using Assets.Core.DOTS.Components;
using Unity.Entities;

namespace Assets.Core.DOTS.Aspects
{
    public readonly partial struct SpawnerAspect : IAspect
    {
        public readonly Entity entity;

        private readonly RefRO<SpawnerComponent> _spawner;
        private readonly RefRW<UnitSpawnTimerComponent> _unitSpawnTimer;

        public Entity Prefab => _spawner.ValueRO.PrefabToSpawn;
        public readonly DynamicBuffer<SpawnPointBufferComponent> SpawnPoints;
        public float TimeToNextSpawn
        {
            get => _unitSpawnTimer.ValueRO.TimeToNextSpawn;
            set => _unitSpawnTimer.ValueRW.TimeToNextSpawn = value;
        }
        public float Timer => _unitSpawnTimer.ValueRO.Timer;
    }
}