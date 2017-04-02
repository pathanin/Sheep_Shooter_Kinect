using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using Leap;
using Leap.Unity;

using PDollarGestureRecognizer;

public class IndexFingerTracker : MonoBehaviour {

	public GameObject IndexFingerBone3;
	public Camera camera;
	public float delayInterval = .15f;

	private IEnumerator _drawingCoroutine;
	private List<Gesture> trainingSet = new List<Gesture> ();
	private List<Point> points = new List<Point> ();
	private List<Vector3> renderPoints = new List<Vector3> ();
	private int strokeId;

	// Use this for initialization
	void Awake() {
		_drawingCoroutine = SpawnDrawingLine ();
	}

	void Start () {
		// camera = GetComponent<Camera> ();
		strokeId = -1;

		//Load pre-made gestures
		TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/10-stylus-MEDIUM/");
		foreach (TextAsset gestureXml in gesturesXml) {
			trainingSet.Add (GestureIO.ReadGestureFromXML (gestureXml.text));
		}
	}

	public void StartDrawing() {
		StartCoroutine (_drawingCoroutine);
	}

	public void StopDrawing() {
		StopAllCoroutines ();

		if (points.Count > 0) {
			++strokeId;
			// Gesture candidate = new Gesture(points.ToArray());
			// Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

			// Debug.Log (gestureResult.GestureClass + " " + gestureResult.Score);
		}

		points.Clear ();
		renderPoints.Clear ();
		strokeId = -1;
	}
	
	// Update is called once per frame
	private IEnumerator SpawnDrawingLine () {
		while (true) {
			renderPoints.Add (IndexFingerBone3.transform.position);
			points.Add (new Point (IndexFingerBone3.transform.position.x, -IndexFingerBone3.transform.position.y, strokeId));
			yield return new WaitForSeconds (delayInterval);
		}
	}

	public List<Vector3> GetRenderPoints() {
		return renderPoints;
	}
}
