using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class ItemNPCMatch : MonoBehaviour
{
    private ItemDisplay itemInfo;
    public UIManager diagUI;
    public int NPC_ID;
    private VIDE_Assign dialogue;
    public int loseNode;
    public int winNode;
    public GameObject itemDetected;
    private bool satisfied = false;

    private void Start()
    {
        diagUI = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!satisfied)
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


    void Success()
    {
        dialogue.overrideStartNode = winNode;
        diagUI.Begin(dialogue);
        //Debug.Log(VD.GetNodeCount(false));
        Destroy(itemDetected);
        satisfied = true;

        //Changement d'animation
    }

    void Fail()
    {

        dialogue.overrideStartNode = loseNode;
        diagUI.Begin(dialogue);
        satisfied = false;

        //Debug.Log(VD.GetNodeCount(false));

        // Item Bounce back ?
    }

}
