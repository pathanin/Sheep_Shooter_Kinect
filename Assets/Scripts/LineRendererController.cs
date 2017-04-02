using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererController : MonoBehaviour {

	private LineRenderer lineRenderer;
	private int vertexCount;

	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.numPositions = 0;
		vertexCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddPoint(Vector3 point) {
		++vertexCount;
		lineRenderer.numPositions = vertexCount;
		lineRenderer.SetPosition (vertexCount - 1, point);
	}
}
