using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RomanDoliba.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance{get; private set;}
        [Header("InGameScreen")]
        [Space]
        [SerializeField] private TextMeshProUGUI _botScore;
        [SerializeField] private TextMeshProUGUI _playerScore;
        [SerializeField] private Button _takeCardBtn;
        [SerializeField] private Button _passBtn;


        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void ChangeBotScore(int value)
        {
            _botScore.SetText($"B: {value}");
        }
        public void ChangePlayerScore(int value)
        {
            _playerScore.SetText($"P: {value}");
        }
    }
}
