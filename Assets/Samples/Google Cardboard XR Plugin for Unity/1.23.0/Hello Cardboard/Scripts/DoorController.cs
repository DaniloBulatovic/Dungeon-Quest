using UnityEngine;

public class DoorController : InteractiveObject
{
    private bool openDoor;
    private bool doorIsOpened;
    private float currentDoorAngle;

    protected override void Start()
    {
        base.Start();
        currentDoorAngle = transform.eulerAngles.y;
    }

    public override void InteractWithObject()
    {
        if (doorIsOpened) return;

        objectAudio.PlayOneShot(objectAudio.clip);
        doorIsOpened = true;
        openDoor = true;
    }

    private void FixedUpdate()
    {
        if (openDoor)
        {
            PlayOpenDoorAnimation();
        }
    }

    private void PlayOpenDoorAnimation()
    {
        transform.Rotate(0, Mathf.LerpAngle(0.0f, 90.0f, Time.deltaTime), 0);
        if (transform.eulerAngles.y - currentDoorAngle > 90.0f || transform.eulerAngles.y - currentDoorAngle < -269.0f)
        {
            openDoor = false;
        }
    }
}
