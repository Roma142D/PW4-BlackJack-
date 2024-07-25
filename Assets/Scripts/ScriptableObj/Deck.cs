using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RomanDoliba.Cards.Data
{
    [CreateAssetMenu(fileName = "Deck", menuName = "Cards/Deck")]
    public class Deck : ScriptableObject
    {
        private List<Card> _deck;
        [SerializeField] private Card[] _hearts;
        [SerializeField] private List<Card> _pikes;
        [SerializeField] private List<Card> _clovers;
        [SerializeField] private List<Card> _tiles;        

        public List<Card> InitDeck()
        {
            List<Card> _deck = new List<Card>();

            _deck.AddRange(_hearts);
            _deck.AddRange(_pikes);
            _deck.AddRange(_clovers);
            _deck.AddRange(_tiles);

            Shuffle(_deck);
            
            return _deck;
        }

        private void Shuffle(List<Card> cards)
        {
            System.Random ran = new System.Random();
            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int r = ran.Next(n + 1);
                Card card = cards[r];
                cards[r] = cards[n];
                cards[n] = card;
            }
        }
    }
}
