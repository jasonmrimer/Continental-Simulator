using System.Collections.Generic;

public class GameController
{
    private Dealer _dealer;
    private List<Player> _players;
    private bool _gameIsOver;
    private int _turnCount;
    private int _currentPlayerIndex;
    private readonly int _turnLimit;
    private readonly GameWriter _gameWriter;

    public GameController(int turnLimit = 100)
    {
        _turnLimit = turnLimit;
        _gameWriter = new GameWriter();

        SetupAndDeal();
    }

    public void Play()
    {
        while (ShouldContinuePlaying())
        {
            GameWriter.PrintDeckAndPileStatus(_dealer, _turnCount);
            Player currentPlayer = _players[_currentPlayerIndex];

            if (outOfCards())
            {
                _gameIsOver = true;
                break;
            }

            _dealer.RecyclePileIntoDeck();
            // PlayersVieForTopDiscard(_currentPlayerIndex);
            PlayerDraws(currentPlayer);
            PlayerDiscards(currentPlayer);

            _currentPlayerIndex = RotateToNextPlayer(_currentPlayerIndex);
        }
    }

    private void PlayersVieForTopDiscard(int currentPlayerIndex)
    {
        // throw new System.NotImplementedException();
    }

    private bool outOfCards()
    {
        return _dealer.DeckCardCount() == 0 && _dealer.PileCardCount() == 0;
    }

    private bool ShouldContinuePlaying()
    {
        if (_turnCount == _turnLimit)
        {
            _gameIsOver = true;
        }

        return !_gameIsOver;
    }

    private int RotateToNextPlayer(int currentPlayerIndex)
    {
        _turnCount++;
        currentPlayerIndex = (currentPlayerIndex == _players.Count - 1) ? 0 : (currentPlayerIndex + 1);
        return currentPlayerIndex;
    }

    private void PlayerDraws(Player player)
    {
        _gameWriter.TurnStart(player, _turnCount);

        DrawSource drawSource = player.ChooseDrawSource(_dealer.TopDiscard != null);
        Card drawnCard = _dealer.GiveCardFrom(drawSource);
        player.AddToHand(drawnCard);
        
        _gameWriter.DrawAction(player, drawSource, drawnCard);
    }
    
    //
    // private void PlayerDrawsFormer(Player player)
    // {
    //     _gameWriter.TurnStart(player, _turnCount);
    //     
    //     DrawSource drawSource = DrawSource.Deck;
    //     Card drawnCard;
    //
    //     if (!_drawChoiceEnabled)
    //     {
    //         drawnCard = _dealer.DrawFromDeck();
    //     }
    //     else
    //     {
    //         drawSource = player.ChooseDrawSource(
    //             pileIsAvailable: _dealer.PileCardCount() > 0
    //         );
    //         drawnCard = ChooseCardFromDeckOrPile(drawSource);
    //     }
    //
    //     player.AddToHand(drawnCard);
    //     _gameWriter.DrawAction(player, drawSource, drawnCard);
    // }

    // private Card ChooseCardFromDeckOrPile(DrawSource drawSource)
    // {
    //     return drawSource == DrawSource.Deck ? _dealer.DrawFromDeck() : _dealer.DrawFromPile();
    // }

    private void PlayerDiscards(Player player)
    {
        Card discard = player.DiscardFromHand();
        _dealer.TakeDiscard(discard);
        _gameWriter.DiscardAction(player, discard);
    }

    private void SetupAndDeal()
    {
        // Setup
        _players = new PlayerStub().CreatePlayers();
        _dealer = new Dealer(new Deck(), _players);

        _turnCount = 1;
        _currentPlayerIndex = 0;
        _gameIsOver = false;

        // Deal
        _dealer.Deal();
    }

    public bool IsFinished()
    {
        return _gameIsOver;
    }

    public int TurnCount()
    {
        return _turnCount;
    }
}