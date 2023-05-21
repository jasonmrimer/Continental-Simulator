using System.Collections.Generic;
using System.Linq;

public class CardList: List<Card>
{
    public CardList(IOrderedEnumerable<Card> orderBy)
    {
        AddRange(orderBy);
    }

    public CardList()
    {
    }

    public CardList(List<Card> cards)
    {
        AddRange(cards);
    }

    public override string ToString()
    {
        return this.Aggregate("", (current, card) => current + $"{card}, ");
    }
}