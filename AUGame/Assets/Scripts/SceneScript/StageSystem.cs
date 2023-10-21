using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSystem : MonoBehaviour
{
    public GameObject C1s;
    public GameObject T1s;
    public GameObject S1s;

    // Start is called before the first frame update
    void Start()
    {
        T1s.SetActive(false);
        S1s.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
