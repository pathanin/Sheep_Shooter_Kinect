using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBound : MonoBehaviour {

    private GameController gameController;

    void OnTriggerExit(Collider other){
        
		Destroy (other.gameObject);
	}
}
