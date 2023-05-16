using System;

public class GameWriter
{
    public static void PrintDeckAndPileStatus(Dealer dealer, int turnCount)
    {
        string topDiscard = dealer.TopDiscard != null ? dealer.TopDiscard.Printable() : "none";
        Console.WriteLine(
            $"> Turn {turnCount} <  Cards left in Deck: " +
            $"{dealer.DeckCardCount()} & Pile: {dealer.PileCardCount()} | " +
            $"Top Card: {topDiscard}");
    }

    public void DiscardAction(Player player, Card discard)
    {
        Console.WriteLine($"{player.Name} discards {discard.Printable()}");
    }

    public void DrawAction(Player player, DrawSource drawSource, Card drawnCard)
    {
        Console.WriteLine($"{player.Name} draws from _{drawSource}_: {drawnCard.Printable()}");
    }

    public void TurnStart(Player player, int turnCount)
    {
        Console.WriteLine($"{player.Name} begins turn {turnCount} with: {player.FormatHandForPrint()}");
    }
}