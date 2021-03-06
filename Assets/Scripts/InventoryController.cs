﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour {
    
    public List<GameObject> theInventory;
    public int currentItem;
    public GameObject currentItemImage;
    public GameObject inventoryUI;
	public GameObject inventoryHL;

	public List<GameObject> inventoryUIitems;



    void Start() {
    }

    void Update () {
		
        ActvateItem();
        ChangeItem();

		if (currentItem != 0) {
			inventoryHL.GetComponent<Image> ().enabled = true;
			inventoryHL.transform.position = inventoryUIitems [currentItem].transform.position;
		} else if (inventoryUIitems.Count > 0) {
			inventoryHL.GetComponent<Image> ().enabled = true;
			inventoryHL.transform.position = inventoryUIitems [0].transform.position;
		} else {
			inventoryHL.GetComponent<Image> ().enabled = false;
		}


    }
    public void AddItem(GameObject newItem)
    {

        theInventory.Add(newItem);
		GameObject tempItem = (GameObject)Instantiate(currentItemImage);
		inventoryUIitems.Add (tempItem);
        tempItem.transform.SetParent(inventoryUI.transform);
        tempItem.GetComponentInChildren<Image>().sprite = newItem.GetComponent<isItem>().itemImage;
        newItem.GetComponent<isItem>().itemsImageGameObject = tempItem;

		AudioPlayer.current.PlaySoundClip ("pickUpItem");
		currentItem = theInventory.Count - 1;
    }




    public void ActvateItem()
    {
        if (Input.GetButtonDown("DropItem") && theInventory.Count > 0 && ItemCursor.current.mouseState.Equals("Empty"))
        {
            Destroy(inventoryUIitems[currentItem].gameObject);
			inventoryUIitems.RemoveAt (currentItem);
			ItemCursor.current.HoveringItem(theInventory [currentItem].GetComponent<isItem> ().gameObject.name, theInventory [currentItem].GetComponent<isItem> ().itemImage, theInventory [currentItem].GetComponent<isItem>().itemInGround, theInventory[currentItem].GetComponent<isItem>().mouseState);

			theInventory.RemoveAt(currentItem);
            if (currentItem > 0)
            {
                currentItem--;
            }
        }
    }

    public void ChangeItem()
    {
		if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
			currentItem++;
            if (currentItem >= theInventory.Count)
            {
				currentItem = 0;
            }

        }if (Input.GetAxis("Mouse ScrollWheel") < 0f)
		{

			currentItem--;

			if (currentItem < 0)
			{
				currentItem = theInventory.Count-1;
			}

		}
		
    }
}
