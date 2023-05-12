using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Editor
{
    public class DealerTest
    {
        [Test]
        public void DealElevenCardsToFourPlayers()
        {
            List<Player> players = new List<Player>(4)
            {
                new(),
                new(),
                new(),
                new()
            };

            Dealer dealer = new Dealer(players);

            dealer.Deal();

            players.ForEach(AllCardsAndUnique);
        }

        private void AllCardsAndUnique(Player player)
        {
            Assert.AreEqual(11, player.CardCount());
            CollectionAssert.AllItemsAreUnique(player.getHand());
        }
    }
}