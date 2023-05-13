using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




[CreateAssetMenu(menuName = "Scriptable Objects/New Card Info")]
public class CardInfo_SO : ScriptableObject
{
   public string info_text;
   public string character;
   public string left_option_text; // must be place on the right top of the card
   public string right_option_text; // vice versa
   public int days_passed;
    public Dictionary<int, int> left_values;
    public Dictionary<int, int> right_values;
    #region PARAMETERS
    public int left_military; // if one of them has no effect on parameters its=0
    public int left_justice;
    public int left_people;
    public int left_wealth;
    public int right_military;
    public int right_justice;
    public int right_people;
    public int right_wealth;
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
        left_values = new Dictionary<int, int>()
        {
            {(int)GameParameters.military,left_military  },
            {(int)GameParameters.wealth,left_wealth },
            {(int)GameParameters.people,left_people },
            {(int)GameParameters.justice,left_justice },

        };
        right_values = new Dictionary<int, int>()
        {
            {(int)GameParameters.military,right_military  },
            {(int)GameParameters.wealth,right_wealth },
            {(int)GameParameters.people,right_people },
            {(int)GameParameters.justice,right_justice },


        };
    }

}

