using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {
		public GameObject learth;
		public GameObject test_star;
		GameObject l, s;

	// Use this for initialization
	void Start ()
	{
		l = Instantiate (learth, new Vector3 (20, -20, 0), new Quaternion (0, 0, 0, 0)) as GameObject;
		s = Instantiate (test_star, new Vector3 (20, 20, 0), new Quaternion (0, 0, 0, 0)) as GameObject;
		
		Vector3 l_movement = l.transform.up;
		Vector3 s_vec = s.transform.position;
		//	Vector3 projection = Vector3.Project (s_vec, l_movement);
		Vector3 star_from_learth = s.transform.position - l.transform.position;
		
		//star_from_learth = l.transform.position + star_from_learth;
		
		float distance = Vector3.Distance (l.transform.position, s.transform.position);
		Vector3 useful_movement_vec = Vector3.Scale (new Vector3 (distance, distance, distance), l_movement);
		
		//slightly past tangent (hyp len)
		useful_movement_vec = useful_movement_vec + l.transform.position;
		
		Vector3 projection = Vector3.Project (star_from_learth, l_movement);
		
		
		Debug.Log ("l_movement: " + l_movement.ToString ());
		Debug.Log ("Star position: " + s_vec.ToString ());
		Debug.Log ("projection: " + projection.ToString ());
		Debug.Log ("Learth position: " + l.transform.position.ToString ());
		Debug.Log ("star_from_learth: " + star_from_learth.ToString ());
		Debug.Log ("useful movement: " + useful_movement_vec.ToString ());
		Debug.Log("distance b/t star and learth: "+distance);
		
		
		GameObject n_s = Instantiate (learth, projection, new Quaternion (0, 0, 0, 0)) as GameObject;
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	
	}
}
