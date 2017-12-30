using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float health = 150f;
	public float laserSpeed;
	public GameObject laser;
	public float shotsPerSecond = 0.5f;
	public int scoreValue = 150;
	public AudioClip deathSound;
	
	private ScoreKeeper scoreKeeper;
	
	void Start(){
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}
	
	void Update(){
		float probability = Time.deltaTime * shotsPerSecond;
		if (Random.value < probability){
			Fire ();
		}
	}
	
	void Fire(){
		GameObject beam = Instantiate(laser, transform.position, laser.transform.rotation) as GameObject;
		beam.rigidbody2D.velocity = new Vector3(0, -laserSpeed, 0);
	}

	void OnTriggerEnter2D(Collider2D collider){
		Laser missile = collider.gameObject.GetComponent<Laser>();
		if (missile){
			health -= missile.GetDamage();
			missile.Hit();
			if (health <= 0){
				AudioSource.PlayClipAtPoint(deathSound, transform.position);
				Destroy(gameObject);
				scoreKeeper.Score(scoreValue);
			}
		}
	}
}
