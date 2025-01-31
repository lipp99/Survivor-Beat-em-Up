using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CharacterLife : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool IsDead = false;
    public int health;
    private Animator anim;
    string AnimDeadForward = "Z_FallingForward";
    string AnimDeadBack = "Z_FallingBack";
    string NameAnimation;
    bool lib = false;

    float timer = 0;
    float duration = 0.5f;
    public float interval = 0.1f;
    SkinnedMeshRenderer[] skinnedMeshRenderer;

    public static CharacterLife characterLife;

   
   
    void Start()
    {
        anim = GetComponent<Animator>();
        characterLife = this;
        skinnedMeshRenderer = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        //StopAnimations();
    }


    void StopAnimations()
    {

        if (anim.GetCurrentAnimatorStateInfo(0).IsName(AnimDeadForward) && lib)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                anim.SetBool("IsDeadNormal", false);
                print("A animação foi parada");
                lib = false;
            }
        }




    }

    public void NameAnimatios(string name)
    {
        NameAnimation = name;
    }

    public void Damege(int dano)
    {
        //print("ENTROU NO CHARACTER LIFE");
        if (!IsDead)
        {
            health -= dano;
            //print(health);
            if (health <= 0)
            {
                // print(health);
                if (this.GetComponent<PlayerController>() != null)
                {
                    //GAME OVER
                   // print("O player está morto");
                    //this.GetComponent<PlayerController>().gameObject.SetActive(false);
                    RestartCurrentScene();
                }
                else
                {
                    ////print("O inimigo está morto");
                    anim.SetBool(NameAnimation, true);
                    AIPatrol enemie = GetComponent<AIPatrol>();

                    if (enemie)
                    {
                        enemie.SetaPosicao();
                        enemie.enabled = false;
                        StartCoroutine(DesabledMeshEnemy());
                    }

                    //StartCoroutine(DesabledMeshEnemy());
                    IsDead = true;
                }
            }
            else if (!IsDead)
            {
                if (this.GetComponent<PlayerController>() != null)
                {
                    //print("Player Está recebendo danos");
                   if(ControlUi.controlUi)
                    {
                        ControlUi.controlUi.ChamaControl();
                    }
                }

                else
                {
                    // print("O inimigo está recebendo danos");
                    //Destroy(this.gameObject);
                    //this.gameObject.SetActive(false);
                }

            }
        }



    }

    //IEnumerator teste()
    //{
    //    yield return new WaitForSeconds(5f);
    //}

    IEnumerator DesabledMeshEnemy()
    {
        yield return new WaitForSeconds(5f);
        //print("Entrou na corrtina do inimgo morto");

        timer = 0;

        while (timer < duration)
        {
            // Alterna a visibilidade do inimigo
            ControllVisible();

            // Aguarda por um intervalo antes de alternar de novo
            yield return new WaitForSeconds(interval);

            // Incrementa o timer
            timer += interval;
            //print(timer);
        }

        gameObject.SetActive(false);
    }



    void ControllVisible()
    {
        foreach (SkinnedMeshRenderer mesh in skinnedMeshRenderer)
        {

            mesh.enabled = !mesh.enabled;

        }
    }

    void RestartCurrentScene()
    {
        StartCoroutine(ResetScene());
    }

    IEnumerator ResetScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

   
}
