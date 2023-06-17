using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static Action OnGameOverEvent;
    public static Action<int> OnPlayerScoreChangeEvent; //score
}