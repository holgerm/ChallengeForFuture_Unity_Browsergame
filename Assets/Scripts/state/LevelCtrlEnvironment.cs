using System.Linq;
using UnityEngine;

public class LevelCtrlEnvironment : MonoBehaviour
{
    public bool isGood = false;

    public GameObject[] goodEnvironmentObjects;
    public GameObject[] badEnvironmentObjects;

    void Start()
    {
        SetStateOnAllEnvironmentalObjects();
    }

    private void OnValidate()
    {
        CheckArrays();
        SetStateOnAllEnvironmentalObjects();
    }

    public void SetState(bool newGood)
    {
        isGood = newGood;
        SetStateOnAllEnvironmentalObjects();
    }

    private void CheckArrays()
    {
        goodEnvironmentObjects = goodEnvironmentObjects.Distinct().ToArray();
        badEnvironmentObjects = badEnvironmentObjects.Distinct().ToArray();
    }

    private void SetStateOnAllEnvironmentalObjects()
    {
        if (null != goodEnvironmentObjects)
            foreach (var go in goodEnvironmentObjects)
            {
                go.SetActive(isGood);
            }

        if (null != badEnvironmentObjects)
            foreach (var go in badEnvironmentObjects)
            {
                go.SetActive(!isGood);
            }
    }
}