using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public struct Enemie
{
    public bool zombie_1;
    //public bool fishSilver;
    //public bool fishRed;
    //public bool monkei;
    //public bool moto;
    //public bool dino_moto;
    //public bool ballon;

}
public class EnemiesAtacks : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject playerMoviment;
    public float rotationSpeed = 5f;
    private Animator anim;
    public float temp = 1f;
    float distace;
    public float finalDistance = 3;
    public Enemie enemie;

    public Transform boxColliderEsq;
    public Transform boxColliderDir;
    public Transform boxColliderHead;

    //public Transform maoEsq;
    //private Transform teste;
    //public Transform maoDir;

    public Transform maoEsq;
    public Transform maoDir;
    public GameObject esfera;

    private Transform posDir;
    private Transform posEsq;
    private Transform head;

    bool lib = true;
    void Start()
    {
        anim = GetComponent<Animator>();
        //StartCoroutine(TempRun());
        //StartCoroutine(ChamaRun());
        //pos = anim.GetBoneTransform(HumanBodyBones.RightHand);

        //teste.position = Vector3.zero;  
        //teste.position = maoDir.position;

    }

    // Update is called once per frame
    void Update()
    {
        //Moviment();
        UpdadeBoxColliders();
        //boxColliderDir.gameObject.transform.position = Vector3.MoveTowards(boxColliderDir.gameObject.transform.position, boxColliderDir.gameObject.transform.position, 0.5f);
        //esfera.transform.position = ;

        //maoDir.position;
        //boxColliderDir.gameObject.transform.position = maoDir.transform.position;
        //boxColliderDir.gameObject.transform.rotation = maoDir.transform.rotation;
        //if ((anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") || anim.GetCurrentAnimatorStateInfo(0).IsName("Z_Idle")) && lib)
        //{
           
        //    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
        //    {
        //        print("A animação de IDLE do inimigo chegou ao fim");
        //        lib = false;
        //        AIPatrol enemie = GetComponent<AIPatrol>();

        //        if (enemie)
        //        {
        //           enemie.Ativa();
        //            //enemie.enabled = true;
                  
        //        }
        //    }
        //}


    }

    private void FixedUpdate()
    {
        //print(maoDir.position);
        
    }


    void Moviment()
    {
        if(enemie.zombie_1)
        {
            if (playerMoviment && 1 == 3)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerMoviment.transform.position, 0.2f);
            }
            distace = Vector3.Distance(playerMoviment.transform.position, transform.position);

            if (distace > finalDistance)
            {
                Vector3 direction = playerMoviment.transform.position - transform.position;
                direction.y = 0; // Mantém a rotação apenas no plano horizontal

                if (direction.magnitude > 0.1f) // Garante que o inimigo não rotacione sem necessidade
                {
                    // Calcula a rotação alvo
                    Quaternion targetRotation = Quaternion.LookRotation(direction);

                    // Interpola suavemente para a rotação alvo
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
            }

            else
            {              
                anim.SetBool("IsAtack", true);
                anim.SetBool("IsRun", false);
            }
        }

       
    }
    //private void LateUpdate()
    //{

    //    posDir = anim.GetBoneTransform(HumanBodyBones.RightHand);
    //    boxColliderDir.position = posDir.position;

    //    posEsq = anim.GetBoneTransform(HumanBodyBones.LeftHand);
    //    boxColliderEsq.position = posEsq.position;


    //}


    void UpdadeBoxColliders()
    {
        posDir = anim.GetBoneTransform(HumanBodyBones.RightHand);
        boxColliderDir.position = posDir.position;

        posEsq = anim.GetBoneTransform(HumanBodyBones.LeftHand);
        boxColliderEsq.position = posEsq.position; ;

        head = anim.GetBoneTransform(HumanBodyBones.Head);
        boxColliderHead.position = head.position;
    }

    private void OnTriggerEnter(Collider other)
    {
       
        //if (other.CompareTag("Player"))
        //{
        //    print("ESBARROU NO PLAYER COMO OS BRAÇOS");
        //}
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Player")
        {
            //print("ESBARROU NO PLAYER");
        }
    }


    IEnumerator TempRun()
    {
        yield return new WaitForSeconds(Random.Range(0.8f,2));
        anim.SetBool("IsWalk", true);
        StartCoroutine(ChamaRun());

    }

    IEnumerator ChamaRun()
    {
        yield return new WaitForSeconds(temp);
        anim.SetBool("IsRun", true);
        //anim.SetBool("IsDeadNormal", true);
    }

}
