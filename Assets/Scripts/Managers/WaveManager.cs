using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviourSingleton<WaveManager>
{
    public int CurrentDifficultyValue;
    public int CurrentlyPresentDifficulty;
    public float DynamicPercentage;
    public int EnemyCount = 0;
    [SerializeField] private float waitTimeBeforeWave = 6f;
    [SerializeField] private float waitTimeAfterWave = 2f;
    [SerializeField] private float timeBetweenEnemiesInWave = 1f;
    [SerializeField] private float timeBetweenEnemiesInBatchSpawn = 0.3f;

    [Header("Bubbles")]
    [SerializeField] private int bubblesSpawnedPerWave;
    [SerializeField] private List<Transform> bubbleSpawnPoints;
    [SerializeField] private float bubbleSpawnMinForce;
    [SerializeField] private float bubbleSpawnMaxForce;
    [SerializeField] private float bubbleSpawnMinOxygen;
    [SerializeField] private float bubbleSpawnMaxOxygen;
    [SerializeField] private Bubble bubblePrefab;
    [SerializeField] private List<Enemy> allEnemies;
    [SerializeField] private List<Transform> _walkerSpawnPositions;
    [SerializeField] private List<Transform> _swimmerSpawnPositions;
    [SerializeField] private List<Transform> _shooterSpawnPositions;
    [SerializeField] private BubbleManager bubbleManager;
    [SerializeField] private List<AudioClip> newWaveClip;
    [SerializeField] private List<AudioClip> endWaveClip;
    private List<Enemy> _enemies = new();
    private List<int> _enemyDifficultyValues = new();
    private int _previousDifficultyValue;
    private int _remainingDifficultyInWave;
    private int _currentWave;
    private bool _isSpawingWave;
    private bool _finishedSpawningWave;



    private void Start()
    {
        if (bubbleManager == null)
        {
            bubbleManager = FindAnyObjectByType<BubbleManager>();
        }

        SetupEnemies();
        CurrentDifficultyValue = 1;
        _previousDifficultyValue = 1;
        StartCoroutine(WaitCoroutine());
    }

    private void Update()
    {
        if (_finishedSpawningWave && DynamicPercentage <= 0.1f)
        {
            _finishedSpawningWave = false;
            StartCoroutine(WaitCoroutine());
        }
    }

    private void SetupEnemies()
    {
        foreach (var enemy in allEnemies)
        {
            if (enemy.DifficultyValue <= 1)
            {
                _enemies.Add(enemy);
                _enemyDifficultyValues.Add(enemy.DifficultyValue);
            }
        }
    }

    private void CheckNewEnemies()
    {
        foreach (var enemy in allEnemies)
        {
            if (enemy.FirstAppearance == _currentWave && !_enemies.Contains(enemy))
            {
                // print("added new enemy: " + enemy.name);
                _enemies.Add(enemy);
                _enemyDifficultyValues.Add(enemy.DifficultyValue);
            }
        }
    }

    private IEnumerator WaitCoroutine()
    {
        if (_currentWave > 0)
        {
            MyAudioManager.Instance?.PlayEndWave();
        }

        // print("wait Coroutine " + _currentWave);
        // MyAudioManager.Instance.SetMusic(5, 0, 3);
        yield return new WaitForSeconds(waitTimeAfterWave);
        // print("start wait phase");
        if (_currentWave > 0)
        {
            // print("bubbles");
            for (int i = 0; i < bubblesSpawnedPerWave; i++)
            {
                Transform bubbleSpawnPoint = bubbleSpawnPoints[Random.Range(0, bubbleSpawnPoints.Count)];
                Bubble newBubble = Instantiate(bubblePrefab, bubbleSpawnPoint.position, bubblePrefab.transform.rotation);
                newBubble.Oxygen = Random.Range(bubbleSpawnMinOxygen, bubbleSpawnMaxOxygen);
                var shootdirection = bubbleSpawnPoint.up;
                newBubble.AddForce(shootdirection * Random.Range(bubbleSpawnMinForce, bubbleSpawnMaxForce));
                newBubble.InitializeSpawnedBubble(bubbleManager.DecreaseRate / 2.5f, bubbleManager.MinValue / 1.5f);
                yield return new WaitForSeconds(Random.Range(0.3f, 0.7f));
            }
        }

        StartCoroutine(PreWaveCoroutine());
    }

    private IEnumerator PreWaveCoroutine()
    {
        // print("pre wave Coroutine " + _currentWave);
        yield return new WaitForSeconds(waitTimeBeforeWave);
        CurrentlyPresentDifficulty = 0;
        AddDynamicDifficulty(0, 0);
        StartCoroutine(WaveCoroutine());
    }


    private IEnumerator WaveCoroutine()
    {
        // print("wave Coroutine " + _currentWave); 
        _currentWave++;
        // print("start wave #" + _currentWave);
        // MyAudioManager.Instance.SetMusic(5, 1, 5);
        CheckNewEnemies();
        Player.Instance.SetWave(_currentWave);
        MyAudioManager.Instance?.PlayClip(newWaveClip);

        // spawn enemies
        _isSpawingWave = true;
        _remainingDifficultyInWave = CurrentDifficultyValue;
        while (_isSpawingWave)
        {
            yield return new WaitForSeconds(timeBetweenEnemiesInWave);
            int numberOfSpawnedEnemies = Random.Range(1, Mathf.CeilToInt(_currentWave / 2f));
            for (int i = 0; i < numberOfSpawnedEnemies; i++)
            {
                if (_isSpawingWave)
                {
                    _remainingDifficultyInWave -= SpawnEnemy();
                    yield return new WaitForSeconds(timeBetweenEnemiesInBatchSpawn);
                }
            }
        }

        // increase difficulty value after the wave ends
        _finishedSpawningWave = true;
        int previousValue = CurrentDifficultyValue;
        CurrentDifficultyValue += _previousDifficultyValue;
        _previousDifficultyValue = previousValue;
    }

    private int SpawnEnemy()
    {
        // returns difficulty value
        // print("spawn enemy");
        Enemy randomEnemy = _enemies[Random.Range(0, _enemies.Count)];
        if (_remainingDifficultyInWave < randomEnemy.DifficultyValue)
        {
            CurrentDifficultyValue += randomEnemy.DifficultyValue - _remainingDifficultyInWave;
            _isSpawingWave = false;
        }
        Instantiate(randomEnemy, GetSpawnPosition(randomEnemy.EnemyType), randomEnemy.transform.rotation);

        return randomEnemy.DifficultyValue;
    }

    private Vector3 GetSpawnPosition(EnemyType enemy)
    {
        switch (enemy)
        {
            case EnemyType.Walker:
                return _walkerSpawnPositions[Random.Range(0, _walkerSpawnPositions.Count)].position
                + Vector3.right * Random.Range(-1.5f, 1.5f);
            case EnemyType.Swimmer:
                return _swimmerSpawnPositions[Random.Range(0, _swimmerSpawnPositions.Count)].position
                + Vector3.right * Random.Range(-2.5f, 2.5f)
                + Vector3.up * Random.Range(-1.5f, 1.5f);
            case EnemyType.Shooter:
                return _shooterSpawnPositions[Random.Range(0, _shooterSpawnPositions.Count)].position
                + Vector3.right * Random.Range(-2.5f, 2.5f)
                + Vector3.up * Random.Range(-1.5f, 1.5f);
        }

        return _walkerSpawnPositions[Random.Range(0, _walkerSpawnPositions.Count)].position
                + Vector3.right * Random.Range(-1.5f, 1.5f);
    }

    public void AddDynamicDifficulty(int value, int enemyCount)
    {
        CurrentlyPresentDifficulty += value;
        EnemyCount += enemyCount;

        // print(CurrentlyPresentDifficulty + " / " + CurrentDifficultyValue);
        // DynamicPercentage = (float)CurrentlyPresentDifficulty / (float)CurrentDifficultyValue;
        DynamicPercentage = (float)EnemyCount / 5f;

        if (MyAudioManager.Instance != null)
        {
            MyAudioManager.Instance.OnDynamicValueUpdate(DynamicPercentage);
        }
    }
}

