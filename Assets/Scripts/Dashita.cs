using System;
using System.Collections.Generic;

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

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        
        Dashita dashita = obj as Dashita;
        
        if (obj.GetType() != GetType())
        {
            return false;
        }


        if (dashita.Atama.Equals(Atama) && dashita.Runs.Equals(Runs))
        {
            Console.WriteLine("===========hw");
            return true;
        }

        return false;
    }

    public override string ToString()
    {
        string message = "";
        
        foreach (List<Card> run in Runs)
        {
            message += $"{run}\n";
        }
        return message;
    }
}