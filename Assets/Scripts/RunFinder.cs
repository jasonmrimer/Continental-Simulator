using System.Collections.Generic;
using System.Linq;

public class RunFinder
{
    public static List<List<Card>> FindPossibleRuns(List<Card> cards)
    {
        List<List<Card>> runOptions = new();
        IEnumerable<IGrouping<Suit, Card>> suitGroups = cards.GroupBy(card => card.Suit);

        foreach (IGrouping<Suit, Card> suitGroup in suitGroups)
        {
            
            List<Card> sortedCards = suitGroup.OrderBy(card => card.Rank).ToList();

            for (int startIndex = 0; startIndex < sortedCards.Count; startIndex++)
            {
                for (int endIndex = startIndex + 3; endIndex < sortedCards.Count; endIndex++)
                {
                    List<Card> run = new();
        
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

    private static bool IsRun(List<Card> potentialRun)
    {
        for (int i = 1; i < potentialRun.Count; i++)
        {
            if (potentialRun[i].Rank != potentialRun[i - 1].Rank + 1)
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