using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RayWenderlich.Unity.StatePatternInUnity;

public class EnemySearchingState : State
{
	public EnemySearchingState(Enemy agent, StateMachine stateMachine) : base(agent, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();
		agent.transform.position += agent.transform.forward * agent.f_MoveSpeed * Time.deltaTime;
		agent.GetComponent<Rigidbody>().useGravity = true;
		if (Vector3.Distance(agent.Target.transform.position, agent.transform.position) <= agent.TargetDetectionRange)
		{
			stateMachine.ChangeState(agent.Walking);
		}
	}
}
