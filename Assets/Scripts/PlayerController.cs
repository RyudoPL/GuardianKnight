using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	//reference to player's animator
	private Animator animator;

	//player's speed
	public float speed = 10f;

	//player's attack power
	public float attackPower;

	//vector of player's movement
	private Vector3 moveAmount;

	private bool isNextRound = false;

	public LayerMask levelMask;

	//reference to attack animation clip
	public AnimationClip attackAnimation;

	//bool that check whether player can move
	public bool canGo = true;

	//reference to attack's sfx
	private AudioSource attackClip;

	//singleton
	private  static PlayerController instance;

	public static PlayerController Instance
	{
		get
		{
			if(instance == null)
			{
				instance = GameObject.FindObjectOfType<PlayerController>();
			}
			return PlayerController.instance;
		}
	}

	void Awake()
	{
		//sets private variables
		animator = GetComponent<Animator>();
		attackClip = GetComponent<AudioSource>();

	}

	// Use this for initialization
	void Start () {
		animator.SetBool("isKnight2Attacking",false);
	}

	void OnLevelWasLoaded()
	{
		if(SceneManager.GetActiveScene().name == "Arcade")
		{
			gameObject.transform.position = new Vector3(-12,-2);
		}
		if(SceneManager.GetActiveScene().name == "Arena")
		{
			gameObject.transform.position = new Vector3(-11.5f,1);
		}

		canGo = true;
		animator.SetBool("isKnight2Attacking",false);
	}

	// Update is called once per frame
	void Update () 
	{
		//check if player is standing still or moving
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("Knight2Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Knight2Walk") || 
			animator.GetCurrentAnimatorStateInfo(0).IsName("Knight2Attack"))
		{
			//
			Touch[] playerTouch = Input.touches; //do pozbycia
			for(int i = 0; i < Input.touchCount; i++)
			{
				playerTouch[i] = Input.GetTouch(i);

			if(Time.timeScale == 1 || Time.timeScale == 0.5f)
			{
				//if(Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
					if(Input.touchCount >= 1 && playerTouch[i].phase == TouchPhase.Moved)
				{
					//Touch playerTouch = Input.GetTouch(0);

						if(canGo == true)
						{
							//move the player
							MovePlayer();

							//turn on walking animation
							animator.SetBool("isKnight2Walking",true);

							//turn off attack animation
							animator.SetBool("isKnight2Attacking",false);
						}
				}
				else
				{
					//turn off walking animation
					animator.SetBool("isKnight2Walking",false);	
				}
				
		}
		
				

		if(GameObject.Find("TopBorder") != null)
		{
			//checks if player is touching TopBorder
			if(gameObject.GetComponent<BoxCollider2D>().IsTouching(GameObject.Find("TopBorder").GetComponent<BoxCollider2D>()))
			{
				//if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
						if(Input.touchCount > 0 && playerTouch[i].phase == TouchPhase.Moved)
				{
					//moveAmount = Input.GetTouch(0).deltaPosition;
							moveAmount = playerTouch[i].deltaPosition;
					
					//checks whether player wants to move down
					if(moveAmount.y < 0)
					{
						//player can move
						canGo = true;
					}
				}
				else
					canGo = false;
			}
		}

			if(GameObject.Find("DownBorder"))
			{
				//check if the player hit DownBorder
				if(gameObject.GetComponent<BoxCollider2D>().IsTouching(GameObject.Find("DownBorder").GetComponent<BoxCollider2D>()))
				{
					//(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
						if(Input.touchCount > 0 && playerTouch[i].phase == TouchPhase.Moved)
					{
						//moveAmount = Input.GetTouch(0).deltaPosition;
							moveAmount = playerTouch[i].deltaPosition;
						if(moveAmount.y > 0)
						{
							//player can move
							canGo = true;
						}
					}	
					else
						canGo = false;
				}	
			}
		}
		}


		if(animator.GetCurrentAnimatorStateInfo(0).IsName("Knight2Attack") && isNextRound == false)
		{
			RaycastHit2D attackHit = Physics2D.Raycast(transform.position,transform.right,2.4f,levelMask.value);
			if(attackHit.collider != null)
			{
				if(GetEnemy(attackHit) != null)
				{
					EnemyController enemy = GetEnemy(attackHit);
					isNextRound = true;
					//start attack animation
					StartCoroutine(AttackAnimationEnd(enemy));
				}
			}
		}

	}

	//function that moves the player
	void MovePlayer()
	{
		Vector3 fingerPos = Input.GetTouch(0).position;
		fingerPos.z = 10;
		Vector3 realWorldPos = Camera.main.ScreenToWorldPoint(fingerPos);
		if(realWorldPos.x < gameObject.transform.position.x + 2)
		{
			moveAmount = Camera.main.ScreenToWorldPoint (new Vector3 (Input.GetTouch(0).position.x,Input.GetTouch(0).position.y, 10f));
			if(GameObject.Find("TopBorder") != null)
			{
				if(GameObject.Find("TopBorder").transform.position.y < moveAmount.y)
				{
					return;
				}
				else
					transform.position = new Vector3(gameObject.transform.position.x, moveAmount.y, moveAmount.z);
			}

			if(GameObject.Find("DownBorder") != null)
			{
				if(GameObject.Find("DownBorder").transform.position.y > moveAmount.y)
				{
					return;
				}
				else
					transform.position = new Vector3(gameObject.transform.position.x, moveAmount.y, moveAmount.z);
			}
		}

	}

	private void OnTriggerEnter2D(Collider2D _obj)
	{
		if(_obj.tag == "Enemy")
		{
			//turn on attack animation when colliding with an enemy
			animator.SetBool("isKnight2Attacking",true);


			if(SceneManager.GetActiveScene().name == "Arena")
			{
				//in Arena Mode - physical attack was used
				GameManager.instance.physicalAttackUsed = true;
			}
		}	

		//when colliding with a border - player cannot move
		if(_obj.tag == "Border")
		{
			//player cannot move
			canGo = false;
		}
	}

	private void OnTriggerStay2D(Collider2D _obj)
	{
		if(_obj.tag == "Enemy" && animator.GetCurrentAnimatorStateInfo(0).IsName("Knight2Attack") == false)
		{
			//turn on attack animation
			animator.SetBool("isKnight2Attacking",true);
		}

	}

	private void OnTriggerExit2D(Collider2D _obj)
	{
		if(_obj.tag == "Enemy")
		{
			animator.SetBool("isKnight2Attacking",false);
			isNextRound = false;
		}
	}

	//Courutine that controls player's attack animation
	private IEnumerator AttackAnimationEnd(EnemyController _enemy)
	{
		//deal damage to enemy
		_enemy.ReceiveDamage(attackPower * _enemy.physicalDefence);
		attackClip.Play();

		//checks whether enemy is dead
		if(_enemy.CheckIfDead() == true)
		{
			//turn off attack animation
			animator.SetBool("isKnight2Attacking",false);
			attackClip.Stop();


			isNextRound = false;
		}
		else
		{
			//wait until attack animation ends
			yield return new WaitForSeconds(attackAnimation.length);
			isNextRound = false;
		}
	}

	//funtion that gets an enemy out of raycasy and returns it
	private EnemyController GetEnemy(RaycastHit2D _attackHit)
	{
		EnemyController enemy = _attackHit.collider.GetComponent<EnemyController>();
		return enemy;
	}

}
