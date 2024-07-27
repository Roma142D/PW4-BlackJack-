using RomanDoliba.Cards;
using RomanDoliba.Hands;
using RomanDoliba.UI;
using UnityEngine;

namespace RomanDoliba.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private DeckController _deck;
        [SerializeField] private HandController _playerHand;
        [SerializeField] private HandController _botHand;
        private UIManager _uiManeger;
       
        private void Start()
        {
            _uiManeger = UIManager.Instance;
            
            StartGame();
        }


        private void StartGame()
        {
            _deck.SpawnDeck();
            for (int i = 0; i < 2; i++)
            {
                var card1 = _deck.GiveCard();
                var card2 = _deck.GiveCard();
                _playerHand.TakeCard(card1);
                _botHand.TakeCard(card2);
            }
            _uiManeger.ChangeBotScore(_botHand.Result);
            _uiManeger.ChangePlayerScore(_playerHand.Result);
        }
    }
}
