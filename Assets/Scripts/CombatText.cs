using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CombatText : MonoBehaviour {

	private float speed; //szybkosc z jaka tekst sie bedzie poruszal
	private float fadeTime; //po jakim czasie zniknie
	private Vector3 direction; //kierunek w ktorym pojdzie tekst;
	public AnimationClip critAnim;
	private bool crit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

			float translation = speed * Time.deltaTime; // potrzebne do ponizszej funkcji Translate()
			transform.Translate (direction * translation); //wskazanie kierunki w ktorym poruszac ma sie tekst
	}

	//function to get access to private variables
	public void Initialize(float _speed,Vector3 _direction, float _fadeTime, bool _crit) //funkcj stworzona po to by miec dostep do parametrow private
	{
		this.speed = _speed;
		this.fadeTime = _fadeTime;
		this.direction = _direction; //this.direction aby zdobyc dostep do private direction
		this.crit = _crit;

		if (crit) 
		{
			GetComponent<Animator>().SetTrigger("Critical");
			crit = false;
			StartCoroutine(Critical());
		} 
		else 
		{
			StartCoroutine (FadeOut ());
		}

	}

	private IEnumerator Critical()
	{
		yield return new WaitForSeconds(critAnim.length);
		StartCoroutine(FadeOut());
	}

	private IEnumerator FadeOut()
	{
		float startAlpha = GetComponent<Text> ().color.a; // pobranie wartosci alpha tekstu

		float rate = 1.0f / fadeTime;
		float progress = 0.0f; //od 0 do 1 bedzie sie poruszal

		while(progress < 1.0f)
		{
			Color tmpColor = GetComponent<Text>().color;
			GetComponent<Text>().color = new Color(tmpColor.a,tmpColor.g,tmpColor.b, Mathf.Lerp(startAlpha,0,progress));

			progress += rate * Time.deltaTime;

			yield return null;
		}
		Destroy (gameObject);
	}
}
