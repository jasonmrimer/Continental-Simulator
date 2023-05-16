using System.Collections.Generic;

public class GameController
{
    private Dealer _dealer;
    private List<Player> _players;
    private bool _gameIsOver;
    private readonly bool _drawChoiceEnabled;
    private int _turnCount;
    private int _currentPlayerIndex;
    private readonly int _turnLimit;
    private readonly GameWriter _gameWriter;

    public GameController(bool drawChoiceEnabled = false, int turnLimit = 100)
    {
        _drawChoiceEnabled = drawChoiceEnabled;
        _turnLimit = turnLimit;
        _gameWriter = new GameWriter();

        SetupAndDeal();
    }

    public void Play()
    {
        while (ShouldContinuePlaying())
        {
            _gameWriter.DeckAndPileStatus(_dealer, _turnCount);
            if (_dealer.DeckCardCount() == 0 && _dealer.PileCardCount() == 0)
            {
                _gameIsOver = true;
                break;
            }

            Player currentPlayer = _players[_currentPlayerIndex];

            _dealer.RecyclePileIntoDeck();
            PlayerDraws(currentPlayer);
            PlayerDiscards(currentPlayer);

            _currentPlayerIndex = RotateToNextPlayer(_currentPlayerIndex);
        }
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

        DrawSource drawSource = DrawSource.Deck;
        Card drawnCard;

        if (!_drawChoiceEnabled)
        {
            drawnCard = _dealer.DrawFromDeck();
        }
        else
        {
            drawSource = player.ChooseDrawSource(
                pileIsAvailable: _dealer.PileCardCount() > 0
            );
            drawnCard = ChooseCardFromDeckOrPile(drawSource);
        }

        player.AddToHand(drawnCard);
        _gameWriter.DrawAction(player, drawSource.ToString(), drawnCard);
    }
    
    private Card ChooseCardFromDeckOrPile(DrawSource drawSource)
    {
        return drawSource == DrawSource.Deck ? _dealer.DrawFromDeck() : _dealer.DrawFromPile();
    }

    private void PlayerDiscards(Player player)
    {
        Card discard = player.DiscardFromHand();
        _dealer.AddToPile(discard);
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