using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWidget : MonoBehaviour
{
    [SerializeField] private GameObject[] slots;
    [SerializeField] private GameObject inventoryOverflowTip;

    private int occupiedSlots = 0;
    
    public bool UpdateSlotContent(ItemContent content)
    {
        if (occupiedSlots < slots.Length)
        {
            var image = slots[occupiedSlots].transform.GetChild(0).transform.GetComponent<Image>();
            image.sprite = content.sprite;
            image.enabled = true;
            slots[occupiedSlots].transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>().SetText(content.ID);

            occupiedSlots++;
            return true;
        }
        else
        {
            StartCoroutine(ShowInventoryOverflowTip());
            return false;
        }
    }

    private IEnumerator ShowInventoryOverflowTip()
    {
        inventoryOverflowTip.SetActive(true);
        yield return new WaitForSeconds(3f);
        inventoryOverflowTip.SetActive(false);
    }
}
