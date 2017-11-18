
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

[RequireComponent(typeof(AudioSource))]
public class word_fit : MonoBehaviour
{	
	public AudioClip clip;
	public AudioClip clip2;
	public AudioClip clip3;

	public AudioClip clip4;
	public AudioClip clip5;
	public AudioClip clip6;

	public AudioClip reorder;

	public AudioClip congo;

	public GameObject obj;
	private bool[] delay;
	private DateTime last_played, wrong_order, last_log_time;
	private TimeSpan SAME, DIFFERENT, LOG_DELAY;
	public static int c1fl, c2fl, c1fr;
	int x;
	int t;
	int wutct;
	void Start()
	{
		t = 0;
		wutct = 0;
		SAME = TimeSpan.FromSeconds(20);
		DIFFERENT = TimeSpan.FromSeconds(5);
		LOG_DELAY = TimeSpan.FromSeconds (2);
		last_played = new DateTime(1990, 12, 12);
		last_log_time = last_played;
		wrong_order = last_played;
		delay = new bool[3];
		for (int i = 0; i < delay.Length; i++)
			delay[i] = false;
		obj = GameObject.Find("bat");
		if (obj != null)
		{
			Debug.Log("Here");
			obj.SetActive(false);
		}
		c1fl = 0;
		c2fl = 0; 
		c1fr = 0;
		x = 0;

	}

	void set(int i)
	{
		for (int j = 0; j < 3; j++)
		{
			if (j != i)
				delay[j] = false;
			else
				delay[j] = true;
		}
	}

	void Update()
	{
		wutct++;
		if (wutct == 25)
			t = 1;
		if (t == 1) {
			DateTime current_time = DateTime.Now;
			TimeSpan difference = current_time - last_played;
			//obj.SetActive(false);

			//             VARIABLES NEED TO BE CHANGED 
			float x_of_f = script_f.x;
			float x_of_i = script_i.x;
			float x_of_t = script_t.x;
			if (current_time - last_log_time >= LOG_DELAY) {
				last_log_time = current_time;

				Debug.Log ("I GET " + x_of_t + ", " + x_of_i + ", " + x_of_f + ", " + delay [0] + ", " + delay [1] + ", " + delay [2] + ", " + difference);	
			}


			bool flip_f = script_f.flip;
			bool flip_i = script_i.flip;
			bool flip_t = script_t.flip;

			if (x_of_f == 0 && x_of_i == 0 && x_of_t == 0)
				return;

			if (x_of_f == 0 || x_of_i == 0 || x_of_t == 0) {
				if (x_of_f == 0) {	
					if (delay [0] == true) {
						if (difference >= SAME) {
							set (0);
							last_played = current_time;
							AudioSource.PlayClipAtPoint (clip2, transform.position, 1.0f);
						}
					} else {
						if (difference >= DIFFERENT) {
							last_played = current_time;
							set (0);
							AudioSource.PlayClipAtPoint (clip2, transform.position, 1.0f);
						}
					}
					Debug.Log ("F is not there");
					c1fl = 1;

				} else if (x_of_i == 0) {	
					if (delay [1] == true) {
						if (difference >= SAME) {
							last_played = current_time;
							set (1);
							c2fl = 1;
							AudioSource.PlayClipAtPoint (clip, transform.position, 1.0f);
						}
					} else {
						if (difference >= DIFFERENT) {
							last_played = current_time;
							set (1);
							c2fl = 1;
							AudioSource.PlayClipAtPoint (clip, transform.position, 1.0f);
						}
					}

					Debug.Log ("A is not there");

				} else if (x_of_t == 0) {
					if (delay [2] == true) {
						if (difference >= SAME) {
							last_played = current_time;
							set (2);
							AudioSource.PlayClipAtPoint (clip3, transform.position, 1.0f);
						}
					} else {
						if (difference >= DIFFERENT) {
							last_played = current_time;
							set (2);
							AudioSource.PlayClipAtPoint (clip3, transform.position, 1.0f);
						}
					}
					Debug.Log ("T is not there");
					c1fr = 1;
				}

				obj.SetActive (false);
			} else {
				if (flip_f || flip_i || flip_t) {
					if (flip_f == true) {
						if (delay [0] == true) {
							if (difference >= SAME) {
								last_played = current_time;
								set (0);
								AudioSource.PlayClipAtPoint (clip4, transform.position, 1.0f);
							}
						} else {
							if (difference >= DIFFERENT) {
								last_played = current_time;
								set (0);
								AudioSource.PlayClipAtPoint (clip4, transform.position, 1.0f);
							}
						}
						Debug.Log ("Try to flip A");
					} else if (flip_i == true) {
						if (delay [1] == true) {
							if (difference >= SAME) {
								last_played = current_time;
								set (1);
								AudioSource.PlayClipAtPoint (clip5, transform.position, 1.0f);
							}
						} else {
							if (difference >= DIFFERENT) {
								last_played = current_time;
								set (1);
								AudioSource.PlayClipAtPoint (clip5, transform.position, 1.0f);
							}
						}
						Debug.Log ("Try to flip B");
					} else if (flip_t == true) {
						if (delay [2] == true) {
							if (difference >= SAME) {
								last_played = current_time;
								set (2);
								AudioSource.PlayClipAtPoint (clip6, transform.position, 1.0f);
							}
						} else {
							if (difference >= DIFFERENT) {
								last_played = current_time;
								set (2);
								AudioSource.PlayClipAtPoint (clip6, transform.position, 1.0f);
							}
						}
						Debug.Log ("Try to flip T");
					}

					obj.SetActive (false);


				} else if (x_of_t > x_of_i && x_of_i > x_of_f) {

					Debug.Log ("Congratulations! It is the correct word!");
					x++;
					if (x == 100) {
						AudioSource.PlayClipAtPoint (congo, transform.position, 1.0f);
						t = 0;
					}

					obj.SetActive (true);
					//                script_a.B
				} else {
					obj.SetActive (false);
					if (current_time - wrong_order >= DIFFERENT) {
						Debug.Log ("Wrong order");
						AudioSource.PlayClipAtPoint (reorder, transform.position, 1.0f);
						wrong_order = current_time;
					}
					Debug.Log ("Maybe they are in the wrong order");
				}


			}
		}
	}
}