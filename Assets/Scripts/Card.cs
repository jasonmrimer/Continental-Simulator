using Game;

public enum CardValue
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
    public CardValue value;

    public Card(Suit suit, CardValue value)
    {
        this.suit = suit;
        this.value = value;
    }

    public string ValueText()
    {
        switch (this.value) {
            case CardValue.Ace:
                return "A";
            case CardValue.Jack:
                return "J";
            case CardValue.Queen:
                return "Q";
            case CardValue.King:
                return "K";
            case CardValue.Joker:
                return "Joker";
            default:
                return ((int)this.value).ToString();
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
}