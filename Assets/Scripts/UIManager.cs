using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RomanDoliba.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance{get; private set;}
        [Header("InGameScreen")]
        [Space]
        [SerializeField] private GameObject _inGameScreen;
        [SerializeField] private TextMeshProUGUI _botScore;
        [SerializeField] private TextMeshProUGUI _playerScore;
        [SerializeField] private Button _takeCardBtn;
        [SerializeField] private Button _passBtn;
        [Header("OnGameOverScreen")]
        [Space]
        [SerializeField] private GameObject _onGameOverScreen;
        [SerializeField] private TextMeshProUGUI _mainText;
        [SerializeField] private TextMeshProUGUI _botResult;
        [SerializeField] private TextMeshProUGUI _playerResult;
        [SerializeField] private Button _restartButton;

        private void Awake()
        {
            GetInstance();
        }
        public UIManager GetInstance()
        {
            if (Instance == null)
            {
                Instance = this;
                return Instance;
            }
            else
            {
                return Instance;
            }
        }
        public void OnGameStart()
        {
            _inGameScreen.SetActive(true);
            _onGameOverScreen.SetActive(false);
        }
        public void OnGameOver(int playerScore, int botScore)
        {
            _inGameScreen.SetActive(false);
            _onGameOverScreen.SetActive(true);
            _botResult.SetText($"B: {botScore}");
            _playerResult.SetText($"P: {playerScore}");
        }
        public void ChangeBotScore(int value)
        {
            _botScore.SetText($"B: {value}");
        }
        public void ChangePlayerScore(int value)
        {
            _playerScore.SetText($"P: {value}");
        }
        public void AddListenerOnTakeBtn(UnityAction action)
        {
            _takeCardBtn.onClick.AddListener(action);
        }
        public void AddListenerOnPassBtn(UnityAction action)
        {
            _passBtn.onClick.AddListener(action);
        }
        public void AddListenerOnRestartBtn(UnityAction action)
        {
            _restartButton.onClick.AddListener(action);
        }
        public void SetTextOnGameOver(string text)
        {
            _mainText.SetText($"{text}");
        }
    }
}
