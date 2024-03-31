using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private bool isTryingToInteract;
    [SerializeField] private float maxGazeDetectionTime = 2f;
    private float elapsedGazeDetectionTime = 0f;

    void Update()
    {
        if (isTryingToInteract)
        {
            elapsedGazeDetectionTime += Time.deltaTime;

            if (elapsedGazeDetectionTime >= maxGazeDetectionTime)
            {
                isTryingToInteract = false;

                if (TryGetComponent(out Button button))
                {
                    button.onClick.Invoke();
                }
            }
        }
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
