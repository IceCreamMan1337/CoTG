using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits;

public class ScoreData
{
    public Champion Owner { get; }
    public float Points { get; }
    public ScoreCategory ScoreCategory { get; }
    public ScoreEvent ScoreEvent { get; }
    public bool DoCallOut { get; }
    public ScoreData(Champion owner, float points, ScoreCategory scoreCategory, ScoreEvent scoreEvent, bool doCallOut)
    {
        Owner = owner;
        Points = points;
        ScoreCategory = scoreCategory;
        ScoreEvent = scoreEvent;
        DoCallOut = doCallOut;
    }
}
