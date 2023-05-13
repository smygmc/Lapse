using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Game_Manager : MonoBehaviour
{
    public List<CardInfo_SO> cards; // initialized from editor
    public int current_card_index;
    public GameObject card_object;
    public GameObject card_prefab;
    public GameObject card_parent;
    UI_Manager ui_manager;
   
    #region GAME PARAMETERS
    public int military;
    public int justice;
    public int wealth;
    public int people;
    public int days_passed;
    #endregion

    #region EVENTS
    public UnityEvent NewCardInstantiated = new UnityEvent();
    public UnityEvent CardDestroyed = new UnityEvent();
    #endregion

    public enum GameParameters
    {
        military,
        wealth,
        people,
        justice,
    }
    private void Awake()
    {
        ui_manager = GetComponent<UI_Manager>();
      
    }
    private void Start()
    {

        current_card_index = 0;
        military = 60;
        justice = 60;
        people = 60;
        wealth = 60;

        //ui_manager.InstantiateNewCard(next_card_index);
        InstantiateNewCard();
        ui_manager.UpdateGameParameters();

    }


    public void OnCardDestroyed(bool swipe_left)
    {
        if (current_card_index == cards.Count - 1)
        {
            Debug.Log("that was the last card");
        }
        else
        {
            CardInfo_SO card = cards[current_card_index];
            if (swipe_left)
            {
                this.military += card.left_military;
                this.justice += card.left_justice;
                this.people += card.left_people;
                this.wealth += card.left_wealth;

            }
            else
            {

                this.military += card.right_military;
                this.justice += card.right_justice;
                this.people += card.right_people;
                this.wealth += card.right_wealth;
            }
            days_passed += card.days_passed;
            CardDestroyed?.Invoke();
            InstantiateNewCard();
        }
    }

    public void InstantiateNewCard()
    {
        current_card_index++;
        this.card_object = Instantiate(card_prefab, card_parent.transform);
        NewCardInstantiated?.Invoke();
    }
}
