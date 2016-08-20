using UnityEngine;
using System.Collections;

public class AnimationPlay : MonoBehaviour {

	void Awake()
	{
		gameObject.GetComponent<Animator>().Play(gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).ToString());
	}

	void OnLevelWasLoaded()
	{
		gameObject.GetComponent<Animator>().Play(gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).ToString());

	}
}
