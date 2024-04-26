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

            AddComponentObject(entity, new UnitPrefabComponent { Value = authoring.UnitPrefab});
            AddComponent(entity, new NewUnitTag());
        }
    }
}
