using System.Collections;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Animator m_Animator;
    private int comboStep = 0;
    private float lastClickTime;
    public float comboResetTime = 1f;
    public static CombatSystem combatSystem;
    private bool canAttack = true;
    private bool animFinished = true;
    void Start()
    {
        combatSystem = this;
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastClickTime > comboResetTime)
        {
            comboStep = 0;
            m_Animator.SetTrigger("Idle");
        }
    }


    public void ComboAttack()
    {
        if (canAttack && animFinished)
        {


            lastClickTime = Time.time;
            //comboStep = (comboStep % 3) + 1; // Alterna entre 1, 2 e 3
            comboStep++;

            if (comboStep > 3)
            {
                animFinished = false;
                 comboStep = 1;
                StartCoroutine(NextCombo());
                return;
            }
            m_Animator.SetTrigger("Atack" + comboStep); // Aciona "Attack1", "Attack2" ou "Attack3"
           
            Invoke(nameof(ResetAnimation), GetCurrentAnimationLength());
            //canAttack = false;
        }
    }

    void ResetAnimation()
    {
        m_Animator.SetTrigger("Idle"); // Volta para Idle
        canAttack = true; // Libera para atacar novamente
    }

    float GetCurrentAnimationLength()
    {
        AnimatorStateInfo state = m_Animator.GetCurrentAnimatorStateInfo(0);
        return state.length;
    }

    IEnumerator NextCombo()
    {
        yield return new WaitForSeconds(1f);
        animFinished = true;
    }



}
