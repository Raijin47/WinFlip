public class PageResult : PagePause
{
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