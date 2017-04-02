using DigitalRuby.PyroParticles;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using Leap;

using PDollarGestureRecognizer;

public class GestureManager : MonoBehaviour {
	
	public Transform gestureOnScreenPrefab;

	public GameObject mouseTracker;
	public Camera camera;
	private bool isDrawing;

	private List<Gesture> trainingSet = new List<Gesture>();

	private List<Point> points = new List<Point>();
	private int strokeId = -1;

	private List<LineRendererController> gestures = new List<LineRendererController>();
	private LineRendererController currentGesture;
	private List<Vector3> lineRendererVertexes;

	private string message;

    // Spawner
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

    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        lineRendererVertexes = new List<Vector3> ();
		// Load pre-made gestures
		// TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/10-stylus-MEDIUM/");
		// foreach (TextAsset gestureXml in gesturesXml)
		//	trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

		// Load user custom gestures
		TextAsset[] customGesturesXml = Resources.LoadAll<TextAsset>("GestureSet/custom/");
		foreach (TextAsset gestureXml in customGesturesXml)
			trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isDrawing) {
			if (currentGesture == null) {
				++strokeId;

				Transform tmpGesture = Instantiate (gestureOnScreenPrefab, transform.position, transform.rotation) as Transform;
				currentGesture = tmpGesture.GetComponent<LineRendererController> ();

				gestures.Add (currentGesture);
			} else {
				Vector3 screen = camera.WorldToScreenPoint (mouseTracker.transform.position);
				points.Add (new Point (screen.x, screen.y, strokeId));

				lineRendererVertexes.Add (screen);

				currentGesture.AddPoint (mouseTracker.transform.position);
			}
		}

		if (!isDrawing && points.Count > 20)
		{
			Gesture candidate = new Gesture (points.ToArray ());
			Result gestureResult = PointCloudRecognizer.Classify (candidate, trainingSet.ToArray ());
			message = gestureResult.GestureClass + " " + gestureResult.Score;
			Debug.Log (message);
			SpawnBullet (gestureResult.GestureClass);

			foreach (LineRendererController lineRenderer in gestures) {
				Destroy(lineRenderer.gameObject);
			}

			gestures.Clear ();
			strokeId = -1;
			points.Clear ();
			lineRendererVertexes.Clear ();
			currentGesture = null;
		}
	}

	public void startDrawing () {
		isDrawing = true;
	}

	public void finishDrawing () {
		isDrawing = false;
	}

	private void SpawnBullet(string Class) {
		Vector3 centroid = findCentroid ();
		// Vector3 spawnPosition = camera.ScreenToWorldPoint(centroid);new Vector3(centroid.x, centroid.y, centroid.z);
		// Quaternion spawnRotation = Quaternion.identity;
		Debug.Log (centroid);
		// Debug.Log (spawnPosition);
        Ray ray;
        Vector3 point;

        switch (Class)
		{
		    case "O":
                ray = Camera.main.ScreenPointToRay(centroid);
                // nextFire = Time.time + fireRate;
                point = ray.origin + ray.direction * 12;
                point.z -= 10;
                // Debug.Log("World point " + point);
                Instantiate(shot, point, shotSpawn.rotation);
                break;
            case "five point star":
                ray = Camera.main.ScreenPointToRay(centroid);
                // nextFire = Time.time + fireRate;
                point = ray.origin + ray.direction * 12;
                point.z -= 10;
                // Debug.Log("World point " + point);
                Instantiate(explosiveShot, point, shotSpawn.rotation);
                // audioSource.Play();
                break;
		    default: break;
		}
	}

	private Vector3 findCentroid() {
		Vector3 centroid = new Vector3 ();
		foreach (Vector3 point in lineRendererVertexes) {
			centroid += point;
		}

		centroid /= lineRendererVertexes.Count;

		return centroid;
	}
}
