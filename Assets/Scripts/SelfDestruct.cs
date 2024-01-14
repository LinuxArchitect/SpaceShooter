using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MySelfDestruct());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator MySelfDestruct()
    {
        yield return new WaitForSeconds(5f); // FIXME, make configurable
        Destroy(gameObject);
    }
}


