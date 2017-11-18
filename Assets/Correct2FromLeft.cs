using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;


[RequireComponent(typeof(AudioSource))]
public class Correct2FromLeft : MonoBehaviour {
	public AudioClip clip;
	int x = 0;
	void Start () {
		
	}
	void Update () {
		Debug.Log ("wHAT IS YOUR PROBLEM?");
		if (word_bat.c2fl == 1) {
			if (x == 100) {
				AudioSource.PlayClipAtPoint (clip, transform.position, 1.0f);
				x = 0;
			}
			x++;
			Debug.Log ("why?");
		}
	}
}




