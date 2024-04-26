using Assets.Core.DOTS.Aspects;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;


namespace Assets.Core.DOTS.Systems
{
    [BurstCompile]
    public partial struct SpawnerSystem : ISystem
    {
        private Random _random;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            _random.InitState();
        }
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            new SpawnJob
            {
                DeltaTime = SystemAPI.Time.DeltaTime,
                EntityCommandBufferParallelWriter = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),
                Random = _random

            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct SpawnJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter EntityCommandBufferParallelWriter;
        public Random Random;
        public void Execute([ChunkIndexInQuery] int indexInQuery, SpawnerAspect spawnerAspect) 
        {
            spawnerAspect.TimeToNextSpawn -= DeltaTime;

            if(spawnerAspect.TimeToNextSpawn <= 0)
            {
                var newUnit = EntityCommandBufferParallelWriter.Instantiate(indexInQuery, spawnerAspect.Prefab);

                spawnerAspect.TimeToNextSpawn = spawnerAspect.Timer;

                EntityCommandBufferParallelWriter.SetComponent(indexInQuery, newUnit, 
                    new LocalTransform { Position = GetRandomSpawnPosition(spawnerAspect.SpawnPoints), Scale = 1f, Rotation = quaternion.identity });
            }
        }
        private float3 GetRandomSpawnPosition(DynamicBuffer<SpawnPointBufferComponent> spawnerPoints)
        {
            var randomIndex = Random.NextInt(0, spawnerPoints.Length);
            return spawnerPoints[randomIndex].Value;
        }
    }
}
