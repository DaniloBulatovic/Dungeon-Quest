using UnityEngine;

public class ScaleController : InteractiveObject
{
    private float itemStartHeight;
    private float itemFloatSpeed = 2.0f;
    private float itemFloatHeight = 1.2f;
    private bool floatItem;

    private bool scaleBalanced;

    [SerializeField] private GameObject FullBeaker;
    [SerializeField] private GameObject Key;

    public override void InteractWithObject()
    {
        if (scaleBalanced) return;

        objectAudio.PlayOneShot(objectAudio.clip);
        FullBeaker.SetActive(true);
        floatItem = true;
        scaleBalanced = true;
    }


    private void FixedUpdate()
    {
        if (floatItem)
        {
            FloatItem();
        }
    }

    private void FloatItem()
    {
        Key.SetActive(true);
        Key.transform.Translate(itemFloatSpeed * Time.deltaTime * Vector3.up, Space.World);
        if (Key.transform.position.y - itemStartHeight > itemFloatHeight)
        {
            Key.transform.position = new Vector3(Key.transform.position.x, itemStartHeight + itemFloatHeight, Key.transform.position.z);
            floatItem = false;
        }
    }
}
