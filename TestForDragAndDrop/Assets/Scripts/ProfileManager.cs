using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
    [SerializeField] private InputField[] inputFields;

    public static ProfileManager profileManager;
    public Profile[] profiles = new Profile[3];
    private int activePlayerIndex = 0;

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
    public void activityChanged(int id)
    {
        if (profiles[id].getActive() == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                profiles[i].setActive(0);
            }

            profiles[id].setActive(1);

            for (int j = 0; j < 3; j++)
            {
                Debug.Log(profiles[j] + profiles[j].getName());
            }
        }
        else
        {
            profiles[id].setActive(0);
            for (int j = 0; j < 3; j++)
            {
                Debug.Log(profiles[id] + profiles[j].getName());
            }
        }
    }

    public void dataChange(int id)
    {
        profiles[id].setDataChanged(true);
    }

    public void usernameValue(int id)
    {
        profiles[id].setName(inputFields[id].text);
        Debug.Log(profiles[id].getName());
    }
}
