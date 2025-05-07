using UnityEngine;

public class BoxTrigger : MonoBehaviour
{
    [SerializeField] private Box _box;

    private void OnTriggerEnter(Collider other)
    {
        Game.Instance.Single<PlayerBase>().Box = _box;
        Game.Instance.Single<PlayerBase>().Attack();
    }
}