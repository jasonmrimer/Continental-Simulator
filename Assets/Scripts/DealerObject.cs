using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class DealerObject : MonoBehaviour
{
    public GameObject Card;
    public GameObject P1_Hand;
    public GameObject P2_Hand;
    public GameObject P3_Hand;
    public GameObject P4_Hand;
    private List<GameObject> _playersObjects;
    private List<Player> _players;

    public DealerObject(List<Player> players)
    {
        this._players = players;
    }

    // Start is called before the first frame update
    void Start()
    {
        _playersObjects = new List<GameObject>(4)
        {
            P1_Hand,
            P2_Hand,
            P3_Hand,
            P4_Hand
        };
    }

    public void OnClick()
    {
        _playersObjects.ForEach(dealStartingHand);
    }

    private void dealStartingHand(GameObject hand)
    {
        for (int i = 0; i < 11; i++)
        {
            GameObject card = Instantiate(
                Card,
                new Vector3(0, 0, 0),
                Quaternion.identity);
            card.transform.SetParent(hand.transform, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public List<GameObject> GetPlayers()
    {
        return this._playersObjects;
    }
}