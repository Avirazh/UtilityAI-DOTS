using Assets.Core.DOTS.Components;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Assets.Core.DOTS.Authoring
{
    public class SpawnerAuthoring : MonoBehaviour
    {
        public GameObject PrefabToSpawn;
        public List<Transform> SpawnPoints;
        public float Timer;
        public class SpawnerBaker : Baker<SpawnerAuthoring>
        {
            public override void Bake(SpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new SpawnerComponent()
                {
                    PrefabToSpawn = GetEntity(authoring.PrefabToSpawn, TransformUsageFlags.Dynamic),
                });
                var buffer = AddBuffer<SpawnPointBufferComponent>(entity);
                TryAddRangeSpawnPoints(authoring, buffer);
                AddComponent(entity, new UnitSpawnTimerComponent() { Timer = authoring.Timer });
            }

            private void TryAddRangeSpawnPoints(SpawnerAuthoring authoring, DynamicBuffer<SpawnPointBufferComponent> spawnPoints)
            {                
                if (authoring.SpawnPoints != null)
                {
                    for (int i = 0; i < authoring.SpawnPoints.Count; i++)
                    {
                        spawnPoints.Add(new SpawnPointBufferComponent() { Value = authoring.SpawnPoints[i].position });
                    }
                }
            }
        }
    }
}