using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private int _floor;
    [SerializeField] private BoxType[] _types;

    private readonly List<Box> Boxes = new();
    private readonly List<BoxType> Numbers = new();

    private void Awake()
    {
        Boxes.AddRange(GetComponentsInChildren<Box>());
        Numbers.AddRange(_types);

        foreach(Box box in Boxes)
        {
            var r = Random.Range(0, Numbers.Count);

            box.Init(Numbers[r], _floor);
            Numbers.RemoveAt(r);
        }     
    }

    private void OnEnable()
    {
        foreach (Box box in Boxes)
        {
            box.OnClick += Box_OnClick;
            box.Interactable = true;
        }
    }

    private void OnDisable()
    {
        foreach (Box box in Boxes)
            box.OnClick -= Box_OnClick;
    }

    private void Box_OnClick(Box target)
    {
        foreach (Box box in Boxes)
            box.Interactable = false;

        Game.Instance.Single<PlayerBase>().SetDestination(target.transform.parent.position);
    }
}