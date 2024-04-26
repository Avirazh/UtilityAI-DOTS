using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
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
        }.ScheduleParallel();
    }
    [BurstCompile]
    public partial struct MoveUnitJob : IJobEntity
    {

        public float3 destination;
        public void Execute(UnitAspect unitAspect)
        {
            unitAspect.SetDestination(new float3(0, 0, 0));
        }
    }
}
