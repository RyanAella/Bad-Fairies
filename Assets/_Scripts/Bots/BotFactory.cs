using _Scripts;
using _Scripts.Bots;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotFactory : MonoBehaviour
{
    // max number of bots in game
    [SerializeField] private int maxBotCount;
    // max number of bots simultaniously in game
    [SerializeField] private int maxIngameBotCount;

    // spwan locations
    // currently by Gameobject set in world
    [SerializeField] private List<Transform> spawnLocations;
    private int locationIndex = -1;

    // Bot prefab
    [SerializeField] private GameObject botPrefab;

    // currently existing bots
    private static List<GameObject> botList;

    // Start is called before the first frame update
    void Awake()
    {
        botList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager._gamePaused)
        {
            if (BotController.GetBotCounter() < maxBotCount)
            {
                // not all bots have been spawned yet
                if (botList.Count < maxIngameBotCount)
                {
                    // increment index by 1
                    locationIndex++;

                    // spawn bots
                    var spawnPos = spawnLocations[locationIndex].position;
                    SpawnBot(spawnPos);

                    // if last location reached reset to -1
                    if (locationIndex == spawnLocations.Count - 1) locationIndex = -1;
                }
            }
            else
            {
                // all bots have been spawned
            }
        }
    }

    private void SpawnBot(Vector3 spawnPosition)
    {
        // instantiate Bot on given location
        var bot = Instantiate(botPrefab, spawnPosition, Quaternion.identity);
        // add bot to botList
        botList.Add(bot);
    }

    public static void RemoveBotFromList(GameObject bot)
    {
        botList.Remove(bot);
    }
}
