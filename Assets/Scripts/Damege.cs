using UnityEngine;

public class Damege : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int dano = 1;
    private int danoHead = 3;
    bool lib = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
    private void OnTriggerEnter(Collider other)
    {
        CharacterLife character = other.GetComponent<CharacterLife>();
        //int layer = other.gameObject
  
      
        if (other.CompareTag("Player"))
        {
            if (character)
            { 
              character.Damege(dano);
            }            
        }

        else if (other.transform.root.CompareTag("Enemies"))
        {

            if (character)
            {
                print("Levou Tiro no corpo");
                character.NameAnimatios("IsDeadNormal");
                character.Damege(dano);
            }


            //if (other.CompareTag("HeadShot"))
            //{

            //    if (character)
            //    {
            //        print("Levou tiro na cabeça");
            //        character.NameAnimatios("IsDeadHead");
            //        character.Damege(danoHead);

            //    }

            //    else
            //    {
            //        print(character);
            //    }
            //}

            //else if (character)
            //{
            //    print("Levou Tiro no corpo");
            //    character.NameAnimatios("IsDeadNormal");
            //    character.Damege(dano);
            //}
        }

    }
}
