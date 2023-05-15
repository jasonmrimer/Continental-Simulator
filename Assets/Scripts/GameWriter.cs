using System;

public class GameWriter
{
    public void DeckAndPileStatus(Dealer dealer)
    {
        Console.WriteLine($"Cards left in Deck: {dealer.DeckCardCount()} & Pile: {dealer.PileCardCount()}");
    }

    public void DiscardAction(Player player, Card discard)
    {
        Console.WriteLine($"{player.Name} discards {discard.Printable()}");
    }

    public void DrawAction(Player player, string drawSource, Card drawnCard)
    {
        Console.WriteLine($"{player.Name} draws {drawnCard.Printable()} from {drawSource}");
    }

    public void TurnStart(Player player, int turnCount)
    {
        Console.WriteLine($"{player.Name} begins turn {turnCount} with: {player.FormatHandForPrint()}");
    }
}