using UnityEngine;
using System.Collections;

public class RecoverySpell : Skill {


	public override void Use(GameObject hover, RaycastHit2D hit)
	{
		Spell spell = hover.GetComponent<Spell>();

		//variable that store AoE Holder's BoxCollider2D
		Collider2D holderBC = hover.GetComponentInChildren<Collider2D>();

		//get an array of colliders which are within spell's radius range
		Collider2D collider = Physics2D.OverlapArea(holderBC.bounds.min,holderBC.bounds.max);

		if(collider.gameObject.tag == "Wall")
		{
			CastleHealth health = GameObject.Find("CastleHealth").GetComponent<CastleHealth>();
			health.currentHealth += spell.spellDamage;
		}

		//destroy the hover object
		Destroy(GameObject.Find("Hover"));
	}
}
