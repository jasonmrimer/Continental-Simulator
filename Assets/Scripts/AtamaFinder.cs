using System.Collections.Generic;
using System.Linq;

public class AtamaFinder
{
    public static List<List<Card>> FindAtama(List<Card> cards)
    {
        List<List<Card>> atamaOptions = new List<List<Card>>();
        Dictionary<Rank, List<Card>> rankGroups = GroupCardsByRank(cards);

        foreach (var rankGroup in rankGroups)
        {
            List<Card> cardsOfCurrentRank = rankGroup.Value;
            if (cardsOfCurrentRank.Count >= 3)
            {
                GenerateAtamaCombinations(
                    cardsOfCurrentRank,
                    currentCombination: new List<Card>(),
                    atamaOptions
                );
            }
        }

        return atamaOptions;
    }

    private static void GenerateAtamaCombinations(
        List<Card> cards,
        List<Card> currentCombination,
        List<List<Card>> combinations)
    {
        if (currentCombination.Count >= 3)
        {
            List<Card> atamaSortedToPreventDupes = new(currentCombination.OrderBy(card => card.Suit));
            combinations.Add(atamaSortedToPreventDupes);
        }

        for (int i = 0; i < cards.Count; i++)
        {
            Card card = cards[i];
            currentCombination.Add(card);

            // Recursive call with remaining cards to generate combinations
            GenerateAtamaCombinations(
                cards.GetRange(i + 1, cards.Count - (i + 1)),
                currentCombination,
                combinations
            );

            currentCombination.Remove(card);
        }
    }

    private static Dictionary<Rank, List<Card>> GroupCardsByRank(List<Card> cards)
    {
        Dictionary<Rank, List<Card>> rankGroups = new Dictionary<Rank, List<Card>>();

        foreach (Card card in cards)
        {
            if (!rankGroups.ContainsKey(card.Rank))
            {
                rankGroups[card.Rank] = new List<Card>();
            }

            rankGroups[card.Rank].Add(card);
        }

        return rankGroups;
    }
}