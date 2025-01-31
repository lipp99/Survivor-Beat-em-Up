using UnityEngine;
using UnityEngine.AI;

public class AIPatrol : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    NavMeshAgent agent;
    public Transform player;
    Animator anim;

    string Anim_Idle = "Z_Idle";
    public string Anim_Walk = "Z_Walk";
    string Anim_Run = "Z_Run_InPlace";
    string Anim_Atack = "";

    bool lib = true, IsRun = false;

    float distace;
    public float finalDistance = 3;

    Vector3 position;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        anim = GetComponent<Animator>();
        //anim.SetBool("IsWalk", true);
        //agent.enabled = false;
        Vector3 position = transform.position;
        position.y = 0f; // Fixa o personagem no nível do solo
        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        //PositionY();
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(Anim_Walk) && lib)
        {
            //print("A animação do inimigo chegou ao fim");
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
            {
               //print("A animação do inimigo chegou ao fim");
               IsRun = true;
               lib = false;
               anim.SetBool("IsRun", true);
            }      
        }


        distace = Vector3.Distance(player.transform.position, transform.position);
        distace = Mathf.Round(distace * 100) / 100;
        //float dist = player.transform.position.z - 3;
        //print("A distancia é de: " + dist);

        if (distace > finalDistance)
        {
            
            if (player && IsRun)
            {
                if (agent)
                {
                    //position = transform.position;
                    //position.y = 0f; // Fixa o personagem no nível do solo
                    //transform.position = position;
                    //print("Está se movimentando");
                    agent.SetDestination(player.position);
                    
                    
                }
            }
        }

        else
        {
            agent.speed = 0;
            agent.angularSpeed = 0;
            //agent.acceleration = 0;
            IsRun = false;  
            anim.SetBool("IsRun", false);
            anim.SetBool("IsAtack", true);
          
           
        }

      
        
    }

    void PositionY()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        Vector3 position = transform.position;
        position.y = 0f; // Fixa o personagem no nível do solo
        transform.position = position;
    }

    public void SetaPosicao()
    {
        agent.updateRotation = false;
        agent.SetDestination(transform.position);
        agent.enabled = false;
        //agent.updateRotation = false;


    }


    public void Ativa()
    {
        agent.enabled = true;
    }
}
