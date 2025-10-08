using System;
using System.Collections.Generic;


namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects;
public class Mutation
{
    public string Stat { get; set; }
    public double Value { get; set; }
    public double PerLevelValue { get; set; }
    public double Min { get; set; }
    public double Max { get; set; }
}

public class MutatedData
{
    public int MutationIndex { get; set; }
    public List<Mutation> Mutation { get; set; }
}

public class MutatorDefinition
{
    public string Type { get; set; }
    public List<MutatedData> MutatedData { get; set; }
}

public class MutatorWrapper
{
    public MutatorDefinition MutatorDefinition { get; set; }
}

public class Mutator
{
    private static int nextMutatorID = 1; // Static field for the MutatorID counter

    public int MutatorID { get; private set; } // Use a private setter
    public MutatorDefinition MutatorDefinition { get; set; }

    public Mutator(MutatorDefinition _MutatorDefinition)
    {
        // Increment the MutatorID counter and assign the value to the MutatorID property
        MutatorDefinition = _MutatorDefinition;
        MutatorID = nextMutatorID++;
    }
}
public class MutatorManager
{
    private Dictionary<int, Mutator> mutators = new();

    public void AddMutatorIndex(Mutator mutator)
    {
        // Ensure that the MutatorID is unique before adding to the dictionary
        if (!mutators.ContainsKey(mutator.MutatorID))
        {
            mutators[mutator.MutatorID] = mutator;
        }
        else
        {
            // Handle the case where the MutatorID is not unique, for example, throw an exception
            throw new InvalidOperationException("MutatorID already exists.");
        }
    }

    public Mutator GetMutatorByID(int mutatorid)
    {
        if (mutators.TryGetValue(mutatorid, out Mutator mutator))
        {
            return mutator;
        }
        else
        {
            // Handle the case where the MutatorID does not exist
            return null;
        }
    }
}
