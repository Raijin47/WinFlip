using DG.Tweening;

public class PageLose : PanelBase
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

        Game.Action.OnLose += Enter;
    }

    private void OnDestroy()
    {
        Game.Action.OnLose -= Enter;
    }
}