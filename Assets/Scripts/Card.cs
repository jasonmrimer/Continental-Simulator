using Game;

public enum Rank
{
    Joker = 0,
    Ace = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Jack = 11,
    Queen = 12,
    King = 13
}

public class Card
{
    public Suit suit;
    public Rank rank;

    public Card(Rank rank, Suit suit)
    {
        this.suit = suit;
        this.rank = rank;
    }

    public string ValueText()
    {
        switch (this.rank) {
            case Rank.Ace:
                return "A";
            case Rank.Jack:
                return "J";
            case Rank.Queen:
                return "Q";
            case Rank.King:
                return "K";
            case Rank.Joker:
                return "Jo";
            default:
                return ((int)this.rank).ToString();
        } 
    }
    
    public string SuitSymbol()
    {
        switch (this.suit) {
            case Suit.Hearts:
                return "♥";
            case Suit.Diamonds:
                return "♦";
            case Suit.Clubs:
                return "♣";
            case Suit.Spades:
                return "♠";
            case Suit.Wild:
                return "★";
            default:
                return base.ToString();
        }
    }

    public string Printable()
    {
        return $"{this.ValueText()}{this.SuitSymbol()}";
    }

    public override string ToString()
    {
        return $"Card {this.Printable()}";
    }
}