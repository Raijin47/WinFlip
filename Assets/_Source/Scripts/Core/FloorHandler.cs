using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloorHandler : MonoContainer
{
    [SerializeField] private CanvasGroup _canvas;
    [SerializeField] private TMP_Text _text;
    private Sequence _sequence;

    private readonly List<Floor> Floors = new();

    public int CurrentFloor { get; private set; }
    public int NextFloor { get; set; }

    protected override void Awake()
    {
        base.Awake();

        Floors.AddRange(GetComponentsInChildren<Floor>());
    }

    private void Start()
    {
        foreach (Floor floor in Floors)
            floor.gameObject.SetActive(false);

        Floors[0].gameObject.SetActive(true);
    }

    public void InitNewFloor()
    {
        _sequence?.Kill();

        _sequence = DOTween.Sequence();

        _sequence.Append(_canvas.DOFade(1, 0.5f).
            OnComplete(() => 
            {
                Floors[CurrentFloor].gameObject.SetActive(false);               
                Floors[NextFloor].gameObject.SetActive(true);

                CurrentFloor = NextFloor;

                _text.text = $"{CurrentFloor + 1}";
                Game.Instance.Single<PlayerBase>().ResetPosition();
            })).
            Append(_canvas.DOFade(0, 0.5f));
    }
}

public enum BoxType
{
    Up, Down, DoubleUp, MoneyBoost, Heal, Damage
}