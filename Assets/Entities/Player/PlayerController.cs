using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 15.0f;
	public float padding = 1.0f;
	public GameObject laser;
	public float laserSpeed;
	public float fireRate = 0.2f;
	public float health = 250f;
	
	private float maxX;
	private float minX;

	// Use this for initialization
	void Start () {
		float Distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Distance));
		minX = leftMost.x + padding;
		maxX = rightMost.x - padding;
	}
	
	// Update is called once per frame
	void Update () {
		MovePlayer();
		if (Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("Shoot", 0.000001f, fireRate);
		}
		if (Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("Shoot");
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		EnemyLaser missile = collider.gameObject.GetComponent<EnemyLaser>();
		if (missile){
			health -= missile.GetDamage();
			missile.Hit();
			if (health <= 0){
				LevelManager levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager>();
				levelManager.LoadLevel("Win Screen");
				Destroy(gameObject);
			}
		}
	}
	
	void MovePlayer(){
		if (Input.GetKey(KeyCode.LeftArrow)){
			//this.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.left * speed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.RightArrow)){
			//this.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		
		float newX = Mathf.Clamp(transform.position.x, minX, maxX);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
	
	void Shoot(){
			GameObject beam = Instantiate(laser, this.transform.position, Quaternion.identity) as GameObject;
			beam.rigidbody2D.velocity = new Vector3(0, laserSpeed, 0);
	}	
}
