﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;


public class ItemNPCMatch : MonoBehaviour
{
    private ItemDisplay itemInfo;
    private VIDEPlayer player;
    public UIManager diagUI;
    public int NPC_ID;
    private VIDE_Assign dialogue;
    private Animator animatorNPC;

    public int loseNode;
    public int winNode;
    public GameObject itemDetected;
    public bool satisfied = false;


    public bool itemDelivered = false;

    private void Start()
    {

        diagUI = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<VIDEPlayer>();
        animatorNPC = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (!satisfied)
        { 
            if (other.gameObject.CompareTag("item"))
            {
                itemInfo = other.gameObject.GetComponent<ItemDisplay>();
                dialogue = gameObject.GetComponent<VIDE_Assign>();
                itemDetected = other.gameObject.transform.parent.gameObject;
               // Debug.Log(itemInfo.item.characterAttached+ " "+ " "  + NPC_ID + " " + itemDetected);

                if (itemInfo.item.characterAttached == NPC_ID && itemDetected != null)
                {
                    Success();
                }else
                {
                    Fail();
                }
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        itemDelivered = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (itemDelivered && VD.isActive)
                diagUI.CallNext();
        }

    }

    void Success()
    {
        player.audioManager.Play("Success");

        if(animatorNPC != null)
            animatorNPC.SetBool("Cured", true);

        dialogue.overrideStartNode = winNode;
        diagUI.Begin(dialogue);
        //Debug.Log(VD.GetNodeCount(false));
        Destroy(itemDetected);
        satisfied = true;
        player.satisfiedNPCCount++;
        itemDelivered = true;
        //Changement d'animation
    }

    void Fail()
    {
        player.audioManager.Play("Fail");
        dialogue.overrideStartNode = loseNode;
        diagUI.Begin(dialogue);
        satisfied = false;
        itemDelivered = true;
        //Debug.Log(VD.GetNodeCount(false));

        // Item Bounce back ?
    }
}
