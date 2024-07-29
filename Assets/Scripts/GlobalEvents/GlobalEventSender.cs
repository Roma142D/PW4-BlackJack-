using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RomanDoliba.Events
{
    public class GlobalEventSender : MonoBehaviour
    {
        public static Action<string> OnEvent;

        public static void FireEvent(string eventName)
        {
            OnEvent?.Invoke(eventName);
        }
    }
}
