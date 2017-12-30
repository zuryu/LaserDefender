using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float speed = 5f;
	public float width = 10f;
	public float height = 5f;
	public float spawnDelay = 0.5f;
	
	private bool movingRight = true;
	private float minX;
	private float maxX;

	// Use this for initialization
	void Start () {
		SpawnUntilFull();
		float Distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Distance));
		minX = leftMost.x;
		maxX = rightMost.x;
	}
	
	void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}
	
	// Update is called once per frame
	void Update () {
		if (movingRight){
			//this.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			//this.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		float rightEdge = transform.position.x + (0.5f * width);
		float leftEdge = transform.position.x - (0.5f * width);
		if (leftEdge < minX){
			movingRight = true;
		} else if (rightEdge > maxX){
			movingRight = false;
		}
		
		if (AllMembersDead()){
			Debug.Log ("dead");
			SpawnUntilFull();
		}
	}
	
	bool AllMembersDead(){
		foreach (Transform child in transform){
			if(child.childCount > 0){
				return false;
			}
		}
		return true;
	}
	
	void CreateEnemies(){
		foreach (Transform child in transform){			
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}
	
	void SpawnUntilFull(){
		Transform freePosition = NextFreePosition();
		if (freePosition){
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if (NextFreePosition()){
			Invoke("SpawnUntilFull", spawnDelay);
		}
	}
	
	Transform NextFreePosition(){
		foreach (Transform child in transform){
			if(child.childCount == 0){
				return child;
			}
		}
		return null;
	}
}
