using System.Collections;
using System.Collections.Generic;
using RomanDoliba.Cards.Data;
using UnityEngine;

namespace RomanDoliba.Cards
{
    public class CardObject : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Card _cardData;
        
        public Card CardData
        {
            get => _cardData;
            set
            {
                _cardData = value;
            }
        }
        public SpriteRenderer SpriteRenderer
        {
            get => _spriteRenderer;
            set
            {
                _spriteRenderer = value;
            }
        }

        private void Awake()
        {
            //_spriteRenderer.sprite = _cardData.Sprite;
        }
    }
}
