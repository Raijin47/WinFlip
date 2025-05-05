using System.Collections;
using TMPro;
using UnityEngine;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private string _start;
    [SerializeField] private string _end;
    private TextMeshProUGUI _text;
    private float _current;
    private readonly float TimeToReachTarget = 5f;
    private Coroutine _changeProcess;

    private void Awake() => _text = GetComponent<TextMeshProUGUI>();

    private void OnEnable()
    {
        Game.Wallet.OnSetMoney += Set;
        Game.Wallet.OnAddMoney += Add;
        Game.Wallet.OnSpendMoney += Spend;
        Set(Game.Wallet.Money);
    }

    private void OnDisable()
    {
        Game.Wallet.OnAddMoney -= Add;
        Game.Wallet.OnSpendMoney -= Spend;
        Game.Wallet.OnSetMoney -= Set;

        if (_changeProcess != null)
            StopCoroutine(_changeProcess);
    }

    private void Set(float value)
    {
        _current = value;
        _text.text = $"{_start}{Mathf.RoundToInt(_current)}{_end}";
    }

    private void Add()
    {
        if (!gameObject.activeInHierarchy) return;
        if (_changeProcess != null)
            StopCoroutine(_changeProcess);
        _changeProcess = StartCoroutine(ChangeMoneyProcess());
    }

    private void Spend()
    {
        if (!gameObject.activeInHierarchy) return;
        if (_changeProcess != null)
            StopCoroutine(_changeProcess);
        _changeProcess = StartCoroutine(ChangeMoneyProcess());
    }

    private IEnumerator ChangeMoneyProcess()
    {
        float elapsedTime = 0;

        while(elapsedTime < TimeToReachTarget)
        {
            Set(Mathf.Lerp(_current, Game.Wallet.Money, elapsedTime / TimeToReachTarget));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Set(Game.Wallet.Money);
    }
}