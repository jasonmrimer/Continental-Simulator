using System;
using System.Linq;

public class Atama: CardList
{
    public override bool Equals(object obj)
    {
        if (obj is not Atama other)
        {
            return false;
        }

        return (other.Count == this.Count) &&
               (other.OrderBy(card => card.Suit).SequenceEqual(this.OrderBy(card => card.Suit)));
    }
}