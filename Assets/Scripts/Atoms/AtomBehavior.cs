using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

public class AtomBehavior : MonoBehaviour {

	bool outerShellFull;
	public int valenceElectrons;
	public bool canBind;
	public int AtomicNumber;
	public int ElectronNumber = -1;
	bool isIon;
	public GameObject proton;
	public GameObject neutron;
	public GameObject electron;
	public GameObject nucleus;
	public GameObject molecule;
	public List<GameObject> protons;
	public List<GameObject> neutrons;
	public List<GameObject> electrons;
//	public bool excited;
//	public double temperature;
	SphereCollider coll;
	int sharedElectrons;
	public Rigidbody rb;
	bool isBound;
	bool oneCollision;
//	public ParticleSystem cloud;

	// Use this for initialization
	void Start () {
		isBound = false;
		oneCollision = false;
		coll = GetComponent<SphereCollider> ();
		rb = GetComponent<Rigidbody> ();
//		GameObject cl = (GameObject) Instantiate (cloud, transform.position, Quaternion.identity);
//		cl.transform.parent = transform;
		if (AtomicNumber == ElectronNumber)
			isIon = false;
		else if (ElectronNumber == -1) {
			ElectronNumber = AtomicNumber;
		} else {
			isIon = true;
		}

		GameObject nuc = (GameObject) Instantiate (nucleus, transform.position, Quaternion.identity);
		nuc.transform.parent = transform;

		GravityBehavior pgb;
		GravityBehavior ngb;
		Vector3 pspawnpoint = transform.position;
		Vector3 nspawnpoint = transform.position;
		Vector3 poffset = new Vector3 (.1f, 0f, .1f);
		Vector3 noffset = new Vector3 (0f, .1f, 0f);
		for (int i = 0; i < AtomicNumber; i++) {
			poffset = poffset * ((i+1)/1.5f);
			noffset = noffset * ((i+1)/1.5f);
			if (i % 2 == 0) {
				pspawnpoint = transform.position + poffset;
				nspawnpoint = transform.position - noffset;
			} else {
				pspawnpoint = transform.position - poffset;
				nspawnpoint = transform.position + noffset;
			}
			GameObject p = (GameObject)Instantiate (proton, pspawnpoint, Quaternion.identity);
			GameObject n = (GameObject)Instantiate (neutron, nspawnpoint, Quaternion.identity);
			pgb = p.GetComponent<GravityBehavior> ();
			ngb = n.GetComponent<GravityBehavior> ();
			pgb.AttractionPoint = nuc;
			ngb.AttractionPoint = nuc;
			pgb.strength = .1f;
			ngb.strength = .1f;
			p.transform.parent = transform;
			n.transform.parent = transform;
			protons.Add (p);
			neutrons.Add (n);
		}

//		transform.localScale = transform.localScale * (AtomicNumber * .2f);

//		coll.radius = .5f * (ElectronNumber * 2.0f);
		rb.mass = protons.Count + neutrons.Count;

		if (!isIon) {
			if (ElectronNumber == 2 ||
			    ElectronNumber == 10 ||
			    ElectronNumber == 18 ||
			    ElectronNumber == 36 ||
			    ElectronNumber == 54 ||
			    ElectronNumber == 86) {
				outerShellFull = true;
				canBind = false;
			} else {
				canBind = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void OnCollisionEnter(Collision other)
	{
		Debug.Log (other.gameObject.tag);

		Rigidbody orb;
		if (other.gameObject.tag == "Atom" && gameObject.tag == "Atom") {
			orb = other.gameObject.GetComponent<Rigidbody> ();
			AtomBehavior oAtom = other.gameObject.GetComponent<AtomBehavior> ();
			if (canBind && oAtom.canBind && !oAtom.isBound && !isBound) {
				GameObject mol = Instantiate (molecule); 
				if (molecule == null) {
					Rigidbody mrb = mol.GetComponent<Rigidbody> ();
					mrb.useGravity = false;
					mrb.mass = orb.mass + rb.mass;
					mol.transform.position = Vector3.Lerp (transform.position, other.transform.position, .5f);
					mrb.isKinematic = false;
				}
				ElectronNumber += oAtom.valenceElectrons;
				oAtom.ElectronNumber += valenceElectrons;
				rb.isKinematic = true;
				oAtom.rb.isKinematic = true;
				transform.parent = mol.transform;
				oAtom.transform.parent = mol.transform;
				valenceElectrons -= oAtom.valenceElectrons;
				oAtom.valenceElectrons -= valenceElectrons;
				gameObject.tag = "Molecule";
				other.gameObject.tag = "Molecule";
				oneCollision = true;
			}
		} else if (other.gameObject.tag == "Molecule" && gameObject.tag == "Atom") {
			oneCollision = true;
			orb = other.gameObject.GetComponent<Rigidbody> ();
			GameObject moleculeObject = other.transform.parent.gameObject;
			MoleculeBehavior mb = moleculeObject.GetComponent<MoleculeBehavior> ();
			if (mb != null && !mb.isComplete) {
				mb.rb.mass += rb.mass;
				rb.isKinematic = true;
				mb.rb.isKinematic = false;
				mb.rb.useGravity = false;
				transform.parent = moleculeObject.transform;
			}
		} else if (other.gameObject.tag == "Molecule" && gameObject.tag == "Molecule" && !oneCollision) {
			oneCollision = true;
			other.transform.parent.parent = transform.parent;
		}

		if (valenceElectrons == 0)
			canBind = false;
	}
}
