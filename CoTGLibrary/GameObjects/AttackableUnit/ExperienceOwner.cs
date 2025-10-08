using CoTG.CoTGServer.API;
using CoTG.CoTGServer.Content;
using CoTG.CoTGServer.Content.FileSystem;
using CoTG.CoTGServer.GameObjects.AttackableUnits;
using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;
using CoTG.CoTGServer.Logging;
using CoTGEnumNetwork.Content;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static PacketVersioning.PktVersioning;

namespace CoTGLibrary.GameObjects;

public interface IExperienceOwner
{
    Experience Experience { get; }
}

public class Experience
{
    private static readonly ILog _logger = LoggerProvider.GetLogger();

    //Further research needed
    //For now the EXP given will not be impacted by the level difference between the champions

    //private const float LEVEL_DIFFERENCE_EXP_MULTIPLE = 0.08f;
    //private const float MINIMUM_EXP_MULTIPLE = 0.15f;

    private const float EXP_KILL_MULTIPLIER = 0.55f;
    internal const int MAX_LEVEL = 18;

    public float Exp { get; internal set; }
    public int Level { get; internal set; }
    public AttackableUnit Owner;
    internal List<float> ExpNeededPerLevel;
    internal readonly SpellTrainingPoints SpellTrainingPoints;
    internal int LevelCap;

    internal Experience(AttackableUnit owner)
    {
        Owner = owner;
        ExpNeededPerLevel = [];
        SpellTrainingPoints = new()
        {
            TrainingPoints = 1
        };
        //this->mExperienceCallBack = ExperienceCallback; ?
        Exp = 0;
        Level = 1;
        LevelCap = 0;
        RFile? f = Cache.GetFile(Path.Join(ContentManager.MapPath, "ExpCurve.ini"));
        for (int i = 1; i < 18; ++i)
        {
            string lineSelect = "Level" + (i + 1);
            float expAtLevelX;
            uint hash = HashFunctions.HashStringSdbm("EXP", lineSelect);
            float pDefault = (60 * ExpNeededPerLevel.FirstOrDefault() * (i - 1)) + (ExpNeededPerLevel.FirstOrDefault() * 120.0f); //wtf??
            if (f is not null)
            {
                f.GetValue(out expAtLevelX, "EXP", lineSelect, hash, pDefault);
            }
            else
            {
                expAtLevelX = pDefault;
            }
            ExpNeededPerLevel.Add(expAtLevelX);
        }
    }

    public float GetEXPGrantedFromChampion(IExperienceOwner c) => (c.Experience.ExpToNextLevel() - c.Experience.ExpToCurrentLevel()) * EXP_KILL_MULTIPLIER;

    //TODO: Find out why they use a -2 
    float ExpToCurrentLevel()
    {
        if (Level > 1 && Level < MAX_LEVEL)
        {
            if (ExpNeededPerLevel.Count == 0 || Level - 2 >= ExpNeededPerLevel.Count)
            {
                //Throw?
                //__invalid_parameter_noinfo();
            }
            return ExpNeededPerLevel[Level - 2];
        }

        if (Level >= MAX_LEVEL)
        {
            if (ExpNeededPerLevel.Count <= 16)
            {
                //Throw?
                //__invalid_parameter_noinfo();
            }
            return ExpNeededPerLevel[16];
        }
        return 0;
    }

    internal float ExpToNextLevel()
    {
        if (Level >= 17)
        {
            return ExpNeededPerLevel[16];
        }
        int index = Level - 1;
        if (ExpNeededPerLevel.Count == 0 || index >= ExpNeededPerLevel.Count)
        {
            //Throw?
            //__invalid_parameter_noinfo();
        }
        return ExpNeededPerLevel[index];
    }

    public void AddEXP(float toAdd, bool notify = true)
    {
        Exp = Math.Clamp(Exp + toAdd, 0, ExpNeededPerLevel.LastOrDefault());
        if (notify && Owner is Champion c)
        {
            UnitAddEXPNotify(c, toAdd);
        }
    }

    internal void LevelUp()
    {
        int newLevel = Math.Clamp(Level + 1, 1, MAX_LEVEL);

        if (newLevel == Level)
        {
            return;
        }

        Level = newLevel;
        SpellTrainingPoints.AddTrainingPoints();

        NPC_LevelUpNotify(Owner as ObjAIBase);
        ApiEventManager.OnLevelUp.Publish(Owner);

        _logger.Debug($"Experience Owner {Owner.Name} leveled up to {Level}");
    }
}


public class SpellTrainingPoints
{
    public byte TrainingPoints { get; set; }
    public byte TotalTrainingPoints { get; set; }
    public SpellTrainingPoints()
    {
        TrainingPoints = 1;
    }

    public void AddTrainingPoints(byte ammount = 1)
    {
        //this is stupid ? 1 +1 = 2 ??? need investigate where is used 
        TrainingPoints += ammount;
        TotalTrainingPoints += ammount;
    }

    public void SpendTrainingPoint()
    {
        TrainingPoints--;
    }
}
