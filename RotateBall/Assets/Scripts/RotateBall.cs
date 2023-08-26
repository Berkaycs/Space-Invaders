using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBall : MonoBehaviour
{
    public Vector3 donme;
    public Vector3 pozisyon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += pozisyon;
        transform.Rotate(donme);
    }
}
