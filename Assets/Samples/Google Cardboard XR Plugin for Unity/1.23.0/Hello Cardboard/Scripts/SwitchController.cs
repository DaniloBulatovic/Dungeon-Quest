using System.Linq;
using UnityEngine;

public class SwitchController : InteractiveObject
{
    private bool switchLever;
    public bool isSwitchOn;
    [SerializeField] private bool shouldSwitchBeOn;
    [SerializeField] private GameObject Lever;
    [SerializeField] private SwitchController[] otherSwitches;
    [SerializeField] private DoorController DoorToOpen;

    public override void InteractWithObject()
    {
        objectAudio.PlayOneShot(objectAudio.clip);
        switchLever = true;
    }

    public override bool IsConditionCompleted()
    {
        return isSwitchOn == shouldSwitchBeOn;
    }

    private void FixedUpdate()
    {
        if (switchLever)
        {
            PlaySwitchAnimation();
        }
    }

    private void PlaySwitchAnimation()
    {
        if (isSwitchOn)
        {
            Lever.transform.Rotate(-Mathf.LerpAngle(0.0f, 90.0f, Time.deltaTime), 0, 0);
            if (Lever.transform.eulerAngles.x >= 45.0f && Lever.transform.eulerAngles.x <= 315.0f)
            {
                Lever.transform.eulerAngles = new Vector3(315.0f, Lever.transform.eulerAngles.y, Lever.transform.eulerAngles.z);
                switchLever = false;
                isSwitchOn = false;
                TryToOpenDoor();
            }
        }
        else if (!isSwitchOn)
        {
            Lever.transform.Rotate(Mathf.LerpAngle(0.0f, 90.0f, Time.deltaTime), 0, 0);
            if (Lever.transform.eulerAngles.x >= 45.0f && Lever.transform.eulerAngles.x <= 315.0f)
            {
                Lever.transform.eulerAngles = new Vector3(45.0f, Lever.transform.eulerAngles.y, Lever.transform.eulerAngles.z);
                switchLever = false;
                isSwitchOn = true;
                TryToOpenDoor();
            }
        }
    }

    private void TryToOpenDoor()
    {
        if (ShouldOpenDoor() && DoorToOpen != null)
        {
            DoorToOpen.InteractWithObject();
        }
    }

    private bool ShouldOpenDoor() => (otherSwitches.All(s => s.IsConditionCompleted()) || otherSwitches.Length == 0) && IsConditionCompleted();
}
