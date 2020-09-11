using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameyManager : MonoBehaviour
{
    public static int levelResources = 0;
    public static float levelMana = 0;

    public static int resourcesMax;
    public static int manaMax;

    public static bool showStats = false;

    public enum GameState
    {
        Menu,
        Tutorial,
        Queue,
        Play,
        End
    }

    public static List<string> visitedLevels = new List<string>();

    public static GameState gameState;

    public static List<Transform> spawnedEnemies = new List<Transform>();

    public static int levelsCompleted = 0;

    public static int resourcesSpent = 0;

    public static int retries = 0;

    public static int spellsCast = 0;

    public static int damageTaken = 0;

    public static int[] enemiesDead = { 0, 0, 0, 0, 0 };
    public static int[] enemiesSpawned = { 0, 0, 0, 0, 0 };
    public static int[] enemiesReachedEnd = { 0, 0, 0, 0, 0 };

    public static float mainVolume = 0.4f;
}
