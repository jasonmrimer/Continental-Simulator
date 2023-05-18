using System.Collections.Generic;
using System.Linq;
using Game;

public class MoveValidator
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
        return new List<Card>();
    }

    private static List<Card> CheckAndCollectRun(List<Card> hand)
    {
        List<Card> run = new List<Card>();
        IEnumerable<IGrouping<Suit, Card>> suitGroups = hand.GroupBy(card => card.suit);

        foreach (IGrouping<Suit, Card> suitGroup in suitGroups)
        {
            run.Clear();

            List<Card> sortedCards = suitGroup.OrderBy(card => card.rank).ToList();

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

        return null;
    }

    private static bool IsConsecutiveRank(Card card1, Card card2)
    {
        return card1.rank + 1 == card2.rank;
    }
}