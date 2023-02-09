using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TestDirectionTrigger : DialogueReader
{
    [SerializeField] TextMeshProUGUI TextBox;
    public Dialogue dialogue;
    [SerializeField] GameObject LocationObjectPrefab;
    GameObject Location;
    [SerializeField] GameObject CharacterObjectPrefab;
    List<GameObject> CharacterObjects;
    [SerializeField] Vector2[] CharacterPositions;

    Controls controls;

    void Start()
    {
        controls = new Controls();
        controls.Enable();
        CharacterObjects = new List<GameObject>();
        ContinueDialogue(dialogue);
    }
    void Update() 
    {
        AssignNextButton();
        DirectionTrigger();
        TextBox.text = outputText;
        if(NextDirectionReady) ContinueDialogue(dialogue);
    }

    public override void InstantiateScene()
    {
        Location = Instantiate(LocationObjectPrefab);
        for (int i = 0; i < 4; i++)
        {
            GameObject tmp = Instantiate(CharacterObjectPrefab);
            tmp.name = i.ToString();
            CharacterObjects.Add(tmp);
        }       
    }
    public override void RefreshScene()
    {
        Location.GetComponent<SpriteRenderer>().sprite = location.Sprite;
        for (int i = 0; i < 4; i++)
        {
            if(i < charactersInScene.Length)
            {
                if(charactersInScene[i].Talking == true)
                {
                    Location.GetComponent<AudioSource>().clip = charactersInScene[i].Character.SoundFont;
                    outputName = charactersInScene[i].Name;
                    baseTextSpeed = charactersInScene[i].Character.TalkSpeed;
                    textSpeed = baseTextSpeed;
                }
                CharacterObjects[i].GetComponent<Transform>().position = CharacterPositions[i];
                CharacterObjects[i].SetActive(true);
                CharacterObjects[i].GetComponent<SpriteRenderer>().sprite = charactersInScene[i].GetEmotion();
            }
            else CharacterObjects[i].SetActive(false);
        }
    }

    public override void PlaySoundFont()
    {
        Location.GetComponent<AudioSource>().Play();
    }

    public override void AssignNextButton()
    {
        NextPressed = controls.FindAction("Next").ReadValue<float>();
    }

    public override void AssignNumericalInput()
    {
        throw new System.NotImplementedException();
    }
}
