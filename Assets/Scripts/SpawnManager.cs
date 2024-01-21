using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private IEnumerator _coroutine;
    [SerializeField] private float minSpawnDelay = 0.5f;
    [SerializeField] private float maxSpawnDelay = 3f;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    
    void Start()
    {
        _coroutine = SpawnEnemy(minSpawnDelay, maxSpawnDelay);
        StartCoroutine(_coroutine);
    }
    
    IEnumerator SpawnEnemy(float minDelay, float maxDelay)
    {
        Vector3 bottomLeftOfScreen = new Vector3(0f, 0f, 0f);
        Vector3 leftBottomOfScreen = Camera.main.ScreenToWorldPoint(bottomLeftOfScreen);
        Vector3 rightTopOfScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        var randomX = Random.Range(leftBottomOfScreen.x, rightTopOfScreen.x);
        var enemySpawnPosition = new Vector3(randomX, rightTopOfScreen.y, 0f);
        var waitTime = Random.Range(minDelay, maxDelay);

        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            var newEnemy = Instantiate(_enemyPrefab, enemySpawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
        }
    }

    public void OnPlayerDeath()
    {
        StopCoroutine(_coroutine);
        for (int i = 0; i < _enemyContainer.transform.childCount; i++)
        {
            GameObject child = _enemyContainer.transform.GetChild(i).gameObject;
            Destroy(child);
        }
    }

    public void OnRestart()
    {
        StartCoroutine(_coroutine);
    }
}
