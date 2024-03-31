using System.Linq;
using UnityEngine;

public class ShieldController : InteractiveObject
{
    [SerializeField] private ShieldController[] otherShields;
    [SerializeField] private GameObject Spear;
    [SerializeField] private DoorController DoorToOpen;

    public override void InteractWithObject()
    {
        objectAudio.PlayOneShot(objectAudio.clip);
        Spear.SetActive(true);

        if (ShouldOpenDoor() && DoorToOpen != null)
        {
            DoorToOpen.InteractWithObject();
        }
    }

    protected override bool HasDoneRequirements()
    {
        if (otherShields.Length > 0)
        {
            int completedShieldsCount = otherShields.Where(shield => shield.IsConditionCompleted()).Count();
            int collectedSpearsCount = requiredObjects.Where(spear => spear.IsConditionCompleted()).Count();

            return collectedSpearsCount > completedShieldsCount;
        }
        else
        {
            return base.HasDoneRequirements();
        }
    }

    public override bool IsConditionCompleted()
    {
        return base.IsConditionCompleted();
    }

    private bool ShouldOpenDoor() => (otherShields.All(shield => shield.IsConditionCompleted()) || otherShields.Length == 0);
}
