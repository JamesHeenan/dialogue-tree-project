using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneHandler : MonoBehaviour
{
    public GameObject locationObject;
    public GameObject characterObjectPrefab;
    List<GameObject> characterObjects;
    public TextMeshProUGUI textObject;
    public void RefreshScene(DirectionBase direction)
    {
        direction.SetTalkingPos();
        GameObject tmp = null;
        locationObject.GetComponent<SpriteRenderer>().sprite = direction.Location.Sprite;
        locationObject.GetComponent<AudioSource>().clip = direction.Characters[direction.TalkingPos].Character.SoundFont;
        for (int i = 0; i < direction.Characters.Length; i++)
        {
            if(i+1 > characterObjects.Count)
                tmp = Instantiate(characterObjectPrefab);
            tmp.GetComponent<SpriteRenderer>().sprite = direction.Characters[i].GetEmotion();
            tmp.GetComponent<Transform>().position = direction.Characters[i].Position;
            characterObjects.Add(tmp);
        }
        for (int i = direction.Characters.Length; i < characterObjects.Count; i++)
        {
            tmp = characterObjects[i];
            characterObjects.Remove(tmp);
            Destroy(tmp);
        }

    }
    public void RefreshText(string input)
    {
        textObject.text = input;
    }
    public void ResetScene()
    {
        locationObject.GetComponent<SpriteRenderer>().sprite = null;
        locationObject.GetComponent<AudioSource>().clip = null;
        characterObjects = new List<GameObject>();
    }
    public void PlaySoundFont()
    {
        locationObject.GetComponent<AudioSource>().Play();
    }
}
