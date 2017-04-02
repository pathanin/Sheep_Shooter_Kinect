using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour {

    public GameObject explosion;
    public LayerMask m_SheepMask;
    public ParticleSystem m_ExplosionParticles;
    public ParticleSystem FireShrapnel;
    public AudioSource m_ExplosionAudio;
    public float m_ExplosionForce = 1000f;
    public float m_MaxLifeTime = 6f;
    public float m_ExplosionRadius = 50f;

    public int scoreValue;
    private GameController gameController;


    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
        GameObject gameCOntrollerObject = GameObject.FindWithTag("GameController");
        m_ExplosionAudio = GetComponent<AudioSource>();
        if (gameCOntrollerObject != null)
        {
            gameController = gameCOntrollerObject.GetComponent<GameController>();
        }
        else
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary" || other.tag == "Shot" || other.tag == "Player")
            return;
        // Find all the sheep in an area around the bolt and damage them.
        //Debug.Log("Heyy");
        int temp = 0;
        Collider[] colliders = Physics.OverlapSphere(other.transform.position, m_ExplosionRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            if (!targetRigidbody)
            {
                continue;
            }
            targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);
            //gameController.AddScore(scoreValue);
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(colliders[i].gameObject);
            Debug.Log("Destroy by Explosion");
            temp = i-1;
        }

        m_ExplosionParticles.transform.parent = null;
        m_ExplosionParticles.Play();
        // if has Audio
        //m_ExplosionAudio.Play();

        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);

        // second explosion particle
        FireShrapnel.transform.parent = null;
        FireShrapnel.Play();

        Destroy(FireShrapnel.gameObject, FireShrapnel.duration);
        Destroy(gameObject);
        //Destroy(other.gameObject);
        gameController.AddScore(scoreValue*temp);
        //Debug.Log("Destroy sheep22222");

    }
    
}
