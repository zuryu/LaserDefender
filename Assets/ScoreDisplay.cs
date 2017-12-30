using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text text = GetComponent<Text>();
		text.text = ScoreKeeper.score.ToString();
		ScoreKeeper.reset();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
