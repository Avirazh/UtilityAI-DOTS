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
            _random.InitState(12333);
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
                for (int i = 0; i < spawnerAspect.SpawnPoints.Length; i++)
                {
                    var newUnit = EntityCommandBufferParallelWriter.Instantiate(indexInQuery, spawnerAspect.Prefab);

                    spawnerAspect.TimeToNextSpawn = spawnerAspect.Timer;

                    EntityCommandBufferParallelWriter.SetComponent(indexInQuery, newUnit,
                        new LocalTransform { Position = spawnerAspect.SpawnPoints.ElementAt(i).Value, Scale = 1f, Rotation = quaternion.identity });
                    //UnityEngine.Debug.Log($"{spawnerAspect.SpawnPoints.Length}");
                }
            }
        }
        private float3 GetRandomSpawnPosition(DynamicBuffer<SpawnPointBufferComponent> spawnerPoints)
        {
            var randomIndex = Random.NextInt(spawnerPoints.Length);
            UnityEngine.Debug.Log($"{spawnerPoints[randomIndex].Value}, {randomIndex}");
            return spawnerPoints[randomIndex].Value;
        }
    }
}
