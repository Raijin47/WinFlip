using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Box : MonoBehaviour, IPointerClickHandler
{
    public event Action<Box> OnClick;

    [SerializeField] private GameObject _object;
    [SerializeField] private GameObject _particle;
    [SerializeField] private GameObject _damageParticle;


    private BoxType _boxType;
    public bool Interactable { get; set; } = true;
    private int _floor;
    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void Init(BoxType type, int value)
    {
        _boxType = type;
        _floor = value;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Interactable) return;

        OnClick?.Invoke(this);
        _collider.enabled = false;
    }

    public void Execute()
    {
        _particle.SetActive(true);
        _object.SetActive(false);
        Game.Instance.Single<Door>().IsOpen = true;

        var floor =  Game.Instance.Single<FloorHandler>();
        var handler = Game.Instance.Single<GameHandler>();

        switch (_boxType)
        {
            case BoxType.Up:
                floor.NextFloor = floor.CurrentFloor + 1;
                handler.AddMoney(handler.Money + 5);
                handler.Send(true);
                break;
            case BoxType.Down:
                floor.NextFloor = floor.CurrentFloor - 1;
                handler.Send(false);
                break;
            case BoxType.DoubleUp:
                floor.NextFloor = floor.CurrentFloor + 2;
                handler.AddMoney(handler.Money + 10);
                handler.Send(true);
                break;
            case BoxType.Damage:
                floor.NextFloor = floor.CurrentFloor + 1;
                handler.Damage();
                Game.Audio.PlayClip(0, 0.5f);
                _damageParticle.SetActive(true);
                handler.Send(false);
                break;
            case BoxType.MoneyBoost:
                floor.NextFloor = floor.CurrentFloor + 1;
                handler.AddMoney(handler.Money + 5);
                handler.Send(true);
                break;
            case BoxType.Heal: 
                floor.NextFloor = floor.CurrentFloor + 1;
                Game.Audio.PlayClip(1);
                handler.Heal();
                handler.Send(true);
                break;
        }
    }
}