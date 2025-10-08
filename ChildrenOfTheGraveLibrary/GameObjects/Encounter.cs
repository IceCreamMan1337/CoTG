using System;
using System.Collections.Generic;


namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects;
public class Monster
{
    public string SkinName { get; set; }
    public string OrderSkinName { get; set; }
    public string ChaosSkinName { get; set; }
    public int Value { get; set; }
    public int Count { get; set; }
    public List<string> Tags { get; set; }
    public string MinimapIcon { get; set; }
    public int Targetable { get; set; }
    public int IsLaneMinion { get; set; }
    public int GoldAwarded { get; set; }
    public int ExperienceAwarded { get; set; }
    public int BehaviorTreeAI { get; set; }
    public string runLuaAI { get; set; }
}

public class MonsterGroup
{
    public List<string> Tags { get; set; }
    public List<Monster> Monsters { get; set; }
}

public class EncounterDefinition
{
    public int BonusValue { get; set; }
    public string SquadAI { get; set; }
    public int MaxMonstersPerSpawn { get; set; }
    public double SpawnDelay { get; set; }
    public List<string> Tags { get; set; }
    public List<MonsterGroup> MonsterGroups { get; set; }
}

public class EncounterWrapper
{
    public EncounterDefinition EncounterDefinition { get; set; }
}

public class Encounter
{
    private static int nextEncounterID = 1; // Static field for the EncounterID counter

    public int EncounterID { get; private set; } // Use a private setter

    public EncounterDefinition EncounterDefinition { get; set; }

    public Dictionary<int, MutatorDefinition> MutatorDefinitionslist { get; set; }

    public Encounter(EncounterDefinition _encounterDefinition)
    {
        // Increment the EncounterID counter and assign the value to the EncounterID property
        EncounterDefinition = _encounterDefinition;
        EncounterID = nextEncounterID++;
    }

    public void Addmutator(Dictionary<int, MutatorDefinition> _MutatorDefinitionslist)
    {
        MutatorDefinitionslist = _MutatorDefinitionslist;
    }
    public void ReplaceMutator(int mutatorID, MutatorDefinition newMutator)
    {
        if (MutatorDefinitionslist.ContainsKey(mutatorID))
        {
            MutatorDefinitionslist[mutatorID] = newMutator;
        }
    }
}
public class EncounterManager
{
    private Dictionary<int, Encounter> encounters = new();

    public void AddEncounter(Encounter encounter)
    {
        // Ensure that the EncounterID is unique before adding to the dictionary
        if (!encounters.ContainsKey(encounter.EncounterID))
        {
            encounters[encounter.EncounterID] = encounter;
        }
        else
        {
            // Handle the case where the EncounterID is not unique, for example, throw an exception
            throw new InvalidOperationException("EncounterID already exists.");
        }
    }

    public Encounter GetEncounterByID(int encounterID)
    {
        if (encounters.TryGetValue(encounterID, out Encounter encounter))
        {
            return encounter;
        }
        else
        {
            // Handle the case where the EncounterID does not exist
            return null;
        }
    }
}
