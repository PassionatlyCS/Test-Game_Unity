using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour
{

    public float speed = 11;
    public int RandomNumber;


    // Start is called before the first frame update
    void Start()
    {
        RandomNumber = Random.Range(1, (int)speed);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(Vector3.up * (RandomNumber * 0.1f) );
    }
}
