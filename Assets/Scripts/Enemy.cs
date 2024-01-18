using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    // FIXME, make these configurable
    [SerializeField] private float _speed = 4.0f;
    
    void Start()
    {
        
    }

    void Update()
    {
        // get our screen position in pixels
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        Vector3 bottomLeftOfScreen = new Vector3(0f, 0f, 0f);
        Vector3 leftBottomOfScreen = Camera.main.ScreenToWorldPoint(bottomLeftOfScreen);
        Vector3 rightTopOfScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        
        transform.Translate(Vector3.down * (_speed * Time.deltaTime));

        if (screenPos.y <= 0) // wrap moving down off bottom edge
        {
            float randomX = Random.Range(leftBottomOfScreen.x, rightTopOfScreen.x);
            //Debug.Log($"ENEMY: wrap down, screen pos y: {screenPos.y} bottom: 0, to {randomX}, {rightTopOfScreen.y}");
            transform.position = new Vector3(randomX, rightTopOfScreen.y, 0f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy hit Player");
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            Destroy(this.gameObject);
            
        } else if (other.CompareTag("Laser"))
        {
            Debug.Log("Laser hit Enemy");
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        } else
        {
            Debug.Log($"Enemy hit by {other.transform.name}");
        }
    }
}
