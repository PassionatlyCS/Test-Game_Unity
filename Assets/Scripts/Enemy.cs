using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Todo List for AI
    public enum State
    {
        LOOKFOR,
        GOTO,
        ATTACK,
    }

    //Ingrediants for the Todo List
    public State CurState;
    public float Speed = 0.5f;
    public float GotoDistance = 13;
    public float AttackDistance = 4;
    public float AttackTimer = 2;

    public Transform Target;
    public string PlayerTag = "Player";

    private float CurTime;
    private Player PlayerScript;
    // Use the AI
    IEnumerator Start()
    {
        //This is start
        Target = GameObject.FindGameObjectWithTag(PlayerTag).transform;
        CurTime = AttackTimer;


        if(Target != null )
        {
            PlayerScript = Target.GetComponent<Player>();
        }

        while (true)
            //This is our update
        {
            switch(CurState)
            {
                case State.LOOKFOR:
                    LookFor();
                    break;
                case State.GOTO:
                    Goto();
                    break;
                case State.ATTACK:
                    Attack();
                    break;
            }
            yield return 0;
        }
    }

    //Helper functions
    void LookFor ()
    {
        if (Vector3.Distance(Target.position, transform.position) < GotoDistance)
        {
            CurState = State.GOTO;
        }
    }
    void Goto()
    {
        transform.LookAt(Target);
        //Line of Sight implementation

        Vector3 fwd = transform.transform.TransformDirection(Vector3.forward);
        RaycastHit PlayerHit;

        if (Physics.Raycast(transform.position, fwd, out PlayerHit))
        {
            if( PlayerHit.transform.tag != PlayerTag)
            {
                CurState = State.LOOKFOR;
                return;
            }
        }

        if (Vector3.Distance(Target.position, transform.position) > AttackDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
        }
        else
        {
            CurState = State.ATTACK;
        }
    }
    void Attack()
    {
        CurTime = CurTime - Time.deltaTime;
        transform.LookAt(Target);
        if(CurTime < 0)
        {
            PlayerScript.health = PlayerScript.health - 1;
            CurTime = AttackTimer;
        }
        if (Vector3.Distance(Target.position, transform.position) > AttackDistance)
        {
            CurState = State.GOTO;
        }
    }
}
