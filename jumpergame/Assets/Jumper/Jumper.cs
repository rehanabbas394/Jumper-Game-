using UnityEngine;
using System.Collections;

public class Jumper : MonoBehaviour {
	public Transform Detonator;
	public static Vector3 Position;
	public static float distance;
	public float startX;
	public static int numberPowerup;
	
	
	
	// Use this for initialization
	void Start () {
		GameManager.Instance.GameStart += GameStart;
		GameManager.Instance.GameOver += GameOver;
		this.enabled = false;
		this.GetComponent<Renderer>().enabled = false;
		this.GetComponent<Rigidbody>().isKinematic = true;
	}
	
	
	void GameStart()
	{
		
		this.enabled = true;
		this.GetComponent<Renderer>().enabled = true;
		this.GetComponent<Rigidbody>().isKinematic = false;
		this.transform.localPosition = new Vector3 (
			this.transform.localPosition.x,
			0,
			0);
		startX = this.transform.localPosition.x;
		GetComponent<Rigidbody>().AddForce(10,0,0,ForceMode.VelocityChange);
		numberPowerup = 3;
		
	}
	
	void GameOver()
	{
		var det = (Transform)Instantiate(Detonator);
		det.localPosition = this.transform.localPosition;
		this.GetComponent<Rigidbody>().Sleep();
		GetComponent<Renderer>().enabled = false;
		this.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(GetComponent<Rigidbody>().velocity.y > 10)
			GetComponent<Rigidbody>().AddForce(0,-1,0,ForceMode.VelocityChange);
		if(Input.GetButtonDown("Jump"))
		{
			GetComponent<Rigidbody>().AddForce(0,15,0,ForceMode.VelocityChange);			
			
		}
		Position = transform.localPosition;
		distance = transform.localPosition.x - startX;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag=="Border"||other.tag=="Barrier")
		{
			GameManager.Instance.OnGameOver();
		}

		
	}
}
