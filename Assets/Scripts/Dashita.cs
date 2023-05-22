using System;
using System.Collections.Generic;
using System.Linq;

public class Dashita
{
    List<Card> _runOne;
    List<Card> _runTwo;


    public Dashita(List<Card> runOne, List<Card> runTwo, List<Card> atama)
    {
        _runOne = runOne;
        _runTwo = runTwo;
        Atama = atama;

        // Runs = new List<Card> { runOne, runTwo };
    }

    public Dashita(List<CardList> runs, List<Card> atama)
    {
        Runs = runs;
        Atama = atama;
    }
    
    public Dashita(List<Run> runs, Atama atama)
    {
        Runsv2 = runs;
        Atamav2 = atama;
    }
    
    


    public List<Card> RunOne => _runOne;

    public List<Card> RunTwo => _runTwo;

    public List<Card> Atama { get; }
    
    public List<Card> Atamav2 { get; }

    public List<CardList> Runs { get; }
    
    public List<Run> Runsv2 { get; }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }

        Dashita other = obj as Dashita;

        List<Run> orderedOtherRuns = other.Runsv2.OrderBy(run => run[0].Rank).ToList();
        List<Run> orderedBaseRuns = this.Runsv2.OrderBy(run => run[0].Rank).ToList();


        // Check if the number of runs and atama is the same
        if (Runsv2.Count != other.Runsv2.Count || Atamav2.Count != other.Atamav2.Count)
            return false;

        // Check if the runs have the same rank and suits
        for (int i = 0; i < Runsv2.Count; i++)
        {
            if (!orderedBaseRuns[i].Equals(orderedOtherRuns[i]))
                return false;
        }

        if (!other.Atamav2.Equals(Atamav2))
        {
            return false;
        }

        return true;
    }

    public override string ToString()
    {
        string message = "";

        foreach (List<Card> run in Runs)
        {
            message += $"{run}\n";
        }

        message += Atama.ToString();

        return message;
    }
}