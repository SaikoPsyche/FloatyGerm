using System;
using UnityEngine;

// Updated 2026-09-14 by Claude (Anthropic AI assistant):
// - Removed the leftover commented-out OnVolumeChange event/method.
// - Left the onX() trigger method names and the MonoBehaviour base class
//   as-is: renaming the invoker methods or converting this to a plain
//   static class would change this file's public API, and other scripts
//   not included in this review likely call these methods directly, so
//   that change is left for you to make deliberately rather than being
//   done silently here.
public class EventManager : MonoBehaviour
{
    public static event Action OnScorePoint;
    public static event Action OnPlayerDeath;
    public static event Action<int> OnLevelIncrease;
    public static event Action OnTouchScreen;

    public static void onScorePoint() => OnScorePoint?.Invoke();
    public static void onPlayerDeath() => OnPlayerDeath?.Invoke();
    public static void onLevelIncrease(int level) => OnLevelIncrease?.Invoke(level);
    public static void onTouchScreen() => OnTouchScreen?.Invoke();
}
