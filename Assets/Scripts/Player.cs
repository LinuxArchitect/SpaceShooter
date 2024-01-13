using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;
    private Vector3 _bottomLeftOfScreen = new Vector3(0f, 0f, 0f);
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello: " + gameObject.name);
        
        // debug and remove
        // get screen edges
        Vector3 rightTopOfScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        float rightOfScreen = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x;
        Vector3 leftBottomOfScreen = Camera.main.ScreenToWorldPoint(_bottomLeftOfScreen);
        float worldLimitY = (rightTopOfScreen.y + leftBottomOfScreen.y) / 2;
        float screenLimitY = (float) Screen.height / 2;
        float worldStartX = (leftBottomOfScreen.x + rightTopOfScreen.x) / 2; // FIXME, make configurable
        float worldStartY = (rightTopOfScreen.y + leftBottomOfScreen.y) / 2; // FIXME, make configurable

        Debug.Log($"world edges left {leftBottomOfScreen.x}, bottom {leftBottomOfScreen.y}, right {rightTopOfScreen.x}, top {rightTopOfScreen.y}, mid {worldLimitY}");
        Debug.Log($"screen edges left 0, bottom 0, right {Screen.width}, top: {Screen.height}, mid {screenLimitY}");
        
        Debug.Log($"Resetting to start position {worldStartX}, {worldStartY}");
        // set starting location
        transform.position = new Vector3(worldStartX, worldStartY, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        // get user WASD input and player's new direction
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        
        // move according to user inputs
        transform.Translate(direction * (_speed * Time.deltaTime));
        
        // get our screen position in pixels
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        // get screen edges
        Vector3 rightTopOfScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        Vector3 leftBottomOfScreen = Camera.main.ScreenToWorldPoint(_bottomLeftOfScreen);
        float worldLimitY = (rightTopOfScreen.y + leftBottomOfScreen.y) / 2.0f; // FIXME, make configurable, % of height
        float screenLimitY = Screen.height / 2.0f; // FIXME, make configurable, % of height
        
        // screen edges left -8.886598, bottom -4, right 8.886598, top 6
        // wrap left, screen pos x: -0.7529109, left edge: 0
        // wrap right, screen pos x: 862.4395 right edge: 862
        // wrap down, screen pos y: -4.388822 bottom: -4
        // limit up, screen pos y: 79.4641 midpoint: 3

        // test all four screen edge boundaries, and reset world position as necessary
        if (screenPos.x <= 0 && direction.x < 0) // wrap moving left off left edge
        {
            Debug.Log($"wrap left, screen pos x: {screenPos.x}, left edge: 0");
            transform.position = new Vector3(rightTopOfScreen.x, transform.position.y, 0f);
            
        } else if (screenPos.x >= Screen.width && direction.x > 0) // wrap moving right off right edge
        {
            Debug.Log($"wrap right, screen pos x: {screenPos.x} right edge: {Screen.width}");
            transform.position = new Vector3(leftBottomOfScreen.x, transform.position.y, 0f);
            
        } else if (screenPos.y <= 0 && direction.y < 0) // wrap moving down off bottom edge to midpoint of screen
        {
            Debug.Log($"wrap down, screen pos y: {screenPos.y} bottom: 0");
            transform.position = new Vector3(transform.position.x, worldLimitY, 0f);
            
        } else if (screenPos.y > screenLimitY && direction.y > 0) // limit moving up to midpoint of screen
        {
            Debug.Log($"limit up, screen pos y: {screenPos.y} midpoint: {screenLimitY}");
            transform.position = new Vector3(transform.position.x, worldLimitY, 0f);
        }
    }
}
