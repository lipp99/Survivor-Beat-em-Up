using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlUi : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Image dano;
    public TextMeshProUGUI life;
    int num = 3;

    public static ControlUi controlUi;

    private void Start()
    {
        controlUi = this;
    }
    public void ChamaControl()
    {
        StartCoroutine(ControllVisibleDano());
    }

   
    IEnumerator ControllVisibleDano()
    {
        num--;
        life.text = num.ToString();  
        dano.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        dano.gameObject.SetActive(false);
    }

}
