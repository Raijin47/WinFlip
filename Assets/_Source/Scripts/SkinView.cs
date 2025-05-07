using UnityEngine;

public class SkinView : MonoBehaviour
{
    [SerializeField] private Material[] _materials;

    private SkinnedMeshRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _renderer.material = _materials[Game.Data.Saves.CurrentEquipSkin];
    }
}
