using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Skill : MonoBehaviour {

	//referance to skill's image
	private Image sprite;

	//referance to skill's neutral image
	public Sprite spriteNormal;

	//reference to spell's ray
	private Ray shootRay;

	//referance to SkillManager class
	private SkillManager skillManager;

	//reference to a skill prefab
	private GameObject skillPrefab;

	//reference to a hover object in the scene
	private GameObject hoverObject;

	//difference between camera's z axis and spell's z axis
	float depth = 10.0f;

	//reference to a layer mask which you can hit by a spell
	public LayerMask levelMask;

	//reference to wall mask
	public LayerMask wallMask;

	//reference to a lottery manager
	private Lottery lottery;

	//reference to spell's name
	private string spellName;

	//reference to bonus script
	private Bonuses bonus;

	[SerializeField]
	private GameObject lightningPrefab;

	public Image Sprite
	{
		get{return sprite;}
		set{sprite = value;}
	}

	void Awake()
	{
		sprite = GetComponent<Image>();
		skillManager = FindObjectOfType<SkillManager>();
		lottery = GameObject.Find("LotteryPanel").GetComponent<Lottery>();
		if(GameObject.Find("BonusSystem") != null)
		{
			bonus = GameObject.Find("BonusSystem").GetComponent<Bonuses>();
		}
		else
			return;
	}

	// Use this for initialization
	void Start () 
	{
		
	}
		
	void OnLevelWasLoaded()
	{
		sprite = GetComponent<Image>();
		skillManager = FindObjectOfType<SkillManager>();
		lottery = GameObject.Find("LotteryPanel").GetComponent<Lottery>();
		if(GameObject.Find("BonusSystem") != null)
		{
			bonus = GameObject.Find("BonusSystem").GetComponent<Bonuses>();
		}
		else
			return;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//if there is a hover object in the scene
		if(GameObject.Find("Hover-" + spellName))
		{
			//array used for multi-touch
			Touch[] spellTouch = Input.touches;
			for(int i = 0; i < Input.touchCount; i++)
			{
				spellTouch[i] = Input.GetTouch(i);

				if(Input.touchCount > 0 && spellTouch[i].phase == TouchPhase.Moved)
				{
					shootRay = Camera.main.ScreenPointToRay(spellTouch[i].position);
			
					//fingerPos.z = 10;
					if(hoverObject != null)
					{
						RaycastHit2D _shootHit = Physics2D.Raycast(shootRay.origin,shootRay.direction,Mathf.Infinity,levelMask.value);

						//make spell follow the finger
						if(_shootHit.collider.CompareTag("Wall") == false)
						{
							hoverObject.transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (spellTouch[i].position.x,spellTouch[i].position.y, depth));
						}

						if(hoverObject.name == "Hover-Recovery")
						{
							hoverObject.transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (spellTouch[i].position.x,spellTouch[i].position.y, depth));
						}
						//return a place where spell hit
				

						//check for a left-mouse click
						if(spellTouch[i].tapCount > 1)
						{
							if(_shootHit.collider.CompareTag("Wall") == true && hoverObject.name == "Hover-Recovery")
							{
								Spell _spell = hoverObject.GetComponent<Spell>();
								_spell.Heal();
								Time.timeScale = 1;
							}
							//check if player clicked an enemy
							else if(_shootHit.collider.CompareTag("Enemy") == true || _shootHit.collider.CompareTag("Level") == true)	
							{
								//use a spell
								Use(hoverObject,_shootHit);
								Time.timeScale = 1;
							}
							else
								return;
						}
					}
				}
			}
		}
	}

	//
	public virtual void Use(GameObject _hover, RaycastHit2D _hit)
	{		
		Spell _spell = _hover.GetComponent<Spell>();

		//position where the spell hit in 2D coordinates
		Vector2 _spellHit = new Vector2(_hit.transform.position.x,_hit.transform.position.y);

		//variable that store AoE Holder's BoxCollider2D
		Collider2D _holderBC = _hover.GetComponentInChildren<Collider2D>();

		//get an array of colliders which are within spell's radius range
		Collider2D[] _colliders = Physics2D.OverlapAreaAll(_holderBC.bounds.min,_holderBC.bounds.max,levelMask.value);

		for (int i = 0; i < _colliders.Length; i++)
		{
			//checks whther we hit an enemy
			if(_colliders[i].gameObject.CompareTag("Enemy") == true)
			{
				EnemyController _enemy = _colliders[i].gameObject.GetComponent<EnemyController>();

				if(_hover.name == "Hover-Dizzy")
				{
					//stop enemy - Dizzy effect spell
					GameObject particleDizzy = (GameObject)Instantiate(_hover.GetComponent<ParticleSystem>().gameObject, new Vector2(_enemy.transform.position.x,_enemy.transform.position.y + 0.5f),Quaternion.identity) as GameObject;
					particleDizzy.transform.FindChild("Holder").gameObject.SetActive(false);
					particleDizzy.name = "Dizzy";
					particleDizzy.transform.SetParent(_enemy.transform);
					particleDizzy.tag = "Dizzy";
					StartCoroutine(_enemy.StopEnemy());
					continue;
				}
					

				//vatiable that store enemy's position
				Vector2 _enemyPosition = _enemy.transform.position;

				//calculate a damage taken - depends on enemy's position
				float _damage = _spell.CalculateDamage(_enemyPosition,_spellHit,_holderBC.bounds.extents);

				//deal damage to an enemy
				_enemy.ReceiveDamage(_damage * _enemy.magicalDefence);

				//coroutine that change enemy's sprite color depending on how much damage was taken by it
				StartCoroutine(_enemy.SetEnemySpriteColor(_damage, _spell.spellTime));

				if(_hover.name == "Hover-WaterBall")
				{
					//slow enemy's movement when Water Ball was used
					_enemy.SlowMovement();
					continue;
				}

				if(_hover.name == "Hover-ElectricBall")
				{
					for(int y = 0; y < bonus.enemiesInGame.Count; y++)
					{
						//checks whether enemy boxcollider2d is touching that certain enemy's box collider 2d
						bool _isTouching = _enemy.GetComponent<BoxCollider2D>().IsTouching(bonus.enemiesInGame[y].GetComponent<BoxCollider2D>());
						if(_isTouching == true && _enemy != bonus.enemiesInGame[y])
						{
							EnemyController _nearbyEnemy = bonus.enemiesInGame[y].GetComponent<EnemyController>();

							//damage enemies in a close proximity
							_nearbyEnemy.ReceiveDamage(_damage / 4f);
							GameObject lightning = (GameObject)Instantiate(lightningPrefab,bonus.enemiesInGame[y].transform.position,bonus.enemiesInGame[y].transform.rotation) as GameObject;
							if(GameObject.FindGameObjectsWithTag("Lightning") != null)
							{
								GameObject[] particles = GameObject.FindGameObjectsWithTag("Lightning");
								if(particles.Length >= 8)
								{
									AchievementManager.Instance.EarnAchievement("I am Thor!");
									GooglePlayManager.Instance.UnlockAchievementById("CgkIkti4j78fEAIQDw");
									AchievementManager.Instance.EarnAchievement("Moon Shield");
								}

							}
							Destroy(lightning, lightning.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
						}
					}
				}

				//swaps all spells in hand when DevilAwait spell was used
				if(_hover.name == "Hover-DevilAwait")
				{
					lottery.IsSpinning = false;
					for(int z = 0; z < lottery.skills.Length; z++)
					{
						lottery.skills[0].Sprite.sprite = lottery.skillsImage[UnityEngine.Random.Range(0,lottery.skillsImage.Length)];
						lottery.skills[1].Sprite.sprite = lottery.skillsImage[UnityEngine.Random.Range(0,lottery.skillsImage.Length)];
						lottery.skills[2].Sprite.sprite = lottery.skillsImage[UnityEngine.Random.Range(0,lottery.skillsImage.Length)];
					}

				}
			}
			

			if(_colliders[i].gameObject.tag == "Level")
			{
				//creates fire on the ground when BlueFire spell was used
				if(_hover.name == "Hover-BlueFire")
				{
					GameObject particleFire = (GameObject)Instantiate(_hover.GetComponent<ParticleSystem>().gameObject, _colliders[i].transform.position,Quaternion.identity) as GameObject;
					particleFire.transform.FindChild("Holder").gameObject.SetActive(false);
					particleFire.AddComponent<BoxCollider2D>().isTrigger = true;
					particleFire.name = "FireAoE";

					//destroy it after 4 seconds
					Destroy(particleFire,4f);
					continue;
				}
			}
		}

		//destroy the hover object
		Destroy(GameObject.Find("Hover-" + spellName));
	}


	//function that creates a hover object
	public void CreateHoverIcon(GameObject _clicked)
	{
		//do it only if lottery is not spinning
		if(lottery.IsSpinning == false/* && !GameObject.FindObjectOfType<ParticleSystem>()*/)
		{

			//try to find a skill prefab base on its name in a dictionary
			if(skillManager.Database.TryGetValue(_clicked.GetComponent<Image>().sprite.name, out skillPrefab))
			{
				spellName = skillPrefab.name;

				if(SceneManager.GetActiveScene().name == "Arena")
				{
					if(GameObject.Find("EnergyImage").GetComponent<EnergyManager>() != null)
					{
						EnergyManager energyManager = GameObject.Find("EnergyImage").GetComponent<EnergyManager>();

						//decrease mana f
						if(energyManager.CheckIfNoMana(skillPrefab.GetComponent<Spell>().energyCost) == true)
						{
							return;
						}
						else
						{
							EnergyManager.Instance.currentMana -= skillPrefab.GetComponent<Spell>().energyCost;
							energyManager.SetManaUI();
						}
					}
				}

				if(skillPrefab.GetComponent<Spell>().wasUsed == false)
				{
					skillPrefab.GetComponent<Spell>().wasUsed = true;
				}

				//instantiates a hover object
				hoverObject = (GameObject)Instantiate(skillPrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject; //Instantiates the hover object 

				//names the object
				hoverObject.name = "Hover-" + spellName; //Sets the name of the hover object

				//enters slow-mo
				Time.timeScale = 0.5f;
			}

			//change the skill slot image to normal
			gameObject.GetComponent<Image>().sprite = spriteNormal;
		}
	}
		
}
