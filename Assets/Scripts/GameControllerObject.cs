using System;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.Serialization;

public class GameControllerObject : MonoBehaviour
{
    GameController _gameController;
    public GameObject Card_Prefab;
    public GameObject ValueText;
    public GameObject SuitText;
    public GameObject P1_Hand;
    public GameObject P2_Hand;
    public GameObject P3_Hand;
    public GameObject P4_Hand;
    private List<GameObject> _playerObjects;
    private List<Player> _players;
    private Dealer _dealer;
    private Deck _deck;
    private Dictionary<Player, GameObject> _playerToObjectDict;

    // Start is called before the first frame update
    void Start()
    {
        _deck = new Deck();

        _players = PlayerStub.CreatePlayers();

        _dealer = new Dealer(_deck, _players);

        _gameController = new GameController(
            _dealer,
            _players
        );

        _playerToObjectDict = new Dictionary<Player, GameObject>
        {
            { _players[0], P1_Hand },
            { _players[1], P2_Hand },
            { _players[2], P3_Hand },
            { _players[3], P4_Hand }
        };
    }

    public void OnClick()
    {
        
    }
    
    public void DealClick()
    {
        _gameController.Deal();
        TranslateControlToCanvas();
    }

    public void PlayClick()
    {
        _gameController.Play();
    }

    private void TranslateControlToCanvas()
    {
        foreach (Player player in _playerToObjectDict.Keys)
        {
            GameObject gameHand = _playerToObjectDict[player];

            foreach (Card card in player.Hand())
            {
                GameObject cardObject = Instantiate(
                    Card_Prefab,
                    new Vector3(0, 0, 0),
                    Quaternion.identity);
                cardObject.transform.SetParent(gameHand.transform, false);
                CardVisual cardVisual = cardObject.GetComponent<CardVisual>();
                cardVisual.SetFront(card);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}