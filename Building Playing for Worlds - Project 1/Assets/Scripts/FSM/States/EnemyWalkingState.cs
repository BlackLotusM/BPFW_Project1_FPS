using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RayWenderlich.Unity.StatePatternInUnity;

public class EnemyWalkingState : State
{
	public EnemyWalkingState(Enemy agent, StateMachine stateMachine) : base(agent, stateMachine)
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
		if (agent.transform.position.y < agent.Target.position.y)
		{
			agent.transform.position += new Vector3(0, 000.1f, 0);
			agent.GetComponent<Rigidbody>().useGravity = false;
		}

		if (Vector3.Distance(agent.transform.position, agent.Target.transform.position) < agent.TargetEngagementRange)
		{
			stateMachine.ChangeState(agent.Shooting);
		}
		if(Vector3.Distance(agent.transform.position, agent.Target.transform.position) > agent.TargetDetectionRange)
		{
			stateMachine.ChangeState(agent.Searching);
		}
	}
}
