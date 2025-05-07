using DG.Tweening;
using TMPro;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _priceText;

    [Space(10)]
    [SerializeField] private ButtonBase _preview;
    [SerializeField] private ButtonBase _action;
    [SerializeField] private ButtonBase _next;

    [Space(10)]
    [SerializeField] private Material[] _materials;

    private SkinnedMeshRenderer _renderer;
    private Tween _tween;
    private Tween _priceTween;

    private int Current
    {
        get => Game.Data.Saves.CurrentEquipSkin;
        set
        {
            Game.Data.Saves.CurrentEquipSkin = value;
        }
    }

    private void Awake()
    {
        _renderer = GetComponentInChildren<SkinnedMeshRenderer>();    
    }

    private void Start()
    {
        _tween = transform.DORotate(new Vector3(0, 360, 0), 4f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);

        _preview.OnClick.AddListener(Preview);
        _action.OnClick.AddListener(Action);
        _next.OnClick.AddListener(Next);

        Current = Game.Data.Saves.CurrentEquipSkin;

        UpdateUI();
        _renderer.material = _materials[Current];
    }

    private void Preview()
    {
        Current--;

        if (Current < 0) Current = _materials.Length - 1;

        UpdateUI();
    }

    private void Action()
    {
        if (Game.Data.Saves.IsPurchased[Current])
        {
            Game.Data.SaveProgress();
            Game.Instance.LoadScene(2, UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
        else
        {
            if (Game.Wallet.Spend(5000))
            {
                Game.Data.Saves.IsPurchased[Current] = true;
                UpdateUI();
                Game.Data.SaveProgress();
            }
        }
    }

    private void Next()
    {
        Current++;

        if (Current >= _materials.Length) Current = 0;

        UpdateUI();
    }

    private void UpdateUI()
    {
        _renderer.material = _materials[Current];

        bool isPurchased = Game.Data.Saves.IsPurchased[Current];

        _priceTween?.Kill();

        _priceTween = _priceText.transform.DOScale(isPurchased ? 0 : 1, 0.5f);
        _text.text = isPurchased ? "PLAY" : "BUY";
    }

    private void OnDestroy()
    {
        _tween?.Kill();
    }
}
