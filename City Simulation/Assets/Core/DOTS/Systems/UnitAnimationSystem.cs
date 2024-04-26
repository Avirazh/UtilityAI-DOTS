using Assets.Core.DOTS.Components.Tags;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.Core.DOTS.Systems
{
    
    [BurstCompile]
    public partial struct UnitAnimationSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
        }

        public void OnDestroy(ref SystemState state)
        {

        }
        public void OnUpdate(ref SystemState state)
        {
            var entityCommandBuffer = SystemAPI.GetSingleton<BeginPresentationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);

            string seed = SystemAPI.Time.DeltaTime.ToString();

            InstantiateUnitPrefab(ref state, entityCommandBuffer, seed);
            MoveUnit(ref state, entityCommandBuffer);
        }

        private void InstantiateUnitPrefab(ref SystemState state, EntityCommandBuffer entityCommandBuffer, string seed) 
        {
            foreach(var (unitPrefabComponent, entity) in SystemAPI.Query<UnitPrefabComponent>().WithAny<NewUnitTag>().WithEntityAccess())
            {
                
                System.Random random = new System.Random(seed.GetHashCode());
                var instantiatedPrefab = Object.Instantiate(unitPrefabComponent.GameObject);
                instantiatedPrefab.GetComponent<MeshRenderer>().material.color = new Color(
                     (float)random.Next(0,255),
                     (float)random.Next(0,255),
                     (float)random.Next(0,255)
                     );

                entityCommandBuffer.AddComponent(entity, new MovableTag { transform = instantiatedPrefab.transform});
                entityCommandBuffer.RemoveComponent<NewUnitTag>(entity);
            }
        }
        private void MoveUnit(ref SystemState state, EntityCommandBuffer entityCommandBuffer)
        {
            foreach(var (entityTransform, gameObject) in SystemAPI.Query<LocalTransform, MovableTag>())
            {
                gameObject.transform.SetPositionAndRotation(entityTransform.Position, entityTransform.Rotation);
            }
        }
    }
}
