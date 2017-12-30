using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	private Text text;	
	public static int score;
	
	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		reset();
	}
	
	public void Score(int points){
		score += points;
		text.text = score.ToString();
	}
	
	public static void reset(){
		score = 0;
	}
}
