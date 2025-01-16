using Content.Shared._Gabystation.StationGoal;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype.Set;

namespace Content.Server._Gabystation.StationGoal.Components;

[RegisterComponent, Access(typeof(StationGoalSystem))]
public sealed partial class StationGoalComponent : Component
{
    /// <summary>
    /// If have a goal.
    /// </summary>
    [DataField("goalId")]
    public string GoalId = ""; //pass to ?

    /// <summary>
    /// Selected station goal.
    /// </summary>
    /*[DataField("goal")]
    public HashSet<EntProtoId> Goal;*/
}
