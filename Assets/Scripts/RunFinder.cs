using System.Collections.Generic;
using System.Linq;

public class RunFinder
{
    public static List<CardList> FindPossibleRuns(List<Card> cards)
    {
        List<CardList> runOptions = new();
        IEnumerable<IGrouping<Suit, Card>> suitGroups = cards.GroupBy(card => card.Suit);

        foreach (IGrouping<Suit, Card> suitGroup in suitGroups)
        {
            
            List<Card> sortedCards = suitGroup.OrderBy(card => card.Rank).ToList();
            Card aceInGroup = sortedCards.Find(card => card.Rank == Rank.Ace);
           
            // add Ace to end for run check
            if (aceInGroup != null)
            {
                sortedCards.Add(aceInGroup);
            }
            
            for (int startIndex = 0; startIndex < sortedCards.Count; startIndex++)
            {
                for (int endIndex = startIndex + 3; endIndex < sortedCards.Count; endIndex++)
                {
                    CardList run = new();
        
                    for (int i = startIndex; i <= endIndex; i++)
                    {
                        run.Add(sortedCards[i]);
                    }
        
                    if (IsRun(run))
                    {
                        runOptions.Add(run);
                    }
                }
            }
            // for (int i = 1; i < sortedCards.Count; i++)
            // {
            //     Card previousCard = run[i - 1];
            //     Card currentCard = sortedCards[i];
            //     
            //     if (IsConsecutiveRank(previousCard, currentCard))
            //     {
            //         run.Add(currentCard);
            //     }
            //     else
            //     {
            //         run.Clear();
            //         run.Add(currentCard);
            //     }
            //
            //     if (run.Count >= 4)
            //     {
            //         runOptions.Add(run);
            //     }
            // }
        }

        

        return runOptions;
    }

    private static bool IsRun(List<Card> suitedAndSortedCards)
    {
        // Check if the cards form a run (straight flush)
        
        // Sort the cards by rank
        // potentialRunOfSuitedCards.Sort();

        // Check if the cards have consecutive ranks, accounting for Aces as both 1 and 14
        for (int i = 1; i < suitedAndSortedCards.Count; i++)
        {
            Rank currentRank = suitedAndSortedCards[i].Rank;
            Rank previousRank = suitedAndSortedCards[i - 1].Rank;

            if (previousRank == Rank.Ace)
            {
                if (currentRank != Rank.Two)
                {
                    return false;
                }

            }
            else if (previousRank == Rank.King)
            {
                // Handle Ace as both 1 and 14
                if (currentRank != Rank.Ace)
                {
                    return false;
                }
            }
            else if (currentRank != previousRank + 1)
            {
                return false;
            }
        }
        
        return true;
    }

    private static bool IsConsecutiveRank(Card card1, Card card2)
    {
        return card1.Rank + 1 == card2.Rank;
    }

}