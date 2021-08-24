using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChoosing : MonoBehaviour
{
    [SerializeField] private Character[] characters;
    private int selectedCharacterId = 0;

    public void nextCharacter()
    {
        characters[selectedCharacterId].setActive(false);

        selectedCharacterId = (selectedCharacterId + 1) % characters.Length;

        characters[selectedCharacterId].setActive(true);
    }

    public void previousCharacter()
    {
        characters[selectedCharacterId].setActive(false);

        selectedCharacterId--;

        if(selectedCharacterId < 0)
        {
            selectedCharacterId += characters.Length;
        }

        characters[selectedCharacterId].setActive(true);
    }
}
