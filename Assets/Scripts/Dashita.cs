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


    public List<Card> RunOne => _runOne;

    public List<Card> RunTwo => _runTwo;

    public List<Card> Atama { get; }

    public List<CardList> Runs { get; }

    public override bool Equals(Object obj)
    {
        if (obj == null)
        {
            return false;
        }

        Dashita other = obj as Dashita;
        
        // Check if the number of runs and atama is the same
        if (Runs.Count != other.Runs.Count || Atama.Count != other.Atama.Count)
            return false;
        
        // Check if the runs have the same rank and suits
        for (int i = 0; i < Runs.Count; i++)
        {
            if (!Runs[i].Equals(other.Runs[i]))
                return false;
        }

        // Check if the atama have the same cards
        for (int i = 0; i < Atama.Count; i++)
        {
            if (!Atama[i].Equals(other.Atama[i]))
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