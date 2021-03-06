using UnityEngine;
using System.Collections;

public class Space_Bomb_Actual : MonoBehaviour {
	
	public Vector3 velocity;
	public Transform lastPos;
	
	
	//hook for range
	public GameObject range;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//calculate velocity every frame
		velocity = this.transform.position - lastPos.position;
		lastPos.position = this.transform.position;
		//regular movement
		this.transform.position += velocity.normalized*Manager.speed*0.75f;	
	}
	
	//called by Space_Bomb power up, this pushes everything out of the way (or blows it up)
	public void Detonate() {
		GameObject range_actual = Instantiate(range,transform.position,new Quaternion(0,0,0,0)) as GameObject;
		//set start size
		range_actual.transform.localScale += new Vector3(25,25,3);
		//destroy the bullet
		Destroy (gameObject);
	}
}
