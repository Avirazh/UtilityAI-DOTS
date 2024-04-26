using Assets.Core.DOTS.Components.Tags;
using Unity.Entities;
using UnityEngine;

public class UnitAuthoring : MonoBehaviour
{
    public GameObject UnitPrefab;
    public class UnitBaker : Baker<UnitAuthoring>
    {
        public override void Bake(UnitAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            var meshRenderer = authoring.UnitPrefab.GetComponent<MeshRenderer>();
            
            AddComponentObject(entity, new UnitPrefabComponent { GameObject = authoring.UnitPrefab, Renderer = meshRenderer});
            AddComponent(entity, new NewUnitTag());
        }
    }
}
