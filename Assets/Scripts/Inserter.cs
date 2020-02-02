using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inserter : MonoBehaviour
{
    [SerializeField]
    private Sprite DoorClosed;
    [SerializeField]
    private Sprite DoorOpend;

    private SpriteRenderer RepairBoxRenderer;

    private void Awake()
    {
        RepairBoxRenderer = GetComponent<SpriteRenderer>();
    }

    // trigger
    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerController pc = col.gameObject.GetComponent<PlayerController>();

        // is collided with player
        if (pc != null)
        {
            // Take from world then destroy if possible
            if (GameObject.Find("InventorySystem").GetComponent<InventorySystem>().PlaceItemIntoClock())
            {
                // insert successfull
                Debug.Log("inserted item!");
                GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayRepairSound();
            }
            RepairBoxRenderer.sprite = DoorOpend;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        PlayerController pc = col.gameObject.GetComponent<PlayerController>();
        if (pc != null)
        {
            RepairBoxRenderer.sprite = DoorClosed;
        }
    }
}
