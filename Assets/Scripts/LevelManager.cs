using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    // States
    public enum LevelState { Idle, DoingWave, InWave };
    public LevelState currentState;

    // Player and Colony Ship
    public GameObject playerPrefab;
    public GameObject colonyShipPrefab;
    public Transform playerSpawn;
    public Transform colonyShipSpawn;
    public FollowPlayer cam;

    // Destination
    public GameObject destination;

    // Lists
    public List<GameObject> enemyPrefabs;
    public List<Transform> enemySpawns;

    // Wave Information
    public int currentWave = 0;
    public int maxWaves;
    public bool waveSetupFinished = false;
    public bool doNextWave = false;

    // Store Player and Colony Ship Pawns
    public Pawn playerPawn;
    public Pawn colonyShipPawn;
    public List<AIController> enemies;

    // Private
    private int enemyCount = 0;

    // Awake event before Start can run
    private void Awake()
    {
        if (instance == null)
        {
            // This is the instance
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Spawn Player and Colony Ship
        SpawnEntity(playerPrefab, playerSpawn);
        SpawnEntity(colonyShipPrefab, colonyShipSpawn);

        // Set cam Follow Player
        if (cam != null)
        {
            PlayerController playerCtrl = playerPrefab.GetComponent<PlayerController>();
            cam.target = playerCtrl.pawn.transform;
        }

        // Set State to Idle
        currentState = LevelState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        // Check for Enemies
        CheckForEnemies();

        // Controls Waves
        MakeDecisions();
    }

    // Spawn Entity (Player, Col. Ship, or Enemy)
    public void SpawnEntity(GameObject prefab, Transform spawnPoint)
    {
        GameObject newEntity = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation) as GameObject; 
    }

    private void MakeDecisions()
    {
        switch (currentState)
        {
            // IDLE STATE
            case LevelState.Idle:
                // Set Wave Bools to False
                waveSetupFinished = false;
                doNextWave = false;

                // Check if Waves are in the Range
                if (currentWave <= maxWaves)
                {
                    // Set up the Wave
                    DoWaveSetup();

                    // Switch State
                    ChangeState(LevelState.DoingWave);
                }
                break;

            // DOING WAVE STATE
            case LevelState.DoingWave:
                if (waveSetupFinished)
                {
                    // Switch State
                    ChangeState(LevelState.InWave);
                }
                break;

            // IN WAVE STATE
            case LevelState.InWave:
                if (!doNextWave)
                {
                    // Finish the Wave
                    FinishWave();
                }
                // Switch to Idle and Do Next
                else
                {
                    // Switch State
                    ChangeState(LevelState.Idle);
                }
                break;
        }
    }

    // Change State
    public virtual void ChangeState(LevelState newState)
    {
        // Change Current State
        currentState = newState;
    }

    private void DoWaveSetup()
    {
        // If we on the Current Wave
        if (!waveSetupFinished)
        {
            // Spawn Initial Enemies
            for (int i = 0; i < enemySpawns.Count; i++)
            {
                GameObject prefab = enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Count)];
                SpawnEntity(prefab, enemySpawns[i]);
                enemyCount++;
            }

            // Finish the Wave
            waveSetupFinished = true;
        }
    }

    private void FinishWave()
    {
        if (enemies != null)
        {
            if (enemies.Count == 0)
            {
                // Increment Wave Number
                currentWave++; ;

                // Tell to do Next Wave
                doNextWave = true;
            }
        }
    }

    private void CheckForEnemies()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
        }
    }
}
