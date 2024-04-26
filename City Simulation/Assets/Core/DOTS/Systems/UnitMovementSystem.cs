using Assets.Core.DOTS.Aspects;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

[BurstCompile]
public partial struct UnitMovementSystem : ISystem
{

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state) 
    {

    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        new MoveUnitJob
        {
            //EntityCommandBufferSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>(),
            //SpawnPointBufferComponents = state.GetBufferLookup<SpawnPointBufferComponent>(true)
        }.ScheduleParallel();
    }
    [BurstCompile]
    public partial struct MoveUnitJob : IJobEntity
    {
        //public EndSimulationEntityCommandBufferSystem.Singleton EntityCommandBufferSingleton;
        //[ReadOnly] public BufferLookup<SpawnPointBufferComponent> SpawnPointBufferComponents;
        public float3 destination;
        public void Execute(UnitAspect unitAspect)
        {
            unitAspect.SetDestination(new float3(24.7f, -5.2f , -367.8f));
        }
    }
}
