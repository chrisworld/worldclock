using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewinder : MonoBehaviour
{
    [SerializeField] private bool PlayerInRewindTrigger;

    protected Animator RewindAnimator;

    private void Awake()
    {
        RewindAnimator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerController pc = col.gameObject.GetComponent<PlayerController>();
        if (pc != null)
        {
            PlayerInRewindTrigger = true;
            RewindAnimator.SetBool("Is_Rewinded", true);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        PlayerController pc = col.gameObject.GetComponent<PlayerController>();
        if (pc != null)
        {
            PlayerInRewindTrigger = false;
            RewindAnimator.SetBool("Is_Rewinded", false);
        }
    }

    private void FixedUpdate()
    {
        if (PlayerInRewindTrigger)
        {
            GameObject.Find("WorldClock").GetComponent<WorldClock>().Rewind(Time.fixedDeltaTime);
        }
    }
}
