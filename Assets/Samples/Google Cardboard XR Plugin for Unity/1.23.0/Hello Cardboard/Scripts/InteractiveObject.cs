using System.Collections;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] protected InteractiveObject[] requiredObjects;
    protected bool isTryingToInteract;
    protected bool hasInteractedWithObject;
    private float maxGazeDetectionTime = 2f;
    private float elapsedGazeDetectionTime = 0f;

    [SerializeField] protected GameObject InfoText;
    protected AudioSource objectAudio;

    protected virtual void Start()
    {
        objectAudio = GetComponent<AudioSource>();
    }

    protected virtual void Update()
    {
        if (isTryingToInteract && !GameManager.Instance.isGamePaused)
        {
            elapsedGazeDetectionTime += Time.deltaTime;

            if (elapsedGazeDetectionTime >= maxGazeDetectionTime)
            {
                TryInteract();
            }
        }
    }

    protected virtual void TryInteract()
    {
        isTryingToInteract = false;

        if (!HasDoneRequirements())
        {
            StartCoroutine(DisplayInfoText());
        }
        else
        {
            InteractWithObject();
            hasInteractedWithObject = true;
        }
    }

    protected virtual bool HasDoneRequirements()
    {
        if (requiredObjects.Length > 0)
        {
            foreach (var obj in requiredObjects)
            {
                if (!obj.IsConditionCompleted())
                    return false;
            }
        }
        return true;
    }

    public virtual void InteractWithObject()
    {
        objectAudio.PlayOneShot(objectAudio.clip);
    }

    public virtual bool IsConditionCompleted()
    {
        return hasInteractedWithObject;
    }

    private IEnumerator DisplayInfoText()
    {
        if (InfoText == null) yield return null;

        InfoText.SetActive(true);
        yield return new WaitForSeconds(2);
        InfoText.SetActive(false);
    }

    public void OnPointerEnter()
    {
        Gaze(true);
    }

    public void OnPointerExit()
    {
        Gaze(false);
    }

    public void Gaze(bool isGazing)
    {
        if (isGazing)
        {
            isTryingToInteract = true;
        }
        else
        {
            isTryingToInteract = false;
            elapsedGazeDetectionTime = 0f;
        }
    }
}
