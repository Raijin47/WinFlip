using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoContainer
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private Image[] _imageHealth;
    [SerializeField] private ParticleSystem _healParticle;

    private Tween _moneyTween;
    private Sequence _sequence;

    private float _money;
    private int _health = 3;

    public float Money
    {
        get => _money;
        set
        {
            _money = Mathf.Round(value);
            _moneyText.text = _money.ToString();
        }
    }

    public void AddMoney(float value)
    {
        _moneyTween?.Kill();
        _moneyTween = DOTween.To(() => Money, x => Money = x, value, 1f);
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

        if (Health >= 3) return;
        
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