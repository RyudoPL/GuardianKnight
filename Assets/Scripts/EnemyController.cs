using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour {

	//reference to enemy's animator
	private Animator animator;

	//reference to enemy attack sound
	public AudioClip enemyAttack;

	//stores enemy's move 2d vector
	private Vector2 moveAmount;

	//resistance to physical attacks
	public float physicalDefence;

	//resistance to magical attacks
	public float magicalDefence;

	//in order for enemy to move to the left
	private float moveDirection = -1.0f;

	//reference to CastleHealth object
	private CastleHealth castle;

	//enemy's speed
	public float speed;

	//enemy's health
	[SerializeField]
	private float startHealth = 100f;

	//reference to current health
	private float currentHealth;

	//enemy's attack power
	public float playerDamage;

	//reference to sprite renderer
	private SpriteRenderer enemySR;

	//reference to enemy's rigidbody
	private Rigidbody2D enemyRB;

	//enemy's sprite color when close to the place where spell hit
	private Color spriteCloseColor = Color.red;

	//enemy's sprite color when far away from the place where spell hit
	private Color spriteFarColor = Color.yellow;

	//reference to Bonuses class
	private Bonuses bonus;

	//reference to AchievementManager script
	public AchievementManager achievement;

	//how many points will Player get after defeating an enemy
	public int enemyScore;

	//reference to enemy's BoxCollider2D
	public BoxCollider2D enemyBC;

	[SerializeField]
	private RectTransform healthBar;

	//color of enemy's UI when full
	private Color fullHealthColor = Color.green;

	//color of enemy's UI when zero
	private Color zeroHealthColor = Color.red;

	private bool enemyCanMove = true;

	[SerializeField]
	private AudioSource[] audioClip;

	void Awake()
	{
		animator = GetComponent<Animator>();
		castle = GameObject.Find("CastleHealth").GetComponent<CastleHealth>();
		enemySR = GetComponent<SpriteRenderer>();
		bonus = GameObject.Find("BonusSystem").GetComponent<Bonuses>();
		enemyBC = GetComponent<BoxCollider2D>();
		enemyRB = GetComponent<Rigidbody2D>();
		achievement = GameObject.Find("AchievementManager").GetComponent<AchievementManager>();
	}

	// Use this for initialization
	void Start () 
	{
		currentHealth = startHealth;	
	}
	
	// Update is called once per frame
	void Update () 
	{
		TouchingSameEnemy();
	}

	void FixedUpdate()
	{
		//if current enemy animator state is equal to walk
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
		{
			//then move an enemy
			if(enemyCanMove == true)
			{
				MoveEnemy();
			}
		}
			
	}

	//function that moves an enemy
	public void MoveEnemy()
	{	
			moveAmount.x = moveDirection * speed * Time.deltaTime;
			transform.Translate(moveAmount); //Move the enemy	
	}

	//start attacking after reaching the wall
	public void OnCollisionEnter2D(Collision2D obj)
	{
		if(obj.collider.tag == "Wall")
		{
			animator.SetBool("isAttacking",true);
			StartCoroutine("Attack");
		}
	}

	public void OnTriggerEnter2D(Collider2D obj)
	{
		//receive damage when touching fire on the ground
		if(obj.transform.name == "FireAoE")
		{
			GameObject particleFire = obj.gameObject;
			Spell spell = particleFire.GetComponent<Spell>();
			StartCoroutine(FireAoE(particleFire,spell));
		}
	}

	//function that decreases enemy's health
	public void ReceiveDamage(float damage)
	{
		int dmg = (int)damage;
		bool critical = Random.Range(1,10) == 1 ? true : false;
		CombatTextManager.Instance.CreateText(transform.position,dmg.ToString(),Color.red,critical,GetComponentInChildren<RectTransform>());

		//decrease health 
		currentHealth -= damage;

		//if an enemy is dead
		if(CheckIfDead() == true)
		{
			SetHealthUI(0,startHealth);
			Dead();

		}
		else
		{
			SetHealthUI(currentHealth,startHealth);
			return;
		}
	}

	//checks if enemy has any hp left
	public bool CheckIfDead()
	{
		if(currentHealth <= 0)
		{
			StopCoroutine("SlowEnemy");
			return true;
		}
		else
			return false;
	}

	//function that covers everything connected to the dead state of an enemy
	private void Dead()
	{
		int _score = enemyScore * CheckIfTouching();
		if(CheckIfTouching() == 2)
		{
			bonus.StartCoroutine("BonusDamage");
		}
		enemyBC.enabled = false;

		//set death animation on
		animator.SetBool("isDead",true);
		animator.Play("Death");
		audioClip[1].Play();

		bonus.UpdateScore(_score);

		if(SceneManager.GetActiveScene().name == "Arena")
		{
			EnergyManager.Instance.currentMana += Random.Range(0,10);
			EnergyManager.Instance.SetManaUI();
		}
		//destroy enemy object
		bonus.enemiesInGame.Remove(gameObject);

		//destroy the object after playing death animation
		Destroy(gameObject,animator.GetCurrentAnimatorStateInfo(0).length);

	}

	//funtion that controls enemy's attack state
	public IEnumerator Attack()
	{
		while(currentHealth > 0)
		{
			//attack multiplied by number of the same enemies being in a row
			audioClip[0].Play();
			castle.ReceiveDamage(playerDamage * CheckIfTouching());
			yield return new WaitForSeconds(1);
		}
	}

	//function that makes enemy's sprite to blink (when taking damage)
	public IEnumerator SetEnemySpriteColor(float _damage, float _time)
	{
		if(CheckIfDead() == false)
		{
			float _waitTime = 0.5f;
			float _timer = 0.0f;

			if(enemySR != null)
			{
				while(_timer <= _time)
				{
					if(enemySR != null)
					{
						enemySR.color = Color.Lerp(spriteCloseColor, spriteFarColor, _time / _damage);
					}
					yield return new WaitForSeconds(_waitTime);
					if(enemySR != null)
					{
						enemySR.color = new Color(255,255,255,255);
					}
					yield return new WaitForSeconds(_waitTime);
					_timer += _waitTime + _waitTime;
				}	
			}
		}
	}

	//function that sets enemy's health UI
	private void SetHealthUI(float _current,float _max)
	{
		float _value = _current / _max;
		
		healthBar.localScale = new Vector3(healthBar.localScale.x,_value,healthBar.localScale.z);

		healthBar.GetComponent<Image>().color = Color.Lerp(zeroHealthColor,fullHealthColor,_value);
	}

	//function that checks whether an enemy is touching other enemy from the same kind with boxcollider2d
	public int CheckIfTouching()
	{
		if(bonus.enemiesInGame != null)
		{
			for(int i = 0; i < bonus.enemiesInGame.Count; i++)
			{
				//if gameobject is different that certain enemy
				if(gameObject != bonus.enemiesInGame[i])
				{
					//checks whether enemy boxcollider2d is touching that certain enemy's box collider 2d
					bool _isTouching = enemyBC.IsTouching(bonus.enemiesInGame[i].GetComponent<BoxCollider2D>());

					//is it is touching other boxcollider2D + the name of those two enemies is the same
					if(gameObject.name == bonus.enemiesInGame[i].name && _isTouching == true)
					{
						for(int y = 0; y < bonus.enemiesInGame.Count; y++)
						{
							//checks whether that certain enemy is different from gameobject + one more enemy
							if(bonus.enemiesInGame[i] != bonus.enemiesInGame[y] && bonus.enemiesInGame[i] != gameObject)
							{
								//checks whether that enemy's box collider 2d is touching another enemy's box collider 2d
								bool _isTouching2 = bonus.enemiesInGame[i].GetComponent<BoxCollider2D>().IsTouching(bonus.enemiesInGame[y].
									GetComponent<BoxCollider2D>());
								if(_isTouching2 == true && bonus.enemiesInGame[i].name == bonus.enemiesInGame[y].name && bonus.enemiesInGame[y] != gameObject)
								{
									return 3;
								}
								else
									continue;
							}
							else 
								continue;
						}
						return 2;
					}
					else continue;
				}
				else continue;
			}
			return 1;
		}
		else
			return 1;
	}

	//function that checks whether an enemy touches another enemy
	private void TouchingSameEnemy()
	{
		bool leftEnemy = true;
		bool rightEnemy = true;
		RaycastHit2D[] leftRay = Physics2D.RaycastAll(transform.position,transform.right,-10f);
		RaycastHit2D[] rightRay = Physics2D.RaycastAll(transform.position,transform.right,10f);
		for(int i = 0; i < leftRay.Length; i++)
		{
			if(enemyBC.IsTouching(leftRay[i].collider))
			{
				if(gameObject != leftRay[i].transform.gameObject)
				{
					if(gameObject.name == leftRay[i].collider.name)
					{
						gameObject.transform.FindChild("Sword").gameObject.SetActive(true);
						leftEnemy = true;
					}
					else
					{
						leftEnemy = false;
					}
				}
			}
		}
		for(int y = 0; y < rightRay.Length; y++)
		{
			if(enemyBC.IsTouching(rightRay[y].collider))
			{
				if( gameObject != rightRay[y].transform.gameObject)
				{
					if(gameObject.name == rightRay[y].collider.name)
					{
						gameObject.transform.FindChild("Sword").gameObject.SetActive(true);
						rightEnemy = true;
					}
					else
					{
						rightEnemy = false;
					}
				}
			}
		}

		if(rightEnemy == false && leftEnemy == false)
		{
			gameObject.transform.FindChild("Sword").gameObject.SetActive(false);
		}
	}

	//function that stops an enemy
	public IEnumerator StopEnemy()
	{
		if(gameObject != null)
		{	
			enemyCanMove = false;
			float mass = enemyRB.mass;
			enemyRB.mass = 100f;
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
			{
				animator.SetBool("isAttacking",false);
			}
			yield return new WaitForSeconds(5f);
			if(gameObject.transform.GetComponentsInChildren<Spell>() != null)
			{
				//check if there are more than one dizzy spell component 
				Spell[] dizzySpells = gameObject.transform.GetComponentsInChildren<Spell>();
				if(dizzySpells.Length >= 2)
				{
					AchievementManager.Instance.EarnAchievement("Can I go?");
					GooglePlayManager.Instance.UnlockAchievementById("CgkIkti4j78fEAIQFA");
					yield return new WaitForSeconds(5f);
				}
				foreach(Spell dizzy in dizzySpells)
				{
					Destroy(dizzy.gameObject);
				}
			}
			enemyCanMove = true;
			enemyRB.mass = mass;
		}
	}
		
	//function that slows an enemy
	public IEnumerator SlowEnemy()
	{
			gameObject.transform.FindChild("Slow").gameObject.SetActive(true);
			float originalSpeed = speed;
			speed /= 2;
			float mass = enemyRB.mass;
			enemyRB.mass = 100f;

			yield return new WaitForSeconds(4f);

			gameObject.transform.FindChild("Slow").gameObject.SetActive(false);		
			enemyRB.mass = mass;
			speed = originalSpeed;
	}

	//function that handles fire on the ground
	private IEnumerator FireAoE(GameObject particleFire, Spell _spell)
	{
		if(particleFire != null && enemyBC != null && _spell != null)
		{
			achievement.EarnAchievement("Engulfed in flames");
			GooglePlayManager.Instance.IncrementAchievementById("CgkIkti4j78fEAIQFQ",1);
			float fireDmg = _spell.spellDamage / 3f;
			while(particleFire != null && particleFire.GetComponent<BoxCollider2D>().IsTouching(enemyBC))
			{
				ReceiveDamage(fireDmg * magicalDefence);
				yield return new WaitForSeconds(1f);
			}
		}
	}
		
	public void SlowMovement()
	{
		StartCoroutine("SlowEnemy");
	}

}
