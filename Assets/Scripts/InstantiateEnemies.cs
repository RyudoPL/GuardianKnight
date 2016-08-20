using UnityEngine;
using System.Collections;

public class InstantiateEnemies : MonoBehaviour {

	//array of instantiators
	public GameObject[] instantiators;

	//array of enemies' prefabs
	public GameObject[] enemies;

	//check if enemies are being generated in that moment
	private bool isGenerating = false;

	//reference to Bonuses script
	private Bonuses bonus;


	void Awake()
	{
		bonus = GameObject.Find("BonusSystem").GetComponent<Bonuses>();
	}

	// Update is called once per frame
	void Update () 
	{
		if(!isGenerating)
		{
			StartCoroutine("InstantiateEnemy");
		}
	}

	//coroutine that starts instantiating enemies
	private IEnumerator InstantiateEnemy()
	{
		//starts generating enemies
		isGenerating = true;

		//yield return new WaitForSeconds(3);
		for(int i = 0; i < instantiators.Length; i++)
		{
			GameObject _temp = (GameObject)Instantiate(enemies[Random.Range(0,enemies.Length)],instantiators[i].transform.position,Quaternion.identity) as GameObject;
			bonus.enemiesInGame.Add(_temp);
		}
		//wait 3 seconds till next round of enemies
		yield return new WaitForSeconds(5);

		//stops genereting enemies
		isGenerating = false;
	}
}
