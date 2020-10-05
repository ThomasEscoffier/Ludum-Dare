using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VIDE_Data;
using UnityEngine.SceneManagement;

public class VIDEPlayer : MonoBehaviour
{
    //This script handles player movement and interaction with other NPC game objects

    public string playerName = "VIDE User";

    //Reference to our diagUI script for quick access
    public UIManager diagUI;
    public AudioManager audioManager;
    private string nameNPC;

    public int satisfiedNPCCount = 0;
    public int sceneCount;
    public int NPCToWin;
    public VIDE_Assign winDialogue;


    //Stored current VA when inside a trigger
    public VIDE_Assign inTrigger;
    private bool intro = true;

    //DEMO variables for item inventory
    //Crazy cap NPC in the demo has items you can collect

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<VIDE_Assign>() != null)
            inTrigger = other.GetComponent<VIDE_Assign>();

        nameNPC = other.gameObject.name;

    }

    void OnTriggerExit()
    {
        inTrigger = null;
        nameNPC = null;
    }

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    void Update()
    {

        if (intro)
        {
            intro = false;
            StartCoroutine(audioManager.Crossfade("GOD")); 
            nameNPC = "GOD";
            diagUI.Interact(inTrigger);
        }
        //Only allow player to move and turn if there are no dialogs loaded
        if (!VD.isActive)
        {
            //transform.Rotate(0, Input.GetAxis("Mouse X") * 5, 0);
            //float move = Input.GetAxisRaw("Vertical");
            //transform.position += transform.forward * 7 * move * Time.deltaTime;
            //if(blue != null)
            //    blue.SetFloat("speed", move);

            if (nameNPC != null && audioManager.musicPlayingName == nameNPC)
            {
                StartCoroutine(audioManager.Crossfade("Theme"));
            }
        }

        //Interact with NPCs when pressing E
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }

        //Hide/Show cursor
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.visible = !Cursor.visible;
            if (Cursor.visible)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }

    }

    public void WinCheck()
    {
        if (satisfiedNPCCount == NPCToWin)
        {
            Win();
        }
    }

    public void GodEnd()
    {
        inTrigger = null;

    }

    void Win()
    {
        VD.EndDialogue();
        audioManager.Play("Success");
        winDialogue.overrideStartNode = 8;
        diagUI.Begin(winDialogue);
    }

    public void Credits()
    {
        Debug.Log("BOOM TRANSITION END GAME");
        StartCoroutine(Ending());
       
    }

    IEnumerator Ending()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneCount);
    }

    //Casts a ray to see if we hit an NPC and, if so, we interact
    void TryInteract()
    {
        /* Prioritize triggers */

        if (inTrigger)
        {
            diagUI.Interact(inTrigger);
            

            if (audioManager.musicPlayingName != null && audioManager.musicPlayingName != nameNPC)
            {
                StartCoroutine(audioManager.Crossfade(nameNPC));
            }
            else if (audioManager.musicPlayingName == null)
            {
                audioManager.Play(nameNPC);
            }

            return;
        }

        /* If we are not in a trigger, try with raycasts */

        RaycastHit rHit;

        if (Physics.Raycast(transform.position, transform.forward, out rHit, 2))
        {
            //Lets grab the NPC's VIDE_Assign script, if there's any
            VIDE_Assign assigned;
            if (rHit.collider.GetComponent<VIDE_Assign>() != null)
                assigned = rHit.collider.GetComponent<VIDE_Assign>();
            else return;

            if (assigned.alias == "QuestUI")
            {

            }
            else
            {
                diagUI.Interact(assigned); //Begins interaction
            }

            if (audioManager.musicPlayingName != null && audioManager.musicPlayingName != nameNPC && nameNPC != null)
            {
                StartCoroutine(audioManager.Crossfade(nameNPC));
            }
            else if (audioManager.musicPlayingName != nameNPC && nameNPC != null)
            {
                audioManager.Play(nameNPC);
            }
        }
    }
}
