namespace MapScripts.Map10;

public class LevelScript : LuaLevelScript { }




//I had a tough time figuring out the exact values for turret stat progression for maps other than 1, so I wanna save them for now

//TurretStatsModifier.Armor.FlatBonus = 1;
//TurretStatsModifier.MagicResist.FlatBonus = 1;
//TurretStatsModifier.AttackDamage.FlatBonus = 4;


/*public static void OnUpdate(floatdiff)
{
    var gameTime = GameTime();

    if (gameTime >= timeCheck && timesApplied < 30)
    {
        UpdateTowerStats();
    }
}*/

/*static float timeCheck = 480.0f * 1000;
static byte timesApplied = 0;
static void UpdateTowerStats()
{
    foreach (var team in TurretList.Keys)
    {
        foreach (var lane in TurretList[team].Keys)
        {
            foreach (var turret in TurretList[team][lane])
            {
                if (turret.Type is TurretPosition.FOUNTAIN_TOWER || ((turret.Type is not TurretPosition.HQ_TOWER1 or not TurretPosition.HQ_TOWER2) && timesApplied >= 20))
                {
                    continue;
                }

                turret.AddStatModifier(TurretStatsModifier);
            }
        }
    }

    timesApplied++;
    timeCheck += 60.0f * 1000;
}*/