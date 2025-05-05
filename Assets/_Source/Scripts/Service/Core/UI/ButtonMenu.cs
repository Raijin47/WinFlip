public class ButtonMenu : ButtonBase
{
    protected override void Click()
    {
        base.Click();

        Game.Instance.Menu();
    }
}