using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour 
{
	//reference to spell's damage
	public int spellDamage;

	//reference to spell's damage radius
	private float spellRadiusX;
	private float spellRadiusY;

	//reference to duration of a spell
	public float spellTime;

	//spell's cost (in mana)
	public int energyCost;

	//bool that checks whether a spell was already used (for achievement purposes)
	public bool wasUsed;


	//function that calculates the damage made by a spell
	public float CalculateDamage (Vector2 _targetPosition, Vector2 _spellHit, Vector2 _aoeBorder)
	{
		spellRadiusX = _aoeBorder.x - _spellHit.x;
		spellRadiusY = _aoeBorder.y - _spellHit.y;

		// Create a vector from the shell to the target.
		Vector2 _explosionToTarget = _targetPosition - _spellHit;

		// Calculate the distance from the shell to the target.
		float _explosionDistance = _explosionToTarget.magnitude;

		float _spellRadius = Mathf.Sqrt((spellRadiusX * spellRadiusX) + (spellRadiusY * spellRadiusY));

		// Calculate the proportion of the maximum distance (the explosionRadius) the target is away.
		float _relativeDistance = (_spellRadius - _explosionDistance) / (_spellRadius);

		// Calculate damage as this proportion of the maximum possible damage.
		float _damage = _relativeDistance * spellDamage;

		// Make sure that the minimum damage is always 0.
		_damage = Mathf.Max (0f, _damage);

		//return how much damage we deal
		return _damage;
	}

	//function that heals the player
	public void Heal()
	{
		CastleHealth castleHP = GameObject.Find("CastleHealth").GetComponent<CastleHealth>();
		castleHP.currentHealth += spellDamage;

		//prevents adding more health than player's maxHealth
		if(castleHP.currentHealth > castleHP.startingHealth)
		{
			castleHP.currentHealth = castleHP.startingHealth;
		}
		Destroy(GameObject.Find("Hover-Recovery"));
	}
}

