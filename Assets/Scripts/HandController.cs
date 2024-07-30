using System.Collections;
using System.Collections.Generic;
using RomanDoliba.Cards;
using UnityEngine;
using UnityEngine.UI;

namespace RomanDoliba.Hands
{
    public class HandController : MonoBehaviour
    {
        private List<CardObject> _cardsInHand;
        private int _cardsValue = 0;
        private Coroutine _cardsMovingCoroutine;
        public int Result {get => _cardsValue;}
        
        //TEST
        [SerializeField] private DeckController _deck;
        


        private void Awake()
        {
            _cardsInHand = new List<CardObject>();
            _cardsMovingCoroutine = null;

            ClearHand();
        }
                       
        public void TakeCard(CardObject card)
        {
            if (card.CardData.Name == "Ace" && _cardsValue > 10)
            {
                card.CardData.Value = 1;
            }
            _cardsInHand.Add(card);
            var moveToPosition = this.transform.localPosition;
            StartCoroutine(PositionCardInHand(card, moveToPosition));
            _cardsValue += card.CardData.Value;
        }
        public void ChangeCardsColor(Color color)
        {
            foreach (var card in _cardsInHand)
            {
                card.SpriteRenderer.color = color;
            }
        }
        
        private IEnumerator PositionCardInHand(CardObject card, Vector3 endPosition)
        {
            var nextPosition = card.transform.localPosition;
            nextPosition.x -= 2f;
            _cardsMovingCoroutine = StartCoroutine(MoveCard(card, nextPosition, 0.25f, true));
            while (_cardsMovingCoroutine != null)
            {
                yield return new WaitForEndOfFrame();
            }
            _cardsMovingCoroutine = StartCoroutine(MoveCard(card, endPosition, 0.25f, true));
            while (_cardsMovingCoroutine != null)
            {
                yield return new WaitForEndOfFrame();
            }
            
            card.transform.SetParent(this.transform);

            if (_cardsInHand.Count == 2)
            {
                nextPosition = _cardsInHand[1].transform.localPosition;
                nextPosition.x -= 1.2f;
                _cardsMovingCoroutine = StartCoroutine(MoveCard(_cardsInHand[1], nextPosition, 0.25f, true));
                while (_cardsMovingCoroutine != null)
                {
                    yield return new WaitForEndOfFrame();
                }
            }
            else if (_cardsInHand.Count > 2)
            {
                var lastCard = _cardsInHand[_cardsInHand.Count - 1];
                nextPosition = _cardsInHand[_cardsInHand.Count - 2].transform.localPosition;
                nextPosition.x -= 1.2f;
                _cardsMovingCoroutine = StartCoroutine(MoveCard(lastCard, nextPosition, 0.25f, true));
                while (_cardsMovingCoroutine != null)
                {
                    yield return new WaitForEndOfFrame();
                }
            }
        }
        private IEnumerator MoveCard(CardObject card, Vector3 moveToPosition, float duration, bool waitRoutine)
        {
            var currentTime = 0f;
            var deltaTime = 0f;
            var endTime = 1f;
            var startPosition = card.transform.localPosition;

            while (deltaTime != duration)
            {
                card.transform.localPosition = Vector3.Lerp(startPosition, moveToPosition, currentTime);
                deltaTime = Mathf.Min(duration, deltaTime + Time.deltaTime);
                currentTime = Mathf.Min(endTime, (endTime * deltaTime) / duration);

                yield return new WaitForEndOfFrame();
            }

            if (waitRoutine)
            {
                _cardsMovingCoroutine = null;
            }
        }
        private void ClearHand()
        {
            if (_cardsInHand.Count != 0)
            {
                foreach (var card in _cardsInHand)
                {
                    Destroy(card.gameObject);
                }
            }
            _cardsInHand.Clear();
        }
    }
}
