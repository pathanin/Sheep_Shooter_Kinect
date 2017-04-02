using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotRotator : MonoBehaviour {

    public float tumble;
    private Rigidbody rigidbody;
    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Vector3 point = new Vector3(0,0, UnityEngine.Random.Range(-0.9f,-0.5f ));
        rigidbody.angularVelocity = point * tumble;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
