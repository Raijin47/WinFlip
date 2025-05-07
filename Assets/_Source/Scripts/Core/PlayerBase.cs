using UnityEngine;
using UnityEngine.AI;

public class PlayerBase : MonoContainer
{
    private NavMeshAgent _agent;
    private Animator _animator;

    public Box Box { get; set; }
    private bool _isDeath;

    protected override void Awake()
    {
        base.Awake();

        _animator = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        Game.Action.OnLose += Action_OnLose;
    }

    private void Action_OnLose()
    {
        _isDeath = true;

        _agent.enabled = false;
        _animator.SetTrigger("Die");
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Game.Action.OnLose -= Action_OnLose;
    }

    public void SetDestination(Vector3 target)
    {
        if (_isDeath) return;
        _agent.SetDestination(target);
    }

    public void ResetPosition()
    {
        _agent.isStopped = true;
        _agent.Warp(Vector3.zero);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
        Invoke(nameof(Execute), 0.7f);
    }

    private void Execute() => Box.Execute();

    private void Update()
    {
        if (_isDeath) return;

        _animator.SetFloat("Speed", _agent.desiredVelocity.magnitude);
    }
}