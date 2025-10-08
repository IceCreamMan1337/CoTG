using System;
using System.Collections.Generic;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;

public class FuzzyFunctor
{


    // Basic properties
    public string FunctorTag { get; }   // Functor identifier
    public int ResultRank { get; }     // Priority or rank

    // Delegate to encapsulate the functor logic
    private Func<IEnumerable<float>, IEnumerable<float>> Operation { get; }

    // Constructor
    public FuzzyFunctor(string tag, int rank, Func<IEnumerable<float>, IEnumerable<float>> operation)
    {
        FunctorTag = tag;
        ResultRank = rank;
        Operation = operation;
    }

    // Method to apply the encapsulated function
    public IEnumerable<float> Evaluate(IEnumerable<float> inputs)
    {
        return Operation(inputs);
    }

}
