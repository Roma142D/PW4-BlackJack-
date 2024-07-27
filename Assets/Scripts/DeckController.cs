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
        private List<CardObject> _spawnedDeck;
        
        private void Awake()
        {
            _spawnedDeck = new List<CardObject>();
            SpawnDeck();
        }


        public void SpawnDeck()
        {

            List<Card> deck = _deck.InitDeck();

            foreach (var card in deck)
            {
                CardObject spawnedCard = Instantiate(_cardPrefab, this.transform.position, Quaternion.LookRotation(Vector3.down) , this.transform);
                spawnedCard.SpriteRenderer.sprite = card.Sprite;
                spawnedCard.CardData = card;
                _spawnedDeck.Add(spawnedCard);
            }
        }
        //TEST
        public CardObject GiveCard()
        {
            var nextCard = _spawnedDeck[0];
            _spawnedDeck.RemoveAt(0);
            return nextCard;
        }
    }
}
