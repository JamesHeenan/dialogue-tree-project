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
        StartDialogue(dialogue);
    }
    void Update() 
    {
        AssignNextButton();
        AssignNumericalInput();
        DirectionTrigger();
        TextBox.text = outputText;
        ContinueDialogue(dialogue);
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
        if(NextPressed != 0) Debug.Log("NextPressed: " + NextPressed);
    }

    public override void AssignNumericalInput()
    {
        float one = controls.FindAction("one").ReadValue<float>();
        float two = controls.FindAction("two").ReadValue<float>();
        float three = controls.FindAction("three").ReadValue<float>();
        float four = controls.FindAction("four").ReadValue<float>();
        float five = controls.FindAction("five").ReadValue<float>();
        float six = controls.FindAction("six").ReadValue<float>();

        NumericalInput = (int)(one + (two*2) + (three*3) + (four*4) + (five*5) + (six*6));

        if(NumericalInput != 0) Debug.Log("NumericalInput: " + NumericalInput);
    }
}
