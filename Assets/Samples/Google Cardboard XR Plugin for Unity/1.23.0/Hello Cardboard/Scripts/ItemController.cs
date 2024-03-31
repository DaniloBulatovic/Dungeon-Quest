public class ItemController : InteractiveObject
{

    protected override void Update()
    {
        base.Update();
        if (IsConditionCompleted())
        {
            if (!objectAudio.isPlaying)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public override void InteractWithObject()
    {
        base.InteractWithObject();

        if (CompareTag("Goblet"))
        {
            GameManager.Instance.EndGame();
        }
    }
}
