using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RomanDoliba.Cards.Data
{
    [CreateAssetMenu(fileName = "Card", menuName = "Cards/Card")]
    
    public class Card : ScriptableObject
    {
        [SerializeField] private CardSuit _suit;
        [SerializeField] private string _name;
        [SerializeField] private Sprite _sprite;
        private int _cardValue;
        private string[] _names = new string[] {"2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace",};

        public string Name
        {
            get => _name;
            protected set => _name = value;
        }
        public int Value 
        {
            get => _cardValue; 
            set => _cardValue = value;
        }
        public CardSuit Suit => _suit;
        public Sprite Sprite => _sprite;

        public void SetValueCard(string name, CardSuit suit)
        {
            Name = name;
            _suit = suit;
            
            try
            {
                _cardValue = int.Parse(name);
            }
            catch 
            {
                if (name == "Jack" || name == "Queen" || name == "King")
                {
                    _cardValue = 10;
                }
                else if (name == "Ace")
                {
                    _cardValue = 11;
                }
            }
        }
            public enum CardSuit
        {
            Hearts,
            Pikes,
            Clovers,
            Tiles
        }
    }
}
