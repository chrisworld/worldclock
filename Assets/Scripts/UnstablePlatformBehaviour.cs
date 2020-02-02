using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstablePlatformBehaviour : MonoBehaviour
{
    [SerializeField]
    protected float SecondsToBreakdown = 2;

    [SerializeField]
    protected float SecondsToVanisch = 2;

    [SerializeField]
    protected float SecondsToRestore = 10;

    private Rigidbody2D physics;
    private Collider2D  collidor;

    [SerializeField]
    private float SecondsLeftToBreakdown;
    [SerializeField]
    private float SecondsLeftToVanisch;
    [SerializeField]
    private float SecondsLeftToRestore;
    [SerializeField]
    private bool PlayerStepTriggered;
    [SerializeField]
    private Vector3 OriginalPosition;

    private void Awake()
    {
        physics = GetComponent<Rigidbody2D>();
        collidor = GetComponent<Collider2D>();
        SecondsLeftToBreakdown = SecondsToBreakdown;
        SecondsLeftToVanisch = SecondsToVanisch;
        SecondsLeftToRestore = SecondsToRestore;
        OriginalPosition = transform.position;
        PlayerStepTriggered = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        PlayerController pc = col.gameObject.GetComponent<PlayerController>();
        if(pc != null)
            PlayerStepTriggered = true;
    }

    private void FixedUpdate()
    {
        if (PlayerStepTriggered)
        {
            if (SecondsLeftToBreakdown > 0)
            {
                SecondsLeftToBreakdown -= Time.fixedDeltaTime;
            }
            else
            {
                physics.bodyType = RigidbodyType2D.Dynamic;
                if (SecondsLeftToVanisch > 0)
                {
                    SecondsLeftToVanisch -= Time.fixedDeltaTime;
                    foreach (var sprite in GetComponentsInChildren<SpriteRenderer>())
                    {
                        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 
                            SecondsLeftToVanisch/SecondsToVanisch);
                    }
                }
                else
                {
                    physics.bodyType = RigidbodyType2D.Static;
                    if (SecondsLeftToRestore > 0)
                    {
                        SecondsLeftToRestore -= Time.fixedDeltaTime;
                        collidor.isTrigger = true;
                        transform.position = OriginalPosition;
                        foreach (var sprite in GetComponentsInChildren<SpriteRenderer>())
                        {
                            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b,
                                1.0f-(SecondsLeftToRestore / SecondsToRestore));
                        }
                    }
                    else
                    {
                        PlayerStepTriggered = false;
                        SecondsLeftToBreakdown = SecondsToBreakdown;
                        SecondsLeftToVanisch = SecondsToVanisch;
                        SecondsLeftToRestore = SecondsToRestore;
                        collidor.isTrigger = false;
                        foreach (var sprite in GetComponentsInChildren<SpriteRenderer>())
                        {
                            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1.0f);
                        }
                    }
                }

            }
        }
    }
}
