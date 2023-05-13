using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class CardVisual : MonoBehaviour
    {
        public Text valueText;
        public Text suitText;

        public void SetFront(Card card)
        {
            valueText.text = card.ValueText();
            suitText.text = card.SuitSymbol();
            ChooseFontColor(card);
        }

        private void ChooseFontColor(Card card)
        {
            switch (card.suit)
            {
                case Suit.Hearts:
                case Suit.Diamonds:
                    valueText.color = Color.red;
                    suitText.color = Color.red;
                    break;
                case Suit.Clubs:
                case Suit.Spades:
                    valueText.color = Color.black;
                    suitText.color = Color.black;
                    break;
                case Suit.Wild:
                    valueText.color = Color.green;
                    suitText.color = Color.green;
                    break;
                default:
                    break;
            }
        }
    }
}