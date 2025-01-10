using Content.Server.Station.Systems;
using Robust.Shared.Prototypes;
using Content.Shared.Random;
using Content.Shared.Random.Helpers;
using Robust.Shared.Random;
using Content.Server.Chat.Systems;
using Content.Server.GameTicking;


using Content.Shared._Gabystation.StationGoal;

namespace Content.Server._Gabystation.StationGoal;

/// <summary>
/// Handle the server-side goal system
/// </summary>
public sealed class StationGoalSystem : EntitySystem

{
    [Dependency] private readonly ChatSystem _chatSystem = default!;
    [Dependency] private readonly IPrototypeManager _prototype = default!;
    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly GameTicker _gameTicker = default!;

    [ValidatePrototypeId<WeightedRandomPrototype>]
    private const string RandomProto = "StationGoals";

    public override void Initialize()
    {
        Log.Debug("Station goal initializing");

        SubscribeLocalEvent<StationInitializedEvent>(OnStationInitialized);
    }

    private void OnStationInitialized(StationInitializedEvent msg)
    {
        //SetRandomGoal();
    }

    public void SetRandomGoal()
    {
        if (!_prototype.TryIndex<WeightedRandomPrototype>(RandomProto, out var goals))
        {
            Log.Error($"StationGoalSystem: Random station goal prototype '{RandomProto}' not found");
            return;
        }

        var goal = goals.Pick(_random);
        SetGoal(goal);
    }

    public void SetGoal(string goalId)
    {
        var stations = _gameTicker.GetSpawnableStations(); // this sucks
        Log.Info(goalId);
        _chatSystem.DispatchStationAnnouncement(stations[0], "A goal has been established for the station!");
    }
}
