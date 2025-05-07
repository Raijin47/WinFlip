using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoContainer
{
    [SerializeField] private TextMeshProUGUI _moneyText, _addMoneyText;
    [SerializeField] private Image[] _imageHealth;
    [SerializeField] private ParticleSystem _healParticle;

    [SerializeField] private ParticleSystem _okey;
    [SerializeField] private ParticleSystem _miss;

    private Tween _moneyTween;
    private Sequence _sequence;

    public void Send(bool value)
    {
        if (value) _okey.Play();
        else _miss.Play();

        Game.Audio.PlayClip(value ? 2 : 3, 0.5f);
    }

    private float _money;
    private int _health = 2;

    public float Money
    {
        get => _money;
        set
        {
            _money = Mathf.Round(value);

            _addMoneyText.text = _money.ToString();
            _moneyText.text = _money.ToString();
        }
    }

    public void AddMoney(float value)
    {
        _moneyTween?.Kill();
        _moneyTween = DOTween.To(() => Money, x => Money = x, value, 1f);
    }

    private void Start()
    {
        Game.Action.OnLose += Action_OnLose;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Game.Action.OnLose -= Action_OnLose;
    }

    public void Action_OnLose()
    {
        Game.Wallet.Add(Mathf.RoundToInt(Money));
    }

    public int Health
    {
        get => _health;
        set
        {
            _health = value;
        }
    }

    public void Heal()
    {
        _healParticle.Play();

        if (Health >= 2) return;
        
        Kill();

        Health++;

        _sequence.
            Append(_imageHealth[Health].DOColor(Color.red, 1f)).
            Join(_imageHealth[Health].transform.DOScale(1f, 1f).SetEase(Ease.OutBack));
    }

    public void Damage()
    {
        Kill();

        if(Health == 0)
            Game.Action.SendLose();

        _sequence.
            Append(_imageHealth[Health].DOColor(Color.gray, 1f)).
            Join(_imageHealth[Health].transform.DOScale(0.8f, 1f).SetEase(Ease.InBack)).
            OnComplete(() => { Health--; });
    }

    private void Kill()
    {
        _sequence?.Kill();

        _sequence = DOTween.Sequence();
    }
}