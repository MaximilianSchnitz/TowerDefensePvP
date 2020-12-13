using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public Vector2 spawnPoint;

    public static List<Dictionary<int, int>> waves = new List<Dictionary<int, int>>();

    // Start is called before the first frame update
    void Start()
    {
        //Wave 1
        var wave1 = new Dictionary<int, int>();
        wave1.Add(0, 2);
        wave1.Add(1, 1);
        wave1.Add(2, 0);
        wave1.Add(3, 0);

        var wave2 = new Dictionary<int, int>();
        wave2.Add(0, 2);
        wave2.Add(1, 1);
        wave2.Add(2, 1);
        wave2.Add(3, 0);

        var wave3 = new Dictionary<int, int>();
        wave3.Add(0, 5);
        wave3.Add(1, 4);
        wave3.Add(2, 1);
        wave3.Add(3, 0);

        var wave4 = new Dictionary<int, int>();
        wave4.Add(0, 7);
        wave4.Add(1, 5);
        wave4.Add(2, 2);
        wave4.Add(3, 2);

        var wave5 = new Dictionary<int, int>();
        wave5.Add(0, 11);
        wave5.Add(1, 11);
        wave5.Add(2, 4);
        wave5.Add(3, 0);

        var wave6 = new Dictionary<int, int>();
        wave6.Add(0, 5);
        wave6.Add(1, 15);
        wave6.Add(2, 4);
        wave6.Add(3, 3);

        var wave7 = new Dictionary<int, int>();
        wave7.Add(0, 11);
        wave7.Add(1, 20);
        wave7.Add(2, 3);
        wave7.Add(3, 1);

        var wave8 = new Dictionary<int, int>();
        wave8.Add(0, 0);
        wave8.Add(1, 5);
        wave8.Add(2, 2);
        wave8.Add(3, 10);

        var wave9 = new Dictionary<int, int>();
        wave9.Add(0, 11);
        wave9.Add(1, 11);
        wave9.Add(2, 5);
        wave9.Add(3, 0);

        var wave10 = new Dictionary<int, int>();
        wave10.Add(0, 5);
        wave10.Add(1, 5);
        wave10.Add(2, 10);
        wave10.Add(3, 15);

        waves.Add(wave1);
        waves.Add(wave2);
        waves.Add(wave3);
        waves.Add(wave4);
        waves.Add(wave5);
    }

    bool waveRunning = false;

    float checkCooldown;
    float checkCooldownLength = 2;

    float spawnCooldown;
    public float spawnCooldownLength;

    int currentWave = 0;
    int currentQueuePos = 0;

    List<GameObject> enemyQueue = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        if (waveRunning)
        {
            if (spawnCooldown <= 0)
            {
                if (currentQueuePos < enemyQueue.Count)
                {
                    Instantiate(enemyQueue[currentQueuePos], spawnPoint, Quaternion.identity);
                    currentQueuePos++;
                }

                spawnCooldown = spawnCooldownLength;
            }
            else
            {
                spawnCooldown -= Time.deltaTime;
            }

            if (checkCooldown <= 0)
            {
                var enemys = GameObject.FindGameObjectsWithTag("Character");
                if (enemys.Length <= 0)
                    waveRunning = false;

                checkCooldown = checkCooldownLength;
            }
            else
            {
                checkCooldown -= Time.deltaTime;
            }

        }
        else
        {
            var slimes = waves[currentWave][0];
            var goblins = waves[currentWave][1];
            var dwarfs = waves[currentWave][2];
            var ogres = waves[currentWave][3];

            for (int j = 0; j < slimes; j++)
                enemyQueue.Add(GetEnemyFromIndex(0));
            for (int j = 0; j < goblins; j++)
                enemyQueue.Add(GetEnemyFromIndex(1));
            for (int j = 0; j < dwarfs; j++)
                enemyQueue.Add(GetEnemyFromIndex(2));
            for (int j = 0; j < ogres; j++)
                enemyQueue.Add(GetEnemyFromIndex(3));

            Debug.Log(enemyQueue.Count);

            waveRunning = true;

            spawnCooldownLength -= currentWave / 100;

            if(waves.Count - 1 > currentWave)
                currentWave++;
        }

    }

    private GameObject GetEnemyFromIndex(int i)
    {
        switch(i)
        {
            case 0:
                return Resources.Load("Slime") as GameObject;
            case 1:
                return Resources.Load("Goblin") as GameObject;
            case 2:
                return Resources.Load("Dwarf") as GameObject;
            case 3:
                return Resources.Load("Ogre") as GameObject;
            default:
                return null;
        }
    }
}
