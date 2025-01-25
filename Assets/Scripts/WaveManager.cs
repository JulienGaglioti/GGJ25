using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    // 1 1 2 3 5 8 13 21     
    [SerializeField] private float timeBetweenTwoWaves = 5f;
    [SerializeField] private float timeBetweenEnemiesInWave = 1f;
    [SerializeField] private List<Enemy> allEnemies;
    [SerializeField] private Transform _spawnPointTest;
    private List<Enemy> _enemies = new();
    private List<int> _enemyDifficultyValues = new();
    private int _currentDifficultyValue;
    private int _previousDifficultyValue;
    private int _remainingDifficultyInWave;
    private int _currentWave;
    private bool _isSpawingWave;

    private void Start() 
    {
        SetupEnemies();
        _currentDifficultyValue = 1;
        _previousDifficultyValue = 1;
        StartCoroutine(WaitCoroutine());
    }

    private void SetupEnemies()
    {
        foreach (var enemy in allEnemies)
        {
            if(enemy.DifficultyValue <= 1)
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
            if(enemy.FirstAppearance == _currentWave && !_enemies.Contains(enemy))
            {
                print("added new enemy: " + enemy.name);
                _enemies.Add(enemy);
                _enemyDifficultyValues.Add(enemy.DifficultyValue);
            }            
        }
    }

    private IEnumerator WaitCoroutine()
    {
        // print("start wait phase");
        yield return new WaitForSeconds(timeBetweenTwoWaves);
        StartCoroutine(WaveCoroutine());
    }

    private IEnumerator WaveCoroutine()
    {        
        _currentWave++;
        print("start wave #" + _currentWave);
        CheckNewEnemies();

        _isSpawingWave = true;
        _remainingDifficultyInWave = _currentDifficultyValue;
        while(_isSpawingWave)
        {
            yield return new WaitForSeconds(timeBetweenEnemiesInWave);
            int numberOfSpawnedEnemies = Random.Range(1, Mathf.CeilToInt(_currentWave/2f));
            for (int i = 0; i < numberOfSpawnedEnemies; i++)
            {
                if(_isSpawingWave)
                {
                    _remainingDifficultyInWave -= SpawnEnemy();
                }                
            }            
        }

        // increase difficulty value after the wave ends
        int previousValue = _currentDifficultyValue;
        _currentDifficultyValue += _previousDifficultyValue;
        _previousDifficultyValue = previousValue;

        
        StartCoroutine(WaitCoroutine());
    }

    private int SpawnEnemy()
    {
        // returns difficulty value
        // print("spawn enemy");
        Enemy randomEnemy = _enemies[Random.Range(0, _enemies.Count)];
        Instantiate(randomEnemy, _spawnPointTest.position, randomEnemy.transform.rotation);

        if(_remainingDifficultyInWave < randomEnemy.DifficultyValue)
        {
            _isSpawingWave = false;
        }

        // int randomDifficultyValue = _enemyDifficultyValues[Random.Range(0, _enemyDifficultyValues.Count)];
        // if(_remainingDifficultyInWave < 0)
        // {
        //     _isSpawingWave = false;
        // }
        return randomEnemy.DifficultyValue;
    }
}
