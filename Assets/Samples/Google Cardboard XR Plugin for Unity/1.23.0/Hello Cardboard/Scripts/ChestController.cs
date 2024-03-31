using UnityEngine;

public class ChestController : InteractiveObject
{
    private bool interactWithChest;
    public bool isChestOpen;
    [SerializeField] private GameObject ChestLid;
    [SerializeField] private GameObject ItemInside;
    private float itemStartHeight;
    private float itemFloatSpeed = 2.0f;
    private float itemFloatHeight = 1.2f;

    protected override void Start()
    {
        base.Start();
        itemStartHeight = ItemInside.transform.position.y;
    }

    public override void InteractWithObject()
    {
        base.InteractWithObject();
        interactWithChest = true;
    }

    private void FixedUpdate()
    {
        if (interactWithChest)
        {
            PlayChestAnimation();
        }

        if (isChestOpen)
        {
            FloatItem();
        }
    }

    private void FloatItem()
    {
        ItemInside.transform.Translate(itemFloatSpeed * Time.deltaTime * Vector3.up, Space.World);
        if (ItemInside.transform.position.y - itemStartHeight > itemFloatHeight)
        {
            ItemInside.transform.position = new Vector3(ItemInside.transform.position.x, itemStartHeight + itemFloatHeight, ItemInside.transform.position.z);
        }
    }

    private void PlayChestAnimation()
    {
        if (!isChestOpen)
        {
            ChestLid.transform.Rotate(-Mathf.LerpAngle(0.0f, 60.0f, Time.deltaTime), 0, 0);
            if (ChestLid.transform.eulerAngles.x >= 0.0f && ChestLid.transform.eulerAngles.x <= 300.0f)
            {
                ChestLid.transform.eulerAngles = new Vector3(300.0f, ChestLid.transform.eulerAngles.y, ChestLid.transform.eulerAngles.z);
                interactWithChest = false;
                isChestOpen = true;
            }
        }
        else if (isChestOpen)
        {
            ChestLid.transform.Rotate(Mathf.LerpAngle(0.0f, 60.0f, Time.deltaTime), 0, 0);
            if (ChestLid.transform.eulerAngles.x >= 0.0f && ChestLid.transform.eulerAngles.x <= 300.0f)
            {
                ChestLid.transform.eulerAngles = new Vector3(0.0f, ChestLid.transform.eulerAngles.y, ChestLid.transform.eulerAngles.z);
                interactWithChest = false;
                isChestOpen = false;
            }
        }
    }
}
