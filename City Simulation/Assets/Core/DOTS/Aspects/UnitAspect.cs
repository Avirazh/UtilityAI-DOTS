using ProjectDawn.Navigation;
using Unity.Entities;
using Unity.Mathematics;

public readonly partial struct UnitAspect : IAspect
{
    public readonly Entity Entity;

    private readonly RefRW<AgentBody> _agentBody;

    public void SetDestination(float3 destination)
    {
        _agentBody.ValueRW.SetDestination(destination);
    }
}