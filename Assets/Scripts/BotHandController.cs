using System;
using System.Collections;
using RomanDoliba.Cards;
using RomanDoliba.Events;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace RomanDoliba.Hands
{
    public class BotHandController : HandController
    {
        private bool _isBotTurn;
        private bool _isBotPass;
        public bool IsBotTurn => _isBotTurn;
        public bool IsBotPass => _isBotPass;
        private void Start()
        {
            GlobalEventSender.OnEvent += OnBotTurn;
            
        }
        public void BotTakeCard(CardObject card)
        {
            if (_isBotTurn && !_isBotPass)
            {
                StartCoroutine(BotTurnRoutine(EndBotTurn, card));
                
                if (Result > 18)
                {
                    _isBotPass = true;
                }
                
            }
        }
        
        private IEnumerator BotTurnRoutine(UnityAction action, CardObject card)
        {
            while (_isBotTurn)
            {
                switch (Result)
                {
                    case <= 15 : 
                    {
                        TakeCard(card);
                        Debug.Log("BotTakeCard<=15");
                        break;
                    }
                    case >= 19 :
                    {
                        _isBotPass = true;
                        Debug.Log("BotPass>=19");
                        break;
                    }
                    case >= 15 :
                    {
                        var rnd = Random.Range(0, 2);
                        if (rnd == 1)
                        {
                            TakeCard(card);
                            Debug.Log("BotTakeCard>=15");
                            break;
                        }
                        else
                        {
                            _isBotPass = true;
                            Debug.Log("BotPass>=15");
                            break;
                        }
                    }
                }

                yield return new WaitForSeconds(2);
                action.Invoke();
            }
        }
        private void EndBotTurn()
        {
            _isBotTurn = false;
        }
        private void OnBotTurn(string eventName)
        {
            if (eventName == GlobalEventData.BOT_TURN)
            {
                _isBotTurn = true;
            }
        }
    }
}
