
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

[RequireComponent(typeof(AudioSource))]
public class word_bat : MonoBehaviour
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
    void Start()
    {
		t=1;
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
		if (t == 1) {
			DateTime current_time = DateTime.Now;
			TimeSpan difference = current_time - last_played;
			//obj.SetActive(false);

			//             VARIABLES NEED TO BE CHANGED 
			float x_of_t = script_t.x;
			float x_of_a = script_a.x;
			float x_of_b = script_b.x;
			if (current_time - last_log_time >= LOG_DELAY) {
				last_log_time = current_time;

				Debug.Log ("I GET " + x_of_t + ", " + x_of_a + ", " + x_of_b + ", " + delay [0] + ", " + delay [1] + ", " + delay [2] + ", " + difference);	
			}

        
			bool flip_a = script_a.flip;
			bool flip_b = script_b.flip;
			bool flip_t = script_t.flip;

			if (x_of_a == 0 && x_of_b == 0 && x_of_t == 0)
				return;

			if (x_of_a == 0 || x_of_b == 0 || x_of_t == 0) {
				if (x_of_b == 0) {	
					Debug.Log ("This  sucks");
					if (delay [0] == true) {
						if (difference >= SAME) {
							Debug.Log ("same b");
							set (0);
							last_played = current_time;
							AudioSource.PlayClipAtPoint (clip2, transform.position, 1.0f);
						}
					} else {
						if (difference >= DIFFERENT) {
							Debug.Log ("different b");
							last_played = current_time;
							set (0);
							AudioSource.PlayClipAtPoint (clip2, transform.position, 1.0f);
						}
					}
					Debug.Log ("B is not there");
					c1fl = 1;

				} else if (x_of_a == 0) {	
					Debug.Log ("This laptop sucks");
					if (delay [1] == true) {
						if (difference >= SAME) {
							Debug.Log ("same a");
							last_played = current_time;
							set (1);
							c2fl = 1;
							AudioSource.PlayClipAtPoint (clip, transform.position, 1.0f);
						}
					} else {
						if (difference >= DIFFERENT) {
							Debug.Log ("different a");
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
							Debug.Log ("same t");
							last_played = current_time;
							set (2);
							AudioSource.PlayClipAtPoint (clip3, transform.position, 1.0f);
						}
					} else {
						if (difference >= DIFFERENT) {
							Debug.Log ("different t");
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
				if (flip_a || flip_b || flip_t) {
					if (flip_a == true) {
						if (delay [1] == true) {
							if (difference >= SAME) {
								Debug.Log ("Flip a same");
								last_played = current_time;
								set (1);
								AudioSource.PlayClipAtPoint (clip4, transform.position, 1.0f);
							}
						} else {
							if (difference >= DIFFERENT) {
								Debug.Log ("Flip a different");
								last_played = current_time;
								set (1);
								AudioSource.PlayClipAtPoint (clip4, transform.position, 1.0f);
							}
						}
						Debug.Log ("Try to flip A");
					} else if (flip_b == true) {
						if (delay [0] == true) {
							if (difference >= SAME) {
								Debug.Log ("Flip b same");
								last_played = current_time;
								set (0);
								AudioSource.PlayClipAtPoint (clip5, transform.position, 1.0f);
							}
						} else {
							if (difference >= DIFFERENT) {
								Debug.Log ("Flip b different");
								last_played = current_time;
								set (0);
								AudioSource.PlayClipAtPoint (clip5, transform.position, 1.0f);
							}
						}
						Debug.Log ("Try to flip B");
					} else if (flip_t == true) {
						if (delay [2] == true) {
							if (difference >= SAME) {
								Debug.Log ("Flip t same");
								last_played = current_time;
								set (2);
								AudioSource.PlayClipAtPoint (clip6, transform.position, 1.0f);
							}
						} else {
							if (difference >= DIFFERENT) {
								Debug.Log ("Flip b different");
								last_played = current_time;
								set (2);
								AudioSource.PlayClipAtPoint (clip6, transform.position, 1.0f);
							}
						}
						Debug.Log ("Try to flip T");
					}

					obj.SetActive (false);


				} else if (x_of_t > x_of_a && x_of_a > x_of_b) {
					
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