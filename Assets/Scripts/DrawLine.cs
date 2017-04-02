using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour {

	public IndexFingerTracker tracker;
	private LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.SetPosition (0, transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		List<Vector3> points = tracker.GetRenderPoints ();

		lineRenderer.numPositions = points.Count;

		for (int i = 0; i < points.Count; i++) {
			lineRenderer.SetPosition (i, points[i] / 100);
		}
	}
}
