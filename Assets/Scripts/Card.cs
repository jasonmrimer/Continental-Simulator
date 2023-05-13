public enum Suit
{
    Clubs,
    Diamonds,
    Hearts,
    Spades,
    Wild
}

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
}