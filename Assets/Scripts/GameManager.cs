using RomanDoliba.Cards;
using RomanDoliba.Events;
using RomanDoliba.Hands;
using RomanDoliba.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RomanDoliba.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private DeckController _deck;
        [SerializeField] private HandController _playerHand;
        [SerializeField] private BotHandController _botHand;
        private UIManager _uiManeger;
        private bool _isPlayerPass;
       
        private void Start()
        {
            _uiManeger = UIManager.Instance;
            _uiManeger.AddListenerOnTakeBtn(TakeCardOnButton);
            _uiManeger.AddListenerOnPassBtn(PassOnButton);
            _uiManeger.AddListenerOnRestartBtn(RestartGame);          

            StartGame();
            CheckResults(_playerHand.Result, _botHand.Result);

            GlobalEventSender.OnEvent += BotTurn;
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
                _botHand.ChangeCardsColor(Color.red);
                Debug.Log("Player and Bot take card");
            }
            
            _uiManeger.ChangePlayerScore(_playerHand.Result);
            _uiManeger.OnGameStart();

            Debug.Log("cards are dealt");
        }
        private void BotTurn(string eventName)
        {
            if (eventName == GlobalEventData.BOT_TURN || eventName == GlobalEventData.PLAYER_PASS)
            {
                var newCard = _deck.GiveCard();
                _botHand.BotTakeCard(newCard);
                _botHand.ChangeCardsColor(Color.red);
                
                Debug.Log("BotTurn");
            }
            
            CheckResults(_playerHand.Result, _botHand.Result);
        }
        private void TakeCardOnButton()
        {
            if (!_botHand.IsBotTurn || _botHand.IsBotPass)
            {
                var newCard = _deck.GiveCard();
                _playerHand.TakeCard(newCard);
                _uiManeger.ChangePlayerScore(_playerHand.Result);
                CheckResults(_playerHand.Result, _botHand.Result);
                GlobalEventSender.FireEvent(GlobalEventData.BOT_TURN);

                Debug.Log("Player take card");
            }
        }
        private void PassOnButton()
        {
            GlobalEventSender.FireEvent(GlobalEventData.PLAYER_PASS);
            GlobalEventSender.FireEvent(GlobalEventData.BOT_TURN);
            _isPlayerPass = true;
            CheckResults(_playerHand.Result, _botHand.Result);

            Debug.Log("Player pass");
        }
        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        private void CheckResults(int playerScore, int botScore)
        {
            if (ResultCheck.IsBlackJack(playerScore) || ResultCheck.IsBlackJack(botScore))
            {
                _uiManeger.OnGameOver(playerScore, botScore);
                _botHand.ChangeCardsColor(Color.white);
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
                _uiManeger.OnGameOver(playerScore, botScore);
                _botHand.ChangeCardsColor(Color.white);
                if (ResultCheck.IsTooMuch(playerScore))
                {
                    _uiManeger.SetTextOnGameOver("You lose");
                }
                else
                {
                    _uiManeger.SetTextOnGameOver("Bot lose");
                }
            }
            else if (_isPlayerPass && _botHand.IsBotPass)
            {
                _uiManeger.OnGameOver(playerScore, botScore);
                _botHand.ChangeCardsColor(Color.white);
                if (ResultCheck.IsPlayerWin(playerScore, botScore))
                {
                    _uiManeger.SetTextOnGameOver("You win");
                }
                else
                {
                    _uiManeger.SetTextOnGameOver("Bot win");
                }
            }
            else if (playerScore == botScore)
            {
                _uiManeger.OnGameOver(playerScore, botScore);
                _uiManeger.SetTextOnGameOver("Tie");
                _botHand.ChangeCardsColor(Color.white);
            }
        }

    }
}
