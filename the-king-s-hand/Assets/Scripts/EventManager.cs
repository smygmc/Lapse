using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EventManager : MonoBehaviour
{
    private static EventManager _singleton;

    public static EventManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
            {
                _singleton = value;
            }
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(EventManager)} instance already exists, destorying duplicate!");
                Destroy(value);
            }

        }
    }



    public UnityEvent NewCardInstantiated=new UnityEvent();
    public UnityEvent<bool> CardDestroyed=new UnityEvent<bool>(); //boolean identifies which direction the card swiped

    public void Awake()
    {

       

    }

   

}
