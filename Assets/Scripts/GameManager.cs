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
            _uiManeger.OnGameStart();
            _uiManeger.AddListenerOnTakeBtn(TakeCardOnButton);

            StartGame();
            CheckResults(_playerHand.Result, _botHand.Result);
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
        private void TakeCardOnButton()
        {
            var newCard = _deck.GiveCard();
            _playerHand.TakeCard(newCard);
            _uiManeger.ChangePlayerScore(_playerHand.Result);
            CheckResults(_playerHand.Result, _botHand.Result);
        }
        private void CheckResults(int playerScore, int botScore)
        {
            if (ResultCheck.IsBlackJack(playerScore) || ResultCheck.IsBlackJack(botScore))
            {
                _uiManeger.OnGameOver();
                if (ResultCheck.IsBlackJack(playerScore))
                {
                    _uiManeger.SetTextOnGameOver("You win");
                }
                else
                {
                    _uiManeger.SetTextOnGameOver("Bot win");
                }
            }
            else if (ResultCheck.IsTooMuch(playerScore) || ResultCheck.IsTooMuch(botScore))
            {
                _uiManeger.OnGameOver();
                if (ResultCheck.IsTooMuch(playerScore))
                {
                    _uiManeger.SetTextOnGameOver("You lose");
                }
                else
                {
                    _uiManeger.SetTextOnGameOver("Bot lose");
                }
            }
        }
    }
}
