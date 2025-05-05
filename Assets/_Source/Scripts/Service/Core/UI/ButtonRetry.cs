public class ButtonRetry : ButtonBase
{
    protected override void Click()
    {
        base.Click();

        Game.Instance.Retry();
    }
}