using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoleculeBehavior : MonoBehaviour {

	public List<GameObject> atoms;
	public bool isComplete;
	public Rigidbody rb;

	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody> ();
		foreach (GameObject atom in atoms) {
			AtomBehavior ab = atom.GetComponent<AtomBehavior> ();
			if (ab.canBind) {
				isComplete = false;
				break;
			}
			isComplete = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
