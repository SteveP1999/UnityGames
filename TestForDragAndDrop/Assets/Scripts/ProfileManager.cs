using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
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
    public Profile[] profiles = new Profile[3];
    private int activePlayerIndex = -1;

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
        profiles = new Profile[3];
        Profile player1 = new Profile();
        Profile player2 = new Profile();
        Profile player3 = new Profile();
        player1.setName("Kis Béla");
        player2.setName("Nagy Anna");
        player3.setName("Kovács Lacika");
        profiles[0] = player1;
        profiles[1] = player2;
        profiles[2] = player3;
    }

    public void setActivePlayerIndex(int val)
    {
        activePlayerIndex = val;
    }

    public int getActivePlayerIndex()
    {
        return activePlayerIndex;
    }

    public void dataChanged()
    {
        modifyData();

        for (int i = 0; i < profiles.Length; i++)
        {
            StartCoroutine(dbManager.saveUser(profiles[i].getName(), profiles[i].getAge(), profiles[i].getLevel(), profiles[i].getPlayer(), profiles[i].getActive()));
        }
    }

    public void modifyData()
    {
        //First user:
        profiles[0].setName(inputField1.text);

        if (toggle1Kicsi.isOn == true)
        {
            profiles[0].setAge("young");
        }
        else if (toggle1Nagy.isOn == true)
        {
            profiles[0].setAge("old");
        }
        else
        {
            profiles[0].setAge("");
        }

        if(toggle1Active.isOn)
        {
            profiles[0].setActive(true);
        }
        else
        {
            profiles[0].setActive(false);
        }
        
        //Second user:
        profiles[1].setName(inputField2.text);

        if (toggle2Kicsi.isOn == true)
        {
            profiles[1].setAge("young");
        }
        else if (toggle2Nagy.isOn == true)
        {
            profiles[1].setAge("old");
        }
        else
        {
            profiles[1].setAge("");
        }

        if (toggle2Active.isOn)
        {
            profiles[1].setActive(true);
        }
        else
        {
            profiles[1].setActive(false);
        }

        //Third user:
        profiles[2].setName(inputField3.text);

        if (toggle3Kicsi.isOn == true)
        {
            profiles[2].setAge("young");
        }
        else if (toggle3Nagy.isOn == true)
        {
            profiles[2].setAge("old");
        }
        else
        {
            profiles[2].setAge("");
        }

        if (toggle3Active.isOn)
        {
            profiles[2].setActive(true);
        }
        else
        {
            profiles[2].setActive(false);
        }
    }









    //public void activityChanged(int id)
    //{
    //    if (profiles[id].getActive() == false)
    //    {
    //        for (int i = 0; i < 3; i++)
    //        {
    //            profiles[i].setActive(true);
    //        }

    //        profiles[id].setActive(true);
    //    }
    //    else
    //    {
    //        profiles[id].setActive(false);
    //    }
    //    StartCoroutine(dbManager.saveUser(profiles[id].getName(), profiles[id].getAge(), profiles[id].getLevel(), profiles[id].getPlayer(), profiles[id].getActive()));
    //}

    //public void ageChanged(int id)
    //{
    //    if(profiles[id].getAge() == "young")
    //    {
    //        profiles[id].setAge("old");
    //    }
    //    else
    //    {
    //        profiles[id].setAge("young");
    //    }
    //    StartCoroutine(dbManager.saveUser(profiles[id].getName(), profiles[id].getAge(), profiles[id].getLevel(), profiles[id].getPlayer(), profiles[id].getActive()));
    //}

    //public void usernameValue(int id)
    //{
    //    profiles[id].setName(inputFields[id].text);
    //    StartCoroutine(dbManager.saveUser(profiles[id].getName(), profiles[id].getAge(), profiles[id].getLevel(), profiles[id].getPlayer(), profiles[id].getActive()));
    //}
}
