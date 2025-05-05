using DG.Tweening;

public class PageWin : PanelBase
{
    protected override void Hide()
    {
        _sequence.
            Append(_canvas.DOFade(0, _delay));
    }

    protected override void Show()
    {
        _sequence.AppendInterval(_delay).
            Append(_canvas.DOFade(1, _delay)).
            Join(_components[0].DOScale(1, _delay).From(0).SetEase(Ease.OutBack)).
            OnComplete(OnShowComplated);
    }

    protected override void Start()
    {
        base.Start();

        Game.Action.OnWin += Enter;
    }

    private void OnDestroy()
    {
        Game.Action.OnWin -= Enter;
    }
}