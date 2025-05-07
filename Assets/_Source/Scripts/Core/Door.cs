using DG.Tweening;
using UnityEngine;

public class Door : MonoContainer
{
    [SerializeField] private Transform _left, _right;

    private Sequence _sequence;

    public bool IsOpen
    {
        set
        {
            _sequence?.Kill();

            _sequence = DOTween.Sequence();

            _sequence.
                Append(_left.DOLocalMoveX(value ? -1.5f : 0, 1f)).
                Join(_right.DOLocalMoveX(value ? 1.5f : 0, 1f)).
                OnComplete(value ? OpenDoor : Execute);
        }
    }

    private void OpenDoor()
    {
        Game.Instance.Single<PlayerBase>().SetDestination(transform.position);
    }

    private void Execute()
    {
        Game.Instance.Single<FloorHandler>().InitNewFloor();
    }

    private void OnTriggerEnter(Collider other)
    {
        IsOpen = false;
    }
}