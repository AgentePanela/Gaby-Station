using Robust.Shared.Prototypes;

namespace Content.Shared._Gabystation.StationGoal;

[Prototype("stationGoal")]
public sealed class StationGoalPrototype : IPrototype
{
    /// <inheritdoc/>
    [IdDataField]
    public string ID { get; } = default!;
    public string Text => Loc.GetString($"station-goal-{ID.ToLower()}");
}
