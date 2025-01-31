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
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimation();
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

    #endregion



    #region INPUT SYSTEM

    public void MovimentPlayer(InputAction.CallbackContext value)
    {
        newImput = value.ReadValue<Vector2>();
    }

    public void JumpPlayer(InputAction.CallbackContext value)
    {

    }

    public void AtackPlayer(InputAction.CallbackContext value)
    {

    }

    public void AtackSpecialPlayer(InputAction.CallbackContext value)
    {

    }

    #endregion
}
