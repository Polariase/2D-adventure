using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public bool onGround;
    public bool onWall;
    public float checkRadius = 0.2f;
    public int jumpBonus = 0;
    public float pref;
    public Vector2 offset;
    public Vector2 woffset;
    public LayerMask groundLayer;
    void Update()
    {
        Check();
    }

    private void Start()
    {
        pref=transform.localScale.x;
    }
    void Check()
    {
        if (pref * transform.localScale.x < 0)
        {
            woffset.x = -woffset.x;
            pref = -pref;
        }
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + offset, checkRadius, groundLayer);
        if (onGround)
        {
            jumpBonus = 1;
        }
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + woffset, checkRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + offset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + woffset, checkRadius);
    }
}
