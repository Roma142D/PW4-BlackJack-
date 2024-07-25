using System.Collections;
using System.Collections.Generic;
using RomanDoliba.Cards.Data;
using UnityEngine;

namespace RomanDoliba.Cards
{
    public class DeckController : MonoBehaviour
    {
        [SerializeField] private Deck _deck;
        [SerializeField] private CardObject _cardPrefab;
        private Stack<CardObject> _spawnedDeck;
        
        private void Awake()
        {
            _spawnedDeck = new Stack<CardObject>();
            SpawnDeck();
        }


        public void SpawnDeck()
        {

            List<Card> deck = _deck.InitDeck();

            foreach (var card in deck)
            {
                CardObject spawnedCard = Instantiate(_cardPrefab, this.transform.position, Quaternion.identity, this.transform);
                spawnedCard.SpriteRenderer.sprite = card.Sprite;
                spawnedCard.CardData = card;
                _spawnedDeck.Push(spawnedCard);
            }
        }
    }
}
