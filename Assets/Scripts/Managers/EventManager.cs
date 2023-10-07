using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action OnScorePoint;
    public static event Action OnPlayerDeath;
    public static event Action OnLevelIncrease;
    public static event Action OnTouchScreen;
    // public static event Action OnVolumeChange;

    public static void onScorePoint() => OnScorePoint?.Invoke();
    public static void onPlayerDeath() => OnPlayerDeath?.Invoke();
    public static void onLevelIncrease() => OnLevelIncrease?.Invoke();
    public static void onTouchScreen() => OnTouchScreen?.Invoke();
    // public static void onVolumeChange() => OnVolumeChange?.Invoke();
}
