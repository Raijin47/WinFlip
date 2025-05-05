using DG.Tweening;

public class PageGame : PanelBase
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
            Join(_components[0].DOLocalMoveY(-300, _delay).SetEase(Ease.OutBack)).
            Join(_components[1].DOScale(1, _delay).From(5).SetEase(Ease.OutBack)).
            Join(_components[2].DOLocalMoveX(0, _delay).From(-300).SetEase(Ease.OutBack)).
            Join(_components[3].DOLocalMoveX(0, _delay).From(300).SetEase(Ease.OutBack)).

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