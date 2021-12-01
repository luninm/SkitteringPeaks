using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// For text
public class TextDialogueAnimator : MonoBehaviour
{
    public string readFromThisFile;
    public GameObject choicePrompt;
    public Color TextColor;
    public Color NameTagColor;
    public Emotion activeEmotion;

    DialogueList dialogueList;
    GameObject nameTag;
    GameObject dialogueBox;
    TMP_Text textMesh;
    Mesh mesh;
    Vector3[] vertices;
    Color[] colors;
    Coroutine CurrentText;

    bool isWriting = false;
    bool display = false;
    bool selected = false;
    int currentChoice = 0;
    int numberOfChoices;

    public bool inChoicePrompt = false;
    public string[] PhraseList;

    public AudioSource voice;

    // Start is called before the first frame update
    void OnEnable()
    {
        voice = GetComponent<AudioSource>();

        activeEmotion = Emotion.normal;

        nameTag = GameObject.Find("NameTag");
        dialogueBox = GameObject.Find("DialogueBox");
        nameTag.SetActive(false);
        dialogueBox.SetActive(false);

        dialogueList = DIALOGUE_UTILITY.ReadJson(readFromThisFile);

        textMesh = GetComponent<TMP_Text>();
        MakeTextTransparent();
        textMesh.ForceMeshUpdate();

        StopAllCoroutines();
        StartCoroutine(StartDialogue());

    }

    private void OnDisable()
    {
        MakeTextTransparent();
        inChoicePrompt = false;

        // Unhighlight Options
        for (var j = 0; j < 3; j++)
        {
            choicePrompt.transform.GetChild(j).gameObject.GetComponent<TMP_Text>().fontStyle = FontStyles.Normal;
            choicePrompt.transform.GetChild(j).gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        textMesh.ForceMeshUpdate();
        WobbleEffect();

        if (inChoicePrompt)
            UpdateChoiceSelection();

        mesh.colors = colors;
        mesh.vertices = vertices;

        textMesh.canvasRenderer.SetMesh(mesh);
    }


    // Start dialogue tree
    IEnumerator StartDialogue()
    {
        // Wait for the camera to zoom in
        yield return new WaitForSeconds(1f);

        // Enable nametag and box
        nameTag.SetActive(true);
        dialogueBox.SetActive(true);

        // Travese dialogue tree
        int i = 0;

        while (i != -1)
        {
            // Choice prompt
            if (dialogueList.list[i].name == "choice")
            {
                currentChoice = 0;
                choicePrompt.SetActive(true);
                HandleChoicePrompt(i);

                yield return new WaitForSeconds(.5f);
                yield return new WaitUntil(ReadyForNextAfterChoice);
                i = dialogueList.list[i].next + currentChoice;
            }
            else // Regular dialogue
            {
                inChoicePrompt = false;
                HandleTextEffects(i);
                HandlePortrait(i);

                yield return new WaitUntil(ReadyForNext);
                i = dialogueList.list[i].next;
            }
        }

        StopWriting();
        // Clear the camera targets
        transform.parent.parent.GetComponent<DialogueManager>().ResetTargets();
        // Dialogue is done
        transform.parent.gameObject.SetActive(false);
        yield break;
    }

    // Dialogue choice stuff
    void HandleChoicePrompt(int i)
    {
        inChoicePrompt = true;

        // Get the options and split them up
        string msg = dialogueList.list[i].message;
        string[] options = msg.Split(',');
        numberOfChoices = options.Length;

        // Disable unused options
        if (numberOfChoices < 3)
            for (var j = numberOfChoices; j < 3; j++)
                choicePrompt.transform.GetChild(j).gameObject.SetActive(false);

        // ** smol bug fix (the selection overlay is offset on a single choice) **
        if(numberOfChoices == 1)
            choicePrompt.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        else
            choicePrompt.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);


        // Display the options
        for (var j = 0; j < numberOfChoices; j++)
        {
            choicePrompt.transform.GetChild(j).gameObject.GetComponent<TMP_Text>().text = options[j];
            choicePrompt.transform.GetChild(j).gameObject.SetActive(true);
        }

        // bold the selection
        choicePrompt.transform.GetChild(currentChoice).gameObject.GetComponent<TMP_Text>().fontStyle = FontStyles.Bold;


        // set first to selected
        choicePrompt.transform.GetChild(currentChoice).GetChild(0).gameObject.SetActive(true);

        // unselect others
        for (var j = 1; j < numberOfChoices; j++)
            choicePrompt.transform.GetChild(j).GetChild(0).gameObject.SetActive(false);

    }

    void UpdateChoiceSelection()
    {
        bool up = Input.GetAxisRaw("Vertical") < -.5f;
        bool down = Input.GetAxisRaw("Vertical") > .5f;

        if ((up || down) && !selected)
        {
            selected = true;
            // Move up or down the list
            if (up)
                currentChoice++;
            else
                currentChoice--;

            // Keep in range 0 to 2
            if (currentChoice < 0)
                currentChoice = numberOfChoices - 1;
            else if (currentChoice > numberOfChoices - 1)
                currentChoice = 0;

            // highlight the selection
            choicePrompt.transform.GetChild(currentChoice).gameObject.GetComponent<TMP_Text>().fontStyle = FontStyles.Bold;

            // Unhighlight others
            for (var j = 0; j < numberOfChoices; j++)
                if (j != currentChoice)
                    choicePrompt.transform.GetChild(j).gameObject.GetComponent<TMP_Text>().fontStyle = FontStyles.Normal;

            // set first to selected
            choicePrompt.transform.GetChild(currentChoice).GetChild(0).gameObject.SetActive(true);

            // unselect others
            for (var j = 0; j < numberOfChoices; j++)
                if (j != currentChoice)
                    choicePrompt.transform.GetChild(j).GetChild(0).gameObject.SetActive(false);

        }
        if (Input.GetAxisRaw("Vertical") == 0f)
            selected = false;
    }

    // Regular dialogue stuff
    void HandleTextEffects(int i)
    {
        StopWriting();
        textMesh.text = "&";
        textMesh.text += dialogueList.list[i].message;
        BeginWriting();
    }

    void HandlePortrait(int i)
    {
        Emotion emotion = dialogueList.list[i].emotion;
        string name = dialogueList.list[i].name;
        TMP_Text nameText = nameTag.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();

        nameText.text = name;

        activeEmotion = emotion;
    }



    // Typewriter effect
    IEnumerator TypeWriter()
    {
        isWriting = true;

        mesh = textMesh.mesh;
        colors = mesh.colors;

        MakeTextTransparent();

        // Set color of each letter 1 by 1
        for (int i = 0; i < textMesh.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = textMesh.textInfo.characterInfo[i];
            int index = c.vertexIndex;

            if (c.character == ' ' || c.character == '&')
                display = false;
            else
                display = true;

            if (display)
            {
                voice.volume = 1f;
                voice.Play();
                yield return new WaitForSeconds(.05f);
                for (int j = 0; j < 4; j++)
                    colors[index + j] = TextColor;
            }
            voice.volume = 0f;
            voice.Stop();
        }

        voice.volume = 0f;
        voice.Stop();

        isWriting = false;
        yield break;
    }


    // Wobble effect
    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * .2f), Mathf.Cos(time * 2f));
    }

    void WobbleEffect()
    {
        mesh = textMesh.mesh;
        vertices = mesh.vertices;

        for (int i = 0; i < textMesh.textInfo.characterCount; i++)
        {

            TMP_CharacterInfo c = textMesh.textInfo.characterInfo[i];
            int index = c.vertexIndex;


            Vector3 offset = Wobble(Time.time + i);
            for (int j = 0; j < 4; j++)
            {
                vertices[index + j] += offset;
            }

        }
    }


    // Utility

    void StopWriting()
    {
        if (CurrentText != null)
            StopCoroutine(CurrentText);
        textMesh.text = "";
        isWriting = false;
    }


    void BeginWriting()
    {
        MakeTextTransparent();
        textMesh.ForceMeshUpdate();
        CurrentText = StartCoroutine(TypeWriter());
    }


    void MakeTextTransparent()
    {
        // SET ALL TEXT TO TRANSPARENT
        for (int i = 0; i < textMesh.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = textMesh.textInfo.characterInfo[i];
            int index = c.vertexIndex;

            for (int j = 0; j < 4; j++)
            {
                colors[index + j] = Color.clear;
            }

        }
    }


    // Waiting for input

    bool ReadyForNext()
    {
        if (!isWriting && !inChoicePrompt && NextKeyPressed())
        {
            return true;
        }
        return false;
    }

    bool ReadyForNextAfterChoice()
    {
        if (!isWriting && inChoicePrompt && NextKeyPressed())
        {
            choicePrompt.SetActive(false);
            inChoicePrompt = false;
            return true;
        }
        return false;
    }

    // Input
    // FIXME could add more input here later
    bool NextKeyPressed()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter)
            || Input.GetKeyDown(KeyCode.Return))
        {
            return true;
        }
        return false;
    }
}
