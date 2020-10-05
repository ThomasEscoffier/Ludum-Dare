using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpSystem : MonoBehaviour
{

    private bool canPickUp;
    private string itemSelected;
    private GameObject itemHolder;
    private string itemInPossession;
    private VIDEPlayer playerAudio;

    public GameObject PickUpScreen;
    public Text PickUpText;
    private ItemDisplay itemInfos;
    public GameObject patate;

    public GameObject spawner;
    public GameObject[] itemsInHand;
    public GameObject[] itemPrefabs;
    

    private void Start()
    {
        playerAudio = gameObject.GetComponent<VIDEPlayer>();
        PickUpScreen.SetActive(false);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("item"))
        {
            itemInfos = other.gameObject.GetComponent<ItemDisplay>();
            PickUpText.text = "Press F to pick up a <color=yellow>" + itemInfos.item.name + "</color>!";
            PickUpScreen.SetActive(true);
            canPickUp = true;
            itemSelected = other.gameObject.name;

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("ItemHolder"))
        {
            itemHolder = other.gameObject;
        }
    }

    void OnTriggerExit()
    {
        PickUpScreen.SetActive(false);
        canPickUp = false;
        itemSelected = null;
        itemHolder = null;
    }

    // Update is called once per frame
    void Update()
    {

        #region Debug
        /*
        if(itemSelected != null)
        {

            Debug.Log(itemSelected.name);
            Debug.Log(itemHolder.name);
        }
        else
        {
            Debug.Log("itemSelected is null");
        }

        */

        #endregion

        if (canPickUp && itemInPossession == null && itemHolder != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                PickUpItem();
                canPickUp = false;
            }

        } else if(itemInPossession != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                DropItem();
            }
        }

    }

    void PickUpItem()
    {
        playerAudio.audioManager.Play("PickUp");
        PickUpScreen.SetActive(false);
        itemInPossession = itemHolder.name;
        Destroy(itemHolder);

        for (int i = 0; i < itemsInHand.Length; i++)
        {
            if(itemsInHand[i].gameObject.name == itemSelected)
            {
                itemsInHand[i].SetActive(true);
            }
                
        }
        canPickUp = false;
        itemSelected = null;
        itemHolder = null;
    }


    void DropItem()
    {
        playerAudio.audioManager.Play("Toss");
        for (int i = 0; i < itemPrefabs.Length; i++)
        {
            if (itemPrefabs[i].gameObject.name == itemInPossession)
            {
               GameObject a = Instantiate(itemPrefabs[i],(spawner.transform.position), spawner.transform.rotation);
                a.name = itemInPossession;

            }

        }

        for (int i = 0; i < itemsInHand.Length; i++)
        {
                itemsInHand[i].SetActive(false);
        }

        itemInPossession = null;
        canPickUp = false;
        itemSelected = null;
        itemHolder = null;
    }

    public void SpawnPatate()
    {

        Instantiate(patate, (spawner.transform.position), spawner.transform.rotation);
         
    }


}
