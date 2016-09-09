using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Vector3 mousePos;

	private static GameManager instance = null;  //made the assumption that we want a persistent GameManager for later

	void Awake() {
		// Persistent singleton
		if (instance != null) {	Destroy (gameObject);
			Debug.Log("Destroying: " + GetInstanceID());}
		else { 
			instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//Get mouse's coordinates and save them in game units.
		//Updated here so that the conversion only has to happen once.
		mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
	}
}
