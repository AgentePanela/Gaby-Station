using Content.Server.Station.Systems;
using Robust.Shared.Prototypes;
using Content.Shared.Random;
using Content.Shared.Random.Helpers;
using Robust.Shared.Random;
using Content.Server.Chat.Systems;
using Content.Server.GameTicking;
using Content.Server.Communications; // send goal
using Content.Shared.Paper;
using Robust.Shared.GameObjects;
using Content.Shared._Gabystation.StationGoal;
using Content.Server._Gabystation.StationGoal.Components;

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
    [Dependency] private readonly PaperSystem _paper = default!;

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

    public void SetGoal(string goalId) //pass to prototype
    {
        var stations = _gameTicker.GetSpawnableStations(); // this sucks
        if (!EntityManager.TryGetComponent<StationGoalComponent>(stations[0], out var comp))
            return;

        Log.Info(goalId);

        comp.GoalId = goalId;
        _chatSystem.DispatchStationAnnouncement(stations[0], "A goal has been established for the station!");
        SendGoal();
    }

    /// <summary>
    /// Send the goal to all communications console.
    /// </summary>
    public void SendGoal()
    {
        //TODO: Comns console function to send papers
        var consoles = EntityManager.EntityQuery<CommunicationsConsoleComponent>();
        foreach (var console in consoles)
        {
            if (!EntityManager.TryGetComponent(console.Owner, out TransformComponent? transform))
                continue;
            var consolePos = transform.MapPosition;
            var paper = EntityManager.Spawn(/*"GoalPaper"*/"Paper", consolePos);
            //_paper.SetContent(paper.Id, "");
        }
    }
}
