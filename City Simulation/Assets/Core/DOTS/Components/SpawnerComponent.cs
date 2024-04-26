using Unity.Entities;

public struct SpawnerComponent : IComponentData
{
    public Entity PrefabToSpawn;
}