using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;

public class Player : MonoBehaviour
{
    // FIXME, make these configurable
    [SerializeField] private float _speed = 10.0f;  
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _laserSpawnOffset = 0.8f; 
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private int _lives = 3;

    private float nextFireTime = 0f;
    private float nextSpawnTime = 3f;
    
    void Start()
    {
        //Debug.Log("Hello: " + gameObject.name);

        // set starting location
        transform.position = new Vector3(0f, 0f, 0f);
    }

    void Update()
    {
        ProcessWASD();
        ProcessSpaceKey();
        
        // temp spawn system for enemy
        if (Time.time > nextSpawnTime)
        {
            Vector3 bottomLeftOfScreen = new Vector3(0f, 0f, 0f);
            Vector3 leftBottomOfScreen = Camera.main.ScreenToWorldPoint(bottomLeftOfScreen);
            Vector3 rightTopOfScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
            float randomX = Random.Range(leftBottomOfScreen.x, rightTopOfScreen.x);
            var enemySpawnPosition = new Vector3(randomX, rightTopOfScreen.y, 0f);
            Instantiate(_enemyPrefab, enemySpawnPosition, Quaternion.identity);
            nextSpawnTime = Time.time + Random.Range(0.5f, 3f);
        }
    }

    void ProcessWASD()
    {
        // get user WASD input and player's new direction
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        Vector3 bottomLeftOfScreen = new Vector3(0f, 0f, 0f);

        // move according to user inputs
        transform.Translate(direction * (_speed * Time.deltaTime));
        
        // get our screen position in pixels
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        // get screen edges in world coordinates
        Vector3 rightTopOfScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        Vector3 leftBottomOfScreen = Camera.main.ScreenToWorldPoint(bottomLeftOfScreen);
        float worldLimitY = (rightTopOfScreen.y + leftBottomOfScreen.y) / 2.0f; // FIXME, make configurable, % of height
        float screenLimitY = Screen.height / 2.0f; // FIXME, make configurable, % of height
        
        // test all four screen edge boundaries, and reset world position as necessary
        if (screenPos.x <= 0 && direction.x < 0) // wrap moving left off left edge
        {
            //Debug.Log($"wrap left, screen pos x: {screenPos.x}, left edge: 0");
            transform.position = new Vector3(rightTopOfScreen.x, transform.position.y, 0f);
            
        } else if (screenPos.x >= Screen.width && direction.x > 0) // wrap moving right off right edge
        {
            //Debug.Log($"wrap right, screen pos x: {screenPos.x} right edge: {Screen.width}");
            transform.position = new Vector3(leftBottomOfScreen.x, transform.position.y, 0f);
            
        } else if (screenPos.y <= 0 && direction.y < 0) // limit moving down off bottom edge
        {
            //Debug.Log($"limit down, screen pos y: {screenPos.y} bottom: 0, direction: {direction}");
            transform.position = new Vector3(transform.position.x, leftBottomOfScreen.y, 0f);
            
        } else if (screenPos.y > screenLimitY && direction.y > 0) // limit moving up to midpoint of screen
        {
            //Debug.Log($"limit up, screen pos y: {screenPos.y} midpoint: {screenLimitY}");
            transform.position = new Vector3(transform.position.x, worldLimitY, 0f);
        }
    }

    void ProcessSpaceKey()
    {
        // Space key action
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFireTime)
        {
            var laserSpawnPosition = transform.position + new Vector3(0f, _laserSpawnOffset, 0f);
            Instantiate(_laserPrefab, laserSpawnPosition, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
        // REMOVEME
        if (Input.GetKeyDown(KeyCode.Period))
        {
            Vector3 bottomLeftOfScreen = new Vector3(0f, 0f, 0f);
            Vector3 leftBottomOfScreen = Camera.main.ScreenToWorldPoint(bottomLeftOfScreen);
            Vector3 rightTopOfScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
            float randomX = Random.Range(leftBottomOfScreen.x, rightTopOfScreen.x);
            var enemySpawnPosition = new Vector3(randomX, rightTopOfScreen.y, 0f);
            Instantiate(_enemyPrefab, enemySpawnPosition, Quaternion.identity);
        }
    }

    public void Damage()
    {
        _lives--;
        if (_lives < 1)
        {
            Debug.Log("Game Over");
            Destroy(this.gameObject);
        }
    }
}
