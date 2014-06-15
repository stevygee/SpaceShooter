using UnityEngine;
using System.Collections;

/// <summary>
/// Player controller and behavior
/// </summary>
public class PlayerScript : MonoBehaviour
{
	/// <summary>
	/// 1 - The speed of the ship
	/// </summary>
	public Vector2 speed = new Vector2(10, 10);
	
	// 2 - Store the movement
	private Vector2 movement;
	
	void Awake() {
		// called when object is created
	}
	
	void Start() {
		// called after awake() and script is enabled
	}

	void Update()
	{
		// 3 - Retrieve axis information
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");
		
		// 4 - Movement per direction
		movement = new Vector2(
								speed.x * inputX,
								speed.y * inputY);

		// 5 - Shooting
		bool shoot = Input.GetButton("Fire1");
		shoot |= Input.GetButton("Fire2");
		// Careful: For Mac users, ctrl + arrow is a bad idea
		
		if (shoot)
		{
			WeaponScript weapon = GetComponent<WeaponScript>();
			if (weapon != null)
			{
				// false because the player is not an enemy
				weapon.Attack(false);
			}
		}

		// 6 - Make sure we are not outside the camera bounds
		var dist = (transform.position - Camera.main.transform.position).z;
		
		var leftBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
			).x;
		
		var rightBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(1, 0, dist)
			).x;
		
		var topBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
			).y;
		
		var bottomBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 1, dist)
			).y;
		
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
			Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
			transform.position.z
			);
	}
	
	void FixedUpdate()
	{
		// 5 - Move the game object
		rigidbody2D.velocity = movement;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		// is invoked when another collider is touching this object collider.

		bool damagePlayer = false;
		
		// Collision with enemy
		EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
		if (enemy != null)
		{
			// Kill the enemy
			HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
			if (enemyHealth != null) enemyHealth.Damage(enemyHealth.hp);
			
			damagePlayer = true;
		}
		
		// Damage the player
		if (damagePlayer)
		{
			HealthScript playerHealth = this.GetComponent<HealthScript>();
			if (playerHealth != null) playerHealth.Damage(1);
		}
	}
	
	void OnCollisionExit2D(Collision2D coll)
	{
		// is invoked when another collider is not touching this object collider anymore.
	}
	
	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		// is invoked when another collider marked as a "Trigger" is touching this object collider.
	}
	
	void OnTriggerExit2D(Collider2D otherCollider)
	{
		// is invoked when another collider marked as a "Trigger" is not touching this object collider anymore.
	}
	
	void Destroy()
	{
	}
}