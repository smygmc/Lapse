using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UI_Manager : MonoBehaviour
{
    private static UI_Manager _singleton;

    public static UI_Manager Singleton{
        get => _singleton;
        private set
        {
            if (_singleton == null)
            {
                _singleton = value;
            }
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(UI_Manager)} instance already exists, destorying duplicate!");
                Destroy(value);
            }

        }
    }

    Game_Manager game_manager;
    public GameObject card_prefab;
    public GameObject Canvas;

    #region TEXT UI
    public TMP_Text card_info;
    public TMP_Text days_passed;
    public TMP_Text char_name;
    #endregion

    #region GAME PARAMETERS UI
    public Image military;
    public Image justice;
    public Image wealth;
    public Image people;
    #endregion
    #region DOTS
    public Image military_dot;
    public Image justice_dot;
    public Image wealth_dot;
    public Image people_dot;
    #endregion

    private void Awake()
    {
        game_manager = GetComponent<Game_Manager>();
        Singleton = this;
    }
    public void InstantiateNewCardParameters()
    {
        int current_card = game_manager.current_card_index;
        card_info.text = game_manager.cards[current_card].info_text;
        char_name.text = game_manager.cards[current_card].character;
        game_manager.card_object.GetComponent<SwipeEffect>().right_option.text = game_manager.cards[current_card].right_option_text;
        game_manager.card_object.GetComponent<SwipeEffect>().left_option.text = game_manager.cards[current_card].left_option_text;
        

    }
    public void UpdateGameParameters()
    {
        days_passed.text =$"You ruled for {game_manager.days_passed} days.";

        StartCoroutine(FillAnimation());
    }

    public IEnumerator FillAnimation()
    {
        Color _green = new Color(0, 0.2235294f, 0.1411765f, 1);
        Color _red = new Color(0.5215687f, 0,0, 1);
        Color _default = new Color(1, 1, 1, 1);

        float current_military = military.fillAmount;
        float current_wealth = wealth.fillAmount;
        float current_justice = justice.fillAmount;
        float current_people = people.fillAmount;

        float next_military = game_manager.military / 100f;
        float next_justice = game_manager.justice / 100f;
        float next_wealth = game_manager.wealth / 100f;
        float next_people = game_manager.people / 100f;

        military.color = next_military == current_military ? _default : next_military > current_military ? _green : _red;// ne yazdým be þiir gibi
        wealth.color = next_wealth == current_wealth ? _default : next_wealth > current_wealth ? _green : _red;
        people.color = next_people == current_people ? _default : next_people > current_people ? _green : _red;
        justice.color = next_justice == current_justice ? _default : next_justice > current_justice ? _green : _red;

        float duration = 1f;
        float t = 0;

            while (t < 1f)
            {
                t += Time.fixedDeltaTime / duration;
                military.fillAmount = Mathf.SmoothStep(current_military, next_military, t);
                justice.fillAmount = Mathf.SmoothStep(current_justice, next_justice, t);
                people.fillAmount = Mathf.SmoothStep(current_people, next_people, t);
                wealth.fillAmount = Mathf.SmoothStep(current_wealth, next_wealth, t);


            yield return null;
            }
        military.color = _default;
        wealth.color = _default;
        people.color = _default;
        justice.color = _default;
        Debug.Log($"anim:{military.fillAmount} real:{game_manager.military}");
        Debug.Log($"anim:{people.fillAmount} real:{game_manager.people}");
        Debug.Log($"anim:{wealth.fillAmount} real:{game_manager.wealth}");
        Debug.Log($"anim:{justice.fillAmount} real:{game_manager.justice}");



    }
    public void DotOpacity(bool swipe_left,float opacity_value)
    {
        CardInfo_SO card = game_manager.cards[game_manager.current_card_index];
        Color _default = new Color(1, 1, 1, 0);
        Color _new = new Color(1, 1, 1, opacity_value);
        if (swipe_left)
        {
            ResetDotOpacity();
            this.military_dot.color=card.left_military!=0?_new: _default;
            this.justice_dot.color = card.left_justice != 0 ? _new : _default;
            this.people_dot.color = card.left_people != 0 ? _new : _default;
            this.wealth_dot.color = card.left_wealth != 0 ? _new : _default;

        }
        else
        {
            ResetDotOpacity();
            this.military_dot.color = card.right_military != 0 ? _new : _default;
            this.justice_dot.color = card.right_justice != 0 ? _new : _default;
            this.people_dot.color = card.right_people != 0 ? _new : _default;
            this.wealth_dot.color = card.right_wealth != 0 ? _new : _default;
        }
    }
    public void ResetDotOpacity()
    {   
        Color _default= new Color(1, 1, 1, 0);
        this.military_dot.color = default;
        this.justice_dot.color = default;
        this.people_dot.color = default;
        this.wealth_dot.color = default;
    }
}
