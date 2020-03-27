using RayWenderlich.Unity.StatePatternInUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

	public GameObject test;
	public float f_RotSpeed = 3.0f, f_MoveSpeed = 0.01f;
	[SerializeField] public Transform target = default;    // The target for this enemy. The enemy will try to destroy the target.
	[SerializeField] private float targetDetectionRange = 35f;  // How far the enemy can "sense" the target. This works through walls atm
	[SerializeField] private float targetEngagementRange = 15f; // How far the enemy can shoot at the target
	[SerializeField] private float moveDestinationSetInterval = 0.1f;   // How often the enemy is told to move to the target position.

	[SerializeField] private StateMachine behaviourSM;

	[SerializeField] private EnemyWalkingState walking;
	[SerializeField] private EnemySearchingState searching;
	[SerializeField] private EnemyShootingState shooting;
	public Transform Target { get => target; set => target = value; }

	public float TargetDetectionRange { get => targetDetectionRange; set => targetDetectionRange = value; }
	public float TargetEngagementRange { get => targetEngagementRange; set => targetEngagementRange = value; }
	public float MoveDestinationSetInterval { get => moveDestinationSetInterval; set => moveDestinationSetInterval = value; }

	public EnemyWalkingState Walking { get => walking; set => walking = value; }
	public EnemySearchingState Searching { get => searching; set => searching = value; }
	public EnemyShootingState Shooting { get => shooting; set => shooting = value; }

	private void Start()
	{
		test = GameObject.Find("Cylinder");
		target = test.transform;
		behaviourSM = new StateMachine();

		searching = new EnemySearchingState(this, behaviourSM);
		walking = new EnemyWalkingState(this, behaviourSM);
		shooting = new EnemyShootingState(this, behaviourSM);

		behaviourSM.Initialize(searching);
	}

	private void Update()
	{
		behaviourSM.CurrentState.HandleInput();
		behaviourSM.CurrentState.LogicUpdate();
        transform.rotation = Quaternion.Slerp(transform.rotation
                                              , Quaternion.LookRotation(target.position - transform.position)
                                              , f_RotSpeed * Time.deltaTime);
	}

	public void remove()
	{
		Destroy(gameObject);
	}
	private void FixedUpdate()
	{
		behaviourSM.CurrentState.PhysicsUpdate();
	}
}