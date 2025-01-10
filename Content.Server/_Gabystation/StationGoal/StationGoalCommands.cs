using System.Linq;
using Content.Server.Administration;
using Content.Shared.Administration;
using Robust.Shared.Console;
using Robust.Shared.Prototypes;

namespace Content.Server._Gabystation.StationGoal;

[AdminCommand(AdminFlags.Fun)]
public sealed class StationGoalCommand : IConsoleCommand
{
    public string Command => "setrandomgoal";
    public string Description => Loc.GetString("set-random-station-goal-command-description");
    public string Help => $"{Command}";

    public void Execute(IConsoleShell shell, string argStr, string[] args)
    {
        var stationGoal = IoCManager.Resolve<IEntityManager>().System<StationGoalSystem>();
        stationGoal.SetRandomGoal();
    }
}
