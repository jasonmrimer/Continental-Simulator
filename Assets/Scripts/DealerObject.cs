using System;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.Serialization;

public class DealerObject : MonoBehaviour
{
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
    private Dictionary<Player, GameObject> playerToObjectDict;

    // Start is called before the first frame update
    void Start()
    {
        _deck = new Deck();

        _players = PlayerStub.CreatePlayers();
        
        _dealer = new Dealer(_deck, _players);
        //
        // _playerObjects = new List<GameObject>(4)
        // {
        //     P1_Hand,
        //     P2_Hand,
        //     P3_Hand,
        //     P4_Hand
        // };

        playerToObjectDict = new Dictionary<Player, GameObject>();

        playerToObjectDict.Add(_players[0], P1_Hand);
        playerToObjectDict.Add(_players[1], P2_Hand);
        playerToObjectDict.Add(_players[2], P3_Hand);
        playerToObjectDict.Add(_players[3], P4_Hand);
    }

    public void OnClick()
    {
        _dealer.Deal();
        TranslateControlToCanvas();
    }

    private void TranslateControlToCanvas()
    {
        foreach (Player player in playerToObjectDict.Keys)
        {
            GameObject gameHand = playerToObjectDict[player];
            
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

    public List<GameObject> GetPlayers()
    {
        return this._playerObjects;
    }
}