using UnityEngine;
using System.Collections;

public class Launcher : MonoBehaviour {

	public GameObject blast;
	GameObject currentBlast;
	Rigidbody rb;
	Camera cam;
	public float shotForce;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = 5;
			currentBlast = (GameObject) Instantiate (blast, cam.ScreenToWorldPoint (mousePos), Quaternion.identity);
			rb = currentBlast.GetComponent<Rigidbody> ();
			rb.AddForce (Vector3.forward * shotForce);
		}
	}
}
