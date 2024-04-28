using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
	public float moveSpeed = 2f;				// The speed the enemy moves at.
	
	public bool flipOnStart = false;			// Change movement direction on start.
	

	public enum Behaviour { Hold, FreeRange, Patrol };
	public Behaviour behaviour = Behaviour.Hold;

	// if behaviour is patrol
	public enum PatrolType { PingPong };
	public PatrolType patrolType = PatrolType.PingPong;
	public float arriveDistance = 0.3f;	// how close to consider I made it?
	public Transform[] patrolPoints;


	private Transform frontCheck;		// Reference to the position of the gameobject used for checking if something is in front.
	private Transform topCheck;			// Reference to the position of the gameobject used for checking if the player is above.
	private SpriteRenderer ren;			// Reference to the sprite renderer.

	
	void Awake ()
	{
		// Setting up the references.
		ren = GetComponent<SpriteRenderer>();
	}


	void Start ()
	{
		// If flip on start is true...
		if (flipOnStart)
		{
			// Flip the enemy movement direction.
			Flip();
		}
	}


	// simple fixed update that has us move back and forwards between items marked as "obstacles"
	void FixedUpdate ()
	{

		// and peform the behaviour as asked for by the player
		switch (behaviour) 
		{
		case Behaviour.Patrol:		BehaviourPatrolUpdate();		break;
		}

	}


	// if patrolling, be heading towards the next patrol point
	private Transform patrolTarget;
	private int patrolIndex = 0;
	private int patrolDI = 1;


	private void BehaviourPatrolUpdate()
	{
		// first use -- set up our first patrol target point
		if (patrolTarget == null) PatrolSetup ();

		// have we arrived near our patrol target
		if (IsPatrolWaypointArrived ()) PatrolWaypointAdvance ();


		// ensure we are heading in the right direction
		if (patrolTarget.position.y < transform.position.y && transform.localScale.y > 0)
			Flip ();
		if (patrolTarget.position.y > transform.position.y && transform.localScale.y < 0)
			Flip ();

		// Set the enemy's velocity to moveSpeed in the x direction.
		GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, patrolDI * moveSpeed);

	}


	private void PatrolSetup()
	{
		patrolIndex = 0;
		patrolTarget = patrolPoints[patrolIndex];
	}


	// have we arrived near our patrol target
	private bool IsPatrolWaypointArrived()
	{
		return (Mathf.Abs (patrolTarget.position.y - transform.position.y) < arriveDistance);
	}


	private void PatrolWaypointAdvance ()
	{
		// go to the next patrol point
		patrolIndex += patrolDI;

		// did we go off the end?
		if (patrolIndex >= patrolPoints.Length || patrolIndex < 0) 
		{
if (patrolType == PatrolType.PingPong)
			{
				patrolIndex -= patrolDI;	// undo the last add
				patrolIndex -= patrolDI;	// second time gets us to one before
				patrolDI = -patrolDI;		// and going back through the points the opposite direction
			}
		}

		// and this is the new target point
		patrolTarget = patrolPoints[patrolIndex];
	}



	private void Flip()
	{
		// Multiply the y component of localScale by -1.
		Vector3 enemyScale = transform.localScale;
		enemyScale.y *= -1;
		transform.localScale = enemyScale;
	}
}