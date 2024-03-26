using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafSpawner : MonoBehaviour
{

    public GameObject leafPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator SpawnLeaf()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
        }
    }
}
