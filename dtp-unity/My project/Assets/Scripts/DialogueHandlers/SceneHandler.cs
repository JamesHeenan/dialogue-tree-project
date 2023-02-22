using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    public Color notTalking;
    public GameObject locationObject;
    public GameObject overlay;
    public GameObject characterObjectPrefab;
    List<GameObject> characterObjects;
    public TextMeshProUGUI textUI;
    public string log;
    public GameObject textObject;
    public Vector2[] basePosAndSize;
    public Vector2[] expandedPosAndSize;
    public Scrollbar scrollbar;
    public SceneDirection loadedSceneDirection;
    public void RefreshScene(SceneDirection direction)
    {
        loadedSceneDirection = (SceneDirection)direction;
        GameObject tmp = null;
        locationObject.GetComponent<SpriteRenderer>().sprite = loadedSceneDirection.Location.Sprite;
        locationObject.GetComponent<AudioSource>().clip = loadedSceneDirection.GetTalking().Character.SoundFont;
        for (int i = 0; i < loadedSceneDirection.Characters.Length; i++)
        {
            if(i+1 > characterObjects.Count)
            {
                tmp = Instantiate(characterObjectPrefab);
                characterObjects.Add(tmp);
            }
            if(!loadedSceneDirection.Characters[i].Talking) 
            {
                characterObjects[i].GetComponent<SpriteRenderer>().color = notTalking;
                characterObjects[i].GetComponent<Transform>().localScale = new Vector3(0.9f,0.9f,0);
            }
            else 
            {
                characterObjects[i].GetComponent<SpriteRenderer>().color = Color.white;
                characterObjects[i].GetComponent<Transform>().localScale = new Vector3(1f,1f,0);
            }
            if(loadedSceneDirection.Characters[i].Flipped) characterObjects[i].GetComponent<SpriteRenderer>().flipX = true;
            else characterObjects[i].GetComponent<SpriteRenderer>().flipX = false;
            characterObjects[i].GetComponent<SpriteRenderer>().sprite = loadedSceneDirection.Characters[i].GetEmotion();
            characterObjects[i].GetComponent<Transform>().position = loadedSceneDirection.Characters[i].Position;

        }
        for (int i = loadedSceneDirection.Characters.Length; i < characterObjects.Count; i++)
        {
            tmp = characterObjects[i];
            characterObjects.Remove(tmp);
            Destroy(tmp);
        }

    }
    public void RefreshText(string input)
    {
        textUI.text = input;
        MoveScrollbarTo(0);
    }
    public void MoveScrollbarTo(int input)
    {
        scrollbar.interactable = false;
        scrollbar.value = input;
        scrollbar.interactable = true;
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
    public void AppendToLog(string input)
    {
        log += input;
    }
    public void SetOverlayAlpha(float alphaLevel)
    {
        Color tmp = overlay.GetComponent<SpriteRenderer>().color;
        tmp.a = alphaLevel;
        overlay.GetComponent<SpriteRenderer>().color = tmp;
        if(alphaLevel == 0) overlay.SetActive(false);
        else overlay.SetActive(true);
    }
    public IEnumerator FadeToBlack(float duration, float DurationDelay, Action action)
    {
        Color tmp = overlay.GetComponent<SpriteRenderer>().color;
        tmp.a = 0; //set aplha to 0
        overlay.GetComponent<SpriteRenderer>().color = tmp;
        overlay.SetActive(true);
        for (int i = 0; i <= duration*100; i++)
        {
            //increase alpha by 1/duration*10
            tmp.a += 1/(duration*100f);
            overlay.GetComponent<SpriteRenderer>().color = tmp;
            yield return new WaitForSecondsRealtime(0.01f);   
        }
        yield return new WaitForSecondsRealtime(DurationDelay);
        action();
    }
    public IEnumerator FadeInToScene(float DurationOfTransition, float DurationDelay, Action action)
    {
        Color tmp = overlay.GetComponent<SpriteRenderer>().color;
        tmp.a = 1; //set aplha to 1
        overlay.GetComponent<SpriteRenderer>().color = tmp;
        for (int i = 0; i <= DurationOfTransition*100; i++)
        {
            //increase alpha by 1/duration*10
            tmp.a -= 1/(DurationOfTransition*100f);
            overlay.GetComponent<SpriteRenderer>().color = tmp;
            yield return new WaitForSecondsRealtime(0.01f);   
        }
        overlay.SetActive(false);
        yield return new WaitForSecondsRealtime(DurationDelay);
        action();       
    }
    public void ToggleTextBoxSize(bool input)
    {
        if(input)
        {
            textObject.GetComponent<RectTransform>().anchoredPosition = expandedPosAndSize[0];
            textObject.GetComponent<RectTransform>().sizeDelta = expandedPosAndSize[1];
        }
        else
        {
            textObject.GetComponent<RectTransform>().anchoredPosition = basePosAndSize[0];
            textObject.GetComponent<RectTransform>().sizeDelta = basePosAndSize[1];
        }

    }
}
