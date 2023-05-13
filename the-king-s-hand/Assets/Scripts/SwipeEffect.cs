using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeEffect : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector3 initialPosition;
    private float distanceMoved;
    private bool swipeLeft;
    public TMP_Text right_option;
    public TMP_Text left_option;
    public float opacity;// bu arkadaþý eventimiz için kullanýcaz
    private int lerp_constant=250;
    
    private void Start()
    {

        right_option.alpha = 0;
        left_option.alpha = 0;

    }

    public void OnDrag(PointerEventData eventData)
    {

        transform.localPosition = new Vector2(transform.localPosition.x + eventData.delta.x, transform.localPosition.y + eventData.delta.y);
        opacity= Mathf.SmoothStep(0, 1, Math.Abs(initialPosition.x - transform.localPosition.x) / lerp_constant);
        if (transform.localPosition.x - initialPosition.x > 0)// means it swiped to right
        {
            right_option.alpha = opacity;
            UI_Manager.Singleton.DotOpacity(false, opacity);
            transform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(0, -30,
                (initialPosition.x + transform.localPosition.x) / (Screen.width / 2)));
        }
        else
        {

            left_option.alpha = opacity;
            UI_Manager.Singleton.DotOpacity(true, opacity);
            transform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(0, 30,
                (initialPosition.x - transform.localPosition.x) / (Screen.width / 2)));
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        initialPosition = transform.localPosition; // kartýn initial pozisyonu
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       
        distanceMoved = Math.Abs(transform.localPosition.x - initialPosition.x);

        if (distanceMoved < 0.1f * Screen.width) // ekranýn yüzde 20sinden kýsa bi drag yaptýysa inital pozitiona geri dönsün kart(0.1 ynaýna f koymayýnca yanlýþ hesaplýyo)
        {
            transform.localPosition = initialPosition;
            transform.localEulerAngles = Vector3.zero;
            right_option.alpha = 0;
            left_option.alpha = 0;
           
        }
        else
        {
            if (transform.localPosition.x < initialPosition.x)
            {
                swipeLeft = true;
            }
            else
            {
                swipeLeft = false;
            }
            StartCoroutine(MovedCard());

        }
        UI_Manager.Singleton.ResetDotOpacity();
    }

    private IEnumerator MovedCard()
    {
        float time = 0;
        while (this.transform.position.x > -2000 & this.transform.position.x<3000) //GetComponent<Image>().color != new Color(0.5f, 0.5f, 0.5f, 0)
        {
            time += Time.deltaTime;
            if (swipeLeft)
            {
                transform.localPosition = new Vector3(Mathf.SmoothStep(transform.localPosition.x,
                    transform.localPosition.x - Screen.width, time), transform.localPosition.y, 0);
            }
            else
            {
                transform.localPosition = new Vector3(Mathf.SmoothStep(transform.localPosition.x,
                    transform.localPosition.x + Screen.width, time), transform.localPosition.y, 0);
            }
            GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, Mathf.SmoothStep(1, 0, time));//t=value used to interpolate between two point if its 0 then output will be equal to start point if its 1 it will be equal to end point
            // bu böyle yavaþ yavaþ time =1 olcak ve while döngüsü biticek
            yield return null;// bir frame geç 
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
      
       GameObject.Find("GameManager").GetComponent<Game_Manager>().OnCardDestroyed(swipeLeft);
    }
}
