using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Dray;
[RequireComponent(typeof(InRoom))]
public class Skeletos : Enemy , IFacingMover
{
    [Header(" Inspector: Skeletos")]
     private int speed = 2; 
     private float timeThinkMin = 1f; 
     private float timeThinkMax = 4f; 

    [Header("Dynamic: Skeletos")]
    [Range(0,4)]
    private int facing =0; 
    private float timeNextDecision=0;
    private InRoom inRm;

    protected override void Awake()
    {
        base.Awake();
        inRm = GetComponent<InRoom>();
    }


    void Update()
    {
        if (Time.time >= timeNextDecision)
        { 
            DecideDirection(); 
        }
        // rigid is inherited from Enemy , which defines it in Enemy.Awake()
        rigid.velocity = directions[facing] * speed;  
    }

    
    void DecideDirection()
    {
        facing = Random.Range(0, 5); 
        timeNextDecision = Time.time + Random.Range(timeThinkMin, timeThinkMax); 
    }


    //-------Implementation of IFacingMover---------------
    public int GetFacing()
    {
        return facing% 4;
    }
    public float GetSpeed()
    {
        return speed;
    }

    public bool moving { get { return (facing <4); } }

    public float gridMult { get { return inRm.gridMult; } }
    public bool isInRoom { get { return inRm.isInRoom; } }

    public Vector2 roomNum
    {
        get { return inRm.roomNum; }
        set { inRm.roomNum = value; }
    }

    public Vector2 posInRoom
    {
        get { return inRm.posInRoom; }
        set { inRm.posInRoom = value; }
    }

    public Vector2 GetGridPosInRoom(float mult = -1)
    {
        return inRm.GetGridPosInRoom(mult);
    }
}
