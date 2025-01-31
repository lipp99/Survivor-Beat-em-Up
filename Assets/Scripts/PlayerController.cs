using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody m_Rigidbody;
    Animator m_Animator;
    Vector2 newImput;

    public float movSpeed;
    bool isWalk;
    bool isLookLeft;


    private Transform posDir;
    private Transform posEsq;
    private Transform head;

    public Transform boxColliderEsq;
    public Transform boxColliderDir;
    public Transform boxColliderHead;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimation();
        LookPlayer();
        UpdadeBoxColliders();
    }

    private void FixedUpdate()
    {
        MovPlayer();
    }

    #region METODOS 


    void MovPlayer()
    {

        isWalk = newImput.x != 0 ? true : false;    
        m_Rigidbody.linearVelocity = new Vector3(0, m_Rigidbody.linearVelocity.y, newImput.x * movSpeed);
       
    }

    void UpdateAnimation()
    {
       
        m_Animator.SetBool("IsWalk", isWalk);
    }

    void LookPlayer()
    {
        if(newImput.x > 0 && isLookLeft)
        {
            Flip();
        }
        else if(newImput.x < 0 && isLookLeft == false)
        {
            Flip();
        }
    }

    void Flip()
    {
        isLookLeft = !isLookLeft;

        switch(isLookLeft)
        {
            case true:
                transform.eulerAngles = new Vector3(0, 180f, 0);
                break;
            
            case false:
                transform.eulerAngles = new Vector3(0, 0, 0);
                break;
        }
    }

    void Jump(bool isPressed)
    {

    }

    void Atack()
    {
        if (CombatSystem.combatSystem)
        {
            CombatSystem.combatSystem.ComboAttack();
        }

        //print("Player atacando");

    }

    void AtackSpecial()
    {

    }

    void UpdadeBoxColliders()
    {
        posDir = m_Animator.GetBoneTransform(HumanBodyBones.RightHand);
        boxColliderDir.position = posDir.position;

        posEsq = m_Animator.GetBoneTransform(HumanBodyBones.LeftHand);
        boxColliderEsq.position = posEsq.position;

       head = m_Animator.GetBoneTransform(HumanBodyBones.Head);
       boxColliderHead.position = head.position;
    }





    #endregion



    #region INPUT SYSTEM

    public void MovimentPlayer(InputAction.CallbackContext value)
    {
        newImput = value.ReadValue<Vector2>();
    }

    public void JumpPlayer(InputAction.CallbackContext value)
    {
        if(value.started) //APERTOU O BOTÃO
        {
            Jump(true);
        }

        if(value.canceled)
        {
            Jump(false);
        }
    }

    public void AtackPlayer(InputAction.CallbackContext value)
    {
        if(value.started) //APERTOU O BOTÃO
        {
            Atack();    
        }

    }

    public void AtackSpecialPlayer(InputAction.CallbackContext value)
    {
        if(value.started)//APERTOU O BOTÃO
        {
            AtackSpecial();
        }
    }

    #endregion
}
