using DigitalRuby.PyroParticles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bound
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	private Rigidbody rigidbody;
	private AudioSource audioSource;
    private float nextFire;
    private FireBaseScript currentPrefabScript;
    private GameObject currentPrefabObject;

    public float speed, tilt;
	public Bound bound;
	public GameObject shot;
    public GameObject explosiveShot;
    public Transform shotSpawn;
	public float fireRate;

    
    void Update(){
        //if (Input.GetButton("Fire1") && Time.time > nextFire) {
        if (Input.GetMouseButtonDown(0) && Time.time > nextFire)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            nextFire = Time.time + fireRate;
            Vector3 point = ray.origin + ray.direction*12;
            point.z -= 10;
            /*
            point.z = Camera.main.farClipPlane;
            point = Camera.main.ScreenToWorldPoint(point);
            point.Normalize();
            */
            Debug.Log("World point " + point);
            Instantiate(shot, point, shotSpawn.rotation);
            /*
            point.y += 0.1f;
            Instantiate(shot, point, shotSpawn.rotation);
            point.y -= 0.2f;
            Instantiate(shot, point, shotSpawn.rotation);
            //audioSource.Play();
            */
		}
        if (Input.GetMouseButtonDown(1) && Time.time > nextFire)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            nextFire = Time.time + fireRate;
            Vector3 point = ray.origin + ray.direction * 12;
            point.z -= 10;
            /*
            point.z = Camera.main.farClipPlane;
            point = Camera.main.ScreenToWorldPoint(point);
            point.Normalize();
            */
            Debug.Log("World point " + point);
            Instantiate(explosiveShot, point, shotSpawn.rotation);
            //audioSource2.Play();
        }
    }

	void FixedUpdate(){
		float moveHoriontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHoriontal, 0.0f, moveVertical);
		rigidbody.velocity = movement*speed;

		rigidbody.position = new Vector3 (
			Mathf.Clamp(rigidbody.position.x,bound.xMin,bound.xMax), 
			0.0f, 
			Mathf.Clamp(rigidbody.position.z,bound.zMin,bound.zMax)
		);

		rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);
	}

	void Start(){
		rigidbody = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> ();
        
    }
}
