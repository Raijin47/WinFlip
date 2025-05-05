using DG.Tweening;

public class PanelSettings : PanelBase
{
    protected override void Hide()
    {
        _sequence.
            Append(_canvas.DOFade(0, _delay));
    }

    protected override void Show()
    {
        _sequence.
            Append(_canvas.DOFade(1, _delay)).
            Join(_components[0].DOScale(1, _delay).From(0).SetEase(Ease.OutBack)).

            OnComplete(OnShowComplated);
    }
}