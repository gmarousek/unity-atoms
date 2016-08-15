using UnityEngine;
using System.Collections;

public class Creation : MonoBehaviour {

	public GameObject atom;
	private AtomBehavior ab;
	public int AtomicNumber;
	private Camera camera;

	// Use this for initialization
	void Start () {
		ab = atom.GetComponent<AtomBehavior> ();
		ab.AtomicNumber = AtomicNumber;
		camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = 5;
			Instantiate (atom, camera.ScreenToWorldPoint (mousePos), Quaternion.identity);
		}
	}
}
