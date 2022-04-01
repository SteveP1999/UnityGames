using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private InputField inputField1;
    [SerializeField] private InputField inputField2;
    [SerializeField] private InputField inputField3;

    [SerializeField] private Toggle toggle1Kicsi;
    [SerializeField] private Toggle toggle1Nagy;
    [SerializeField] private Toggle toggle2Kicsi;
    [SerializeField] private Toggle toggle2Nagy;
    [SerializeField] private Toggle toggle3Kicsi;
    [SerializeField] private Toggle toggle3Nagy;

    [SerializeField] private Toggle toggle1Active;
    [SerializeField] private Toggle toggle2Active;
    [SerializeField] private Toggle toggle3Active;
        
    [SerializeField] private DbManager dbManager;

    public static ProfileManager profileManager;
    public SubUser[] subUsers = new SubUser[3];
    public Scores[] scores = new Scores[3];
    private int activePlayerIndex = -1;
    #endregion

    void Awake()
    {
        if(profileManager == null)
        {
            DontDestroyOnLoad(gameObject);
            profileManager = this;
        }
        else if(profileManager != this)
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {
        subUsers = new SubUser[3];
        scores = new Scores[3];
        SubUser player1 = new SubUser();
        SubUser player2 = new SubUser();
        SubUser player3 = new SubUser();
        Scores score1 = new Scores();
        score1.bestParallelScoreInPairGame = 2;
        Scores score2 = new Scores();
        score2.bestParallelScoreInPairGame = 3;
        Scores score3 = new Scores();
        score3.bestParallelScoreInPairGame = 8;
        scores[0] = score1;
        scores[1] = score2;
        scores[2] = score3;
        player1.userName = "Kis Béla";
        player2.userName = "Nagy Anna";
        player3.userName = "Kovács Lacika";
        subUsers[0] = player1;
        subUsers[1] = player2;
        subUsers[2] = player3;
    }

    public void setActivePlayerIndex(int val)
    {
        activePlayerIndex = val;
    }

    public int getActivePlayerIndex()
    {
        return activePlayerIndex;
    }


    public void modifyData()
    {
        //First user:
        subUsers[0].userName = inputField1.text;

        if (toggle1Kicsi.isOn == true)
        {
            subUsers[0].age = "young";
        }
        else if (toggle1Nagy.isOn == true)
        {
            subUsers[0].age = "old";
        }
        else
        {
            subUsers[0].age = "";
        }

        if(toggle1Active.isOn)
        {
            subUsers[0].active = true;
        }
        else
        {
            subUsers[0].active = false;
        }

        //Second user:
        subUsers[1].userName = inputField2.text;

        if (toggle2Kicsi.isOn == true)
        {
            subUsers[1].age = "young";
        }
        else if (toggle2Nagy.isOn == true)
        {
            subUsers[1].age = "old";
        }
        else
        {
            subUsers[1].age = "";
        }

        if (toggle2Active.isOn)
        {
            subUsers[1].active = true;
        }
        else
        {
            subUsers[1].active = false;
        }

        //Third user:
        subUsers[2].userName = inputField3.text;

        if (toggle3Kicsi.isOn == true)
        {
            subUsers[2].age = "young";
        }
        else if (toggle3Nagy.isOn == true)
        {
            subUsers[2].age = "old";
        }
        else
        {
            subUsers[2].age = "";
        }

        if (toggle3Active.isOn)
        {
            subUsers[2].active = true;
        }
        else
        {
            subUsers[2].active = false;
        }
    }
}
