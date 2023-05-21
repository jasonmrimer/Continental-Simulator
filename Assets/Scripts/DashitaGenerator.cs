using System;
using System.Collections.Generic;
using System.Linq;
using Game;
using NUnit.Framework;

public class DashitaGenerator
{
    public static Dashita CheckAndCreateDashita(Player player)
    {
        List<Card> handCopy = player.Hand();

        List<Card> run1 = CheckAndCollectRun(handCopy);
        List<Card> run2 = CheckAndCollectRun(handCopy);
        List<Card> atama = CheckAndCollectAtama(handCopy);

        return new Dashita(
            run1,
            run2,
            atama
        );
    }

    private static List<Card> CheckAndCollectAtama(List<Card> hand)
    {
        List<Card> atama = new List<Card>();
        List<Card> sortedCards = hand.OrderBy(card => card.Rank).ToList();

        Card previousCard = null;
        Card currentCard = null;
        atama.Add(sortedCards[0]);

        for (int i = 1; i < sortedCards.Count; i++)
        {
            previousCard = atama[i - 1];
            currentCard = sortedCards[i];

            if (IsEqualRank(previousCard, currentCard))
            {
                atama.Add(currentCard);
            }
            else
            {
                atama.Clear();
                atama.Add(currentCard);
            }

            if (atama.Count == 3)
            {
                hand.RemoveAll(card => atama.Contains(card));
                return atama;
            }
            
        }
        return new List<Card>();
    }

    private static bool IsEqualRank(Card card1, Card card2)
    {
        return card1.Rank == card2.Rank;
    }

    private static List<Card> CheckAndCollectRun(List<Card> hand)
    {
        List<Card> run = new List<Card>();
        IEnumerable<IGrouping<Suit, Card>> suitGroups = hand.GroupBy(card => card.Suit);

        foreach (IGrouping<Suit, Card> suitGroup in suitGroups)
        {
            run.Clear();

            List<Card> sortedCards = suitGroup.OrderBy(card => card.Rank).ToList();

            if (sortedCards.Count < 4)
            {
                break;
            }
            
            Card previousCard = null;
            Card currentCard = null;
            run.Add(sortedCards[0]);

            for (int i = 1; i < sortedCards.Count; i++)
            {
                previousCard = run[i - 1];
                currentCard = sortedCards[i];
                
                if (IsConsecutiveRank(previousCard, currentCard))
                {
                    run.Add(currentCard);
                }
                else
                {
                    run.Clear();
                    run.Add(currentCard);
                }

                if (run.Count == 4)
                {
                    hand.RemoveAll(card => run.Contains(card));
                    return run;
                }
            }
        }

        return new List<Card>();
    }

    private static bool IsConsecutiveRank(Card card1, Card card2)
    {
        return card1.Rank + 1 == card2.Rank;
    }

    public static List<Dashita> DashitaOptions()
    {
        return new List<Dashita>();
    }
}