using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public GameObject modelOne;
    public GameObject modelTwo;
    public GameObject modelThree;

    public void SetCharacter(int index)
    {
        modelOne.SetActive(index == 0);
        modelTwo.SetActive(index == 1);
        modelThree.SetActive(index == 2);
    }

    private void OnEnable()
    {
        SetCharacter(0);
    }
}
