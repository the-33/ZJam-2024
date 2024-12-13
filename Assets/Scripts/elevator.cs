using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator : MonoBehaviour
{
    public GameObject door1;
    public GameObject door2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator openDoors()
    {
        yield return new WaitForSeconds(Random.Range(0, 16));

        door1.SetActive(false);
        door2.SetActive(false);

        gameObject.layer = 6;
    }
}
