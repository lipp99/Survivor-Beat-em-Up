using UnityEngine;

public class HeadShot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int danoHead = 3;
    private CharacterLife character;
    void Start()
    {
        character = GetComponentInParent<CharacterLife>();
    }

    private void OnTriggerEnter(Collider other)
    {

        //CharacterLife character = other.GetComponent<CharacterLife>();
        //MovBullet bullet = other.GetComponent<MovBullet>();
        //if(bullet)
        //{
        //    if (character != null)
        //    {
        //        print("ACERTOU A CABEÇA DO INIMIGO");
        //        character.NameAnimatios("IsDeadHead");
        //        character.Damege(danoHead);
        //    }
        //}
       
    }
    
}
