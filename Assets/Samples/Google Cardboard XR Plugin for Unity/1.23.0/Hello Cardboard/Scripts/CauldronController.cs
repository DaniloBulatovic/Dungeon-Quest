using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class CauldronController : InteractiveObject
{
    private bool bluePotionAdded;
    private bool greenPotionAdded;
    private bool redPotionAdded;

    public bool isCauldronFull;
    public bool isCauldronEmptied;

    private float itemStartHeight;
    private float itemFloatSpeed = 2.0f;
    private float itemFloatHeight = 1.3f;
    private bool floatItem;

    [SerializeField] private Mesh EmptyCauldronMesh;
    [SerializeField] private Mesh FullCauldronMesh;
    [SerializeField] private Material metalPolished;
    [SerializeField] private Material metalRusty;
    [SerializeField] private Material potionMaterial;
    [SerializeField] private GameObject FullBeaker;

    private AudioSource cauldronAudio;
    [SerializeField] private AudioClip placePotion;
    [SerializeField] private AudioClip successSound;

    private bool playSuccessAudio;

    protected override void Start()
    {
        cauldronAudio = GetComponent<AudioSource>();
        playSuccessAudio = true;
    }

    protected override void TryInteract()
    {
        isTryingToInteract = false;

        if (requiredObjects.First(p => p.CompareTag("BluePotion")).IsConditionCompleted() && !bluePotionAdded)
        {
            bluePotionAdded = true;
            StartCoroutine(DisplayInfoText("Added blue potion", Color.blue));
            PlayPotionSound();
        }
        else if (requiredObjects.First(p => p.CompareTag("GreenPotion")).IsConditionCompleted() && !greenPotionAdded)
        {
            greenPotionAdded = true;
            StartCoroutine(DisplayInfoText("Added green potion", Color.green));
            PlayPotionSound();
        }
        else if (requiredObjects.First(p => p.CompareTag("RedPotion")).IsConditionCompleted() && !redPotionAdded)
        {
            redPotionAdded = true;
            StartCoroutine(DisplayInfoText("Added red potion", Color.red));
            PlayPotionSound();
        }
        else if (requiredObjects.First(p => p.CompareTag("EmptyBeaker")).IsConditionCompleted() && isCauldronFull)
        {
            isCauldronFull = false;
            UpdateMeshToEmpty();
            PlayPotionSound();
            floatItem = true;
            hasInteractedWithObject = true;
        }
        else
        {
            StartCoroutine(DisplayInfoText("Missing item", Color.red));
        }
    }

    private IEnumerator DisplayInfoText(string text, Color color)
    {
        InfoText.SetActive(true);
        InfoText.GetComponent<TMP_Text>().text = text;
        InfoText.GetComponent<TMP_Text>().color = color;
        yield return new WaitForSeconds(2);
        InfoText.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (bluePotionAdded && greenPotionAdded && redPotionAdded)
        {
            isCauldronFull = true;
            if (!cauldronAudio.isPlaying && playSuccessAudio)
            {
                cauldronAudio.PlayOneShot(successSound);
                playSuccessAudio = false;
            }
            UpdateMeshToFull();
        }
        if (floatItem)
        {
            FloatItem();
        }
    }

    private void UpdateMeshToFull()
    {
        GetComponent<MeshFilter>().mesh = FullCauldronMesh;
        Material[] meshMaterials = new Material[3];
        meshMaterials[0] = metalRusty;
        meshMaterials[1] = metalPolished;
        meshMaterials[2] = potionMaterial;
        GetComponent<MeshRenderer>().materials = meshMaterials;
        transform.position = new Vector3(transform.position.x, 1.03f, transform.position.z);
    }

    private void UpdateMeshToEmpty()
    {
        GetComponent<MeshFilter>().mesh = EmptyCauldronMesh;
        Material[] meshMaterials = new Material[2];
        meshMaterials[0] = metalPolished;
        meshMaterials[1] = metalRusty;
        GetComponent<MeshRenderer>().materials = meshMaterials;
        transform.position = new Vector3(transform.position.x, 0.939f, transform.position.z);
    }

    private void PlayPotionSound()
    {
        if (!cauldronAudio.isPlaying)
        {
            cauldronAudio.PlayOneShot(placePotion);
        }
    }

    private void FloatItem()
    {
        FullBeaker.SetActive(true);
        FullBeaker.transform.Translate(itemFloatSpeed * Time.deltaTime * Vector3.up, Space.World);
        if (FullBeaker.transform.position.y - itemStartHeight > itemFloatHeight)
        {
            FullBeaker.transform.position = new Vector3(FullBeaker.transform.position.x, itemStartHeight + itemFloatHeight, FullBeaker.transform.position.z);
            floatItem = false;
        }
    }
}
