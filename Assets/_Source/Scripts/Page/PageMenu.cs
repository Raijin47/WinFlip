using DG.Tweening;
using Utilities.Container;

public class PageMenu : PanelBase, IContainer
{
    protected override void Hide()
    {
        _sequence.
            Append(_canvas.DOFade(0, _delay)).
            Join(_components[0].DOScale(0, _delay).SetEase(Ease.InBack));
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
        IsActive = true;
        _canvas.alpha = 1;

        Game.Instance.Add(this);
    }
}