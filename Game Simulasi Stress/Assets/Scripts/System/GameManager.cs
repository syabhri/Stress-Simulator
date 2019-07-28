using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    #region Variables and Class Reverences
    [Header("Properties")]
    public Vector2Variable DefaultSpawnPoint;
    public TimeContainer GameTimeUp;
    public TimeContainer StarTime;

    [Header("External Variables")]
    public PlayerData playerData;
    public TimeContainer CurrentTime;
    //public TimeContainer TimePasser;

    [Header("Reference")]
    public ThingRuntimeSet noticePanel;
    public ThingRuntimeSet Player;

    [Header("Conditions")]
    public BoolVariable IsPlaying;

    [Header("UI Output")]
    public StringVariable NoticeText;

    [Header("Event")]
    public GameEvent OnTimeSet;

    //private SpriteRenderer playerSprite;
    //private Animator playerAnimator;

    #endregion

    #region Unity Event Function
    private void Start()
    {
        //playerSprite = Player.Item.GetComponent<SpriteRenderer>();
        //playerAnimator = Player.Item.GetComponent<Animator>();
        if (IsPlaying.value)
        {
            //SpawnPlayer(playerData.playerPosition.position);
            //AssignAvatar();
            SpawnPlayer(playerData.playerPosition.position);
        }

    }

    private void Update()
    {
        if (IsPlaying.value)
        {
            CheckEndGame();
        }
    }

    private void OnApplicationQuit()
    {
        IsPlaying.value = false;
    }
    #endregion

    #region GameManager function
    public void PlayGame()
    {
        IsPlaying.value = true;
        ChangeScene("GameplayScene");
    }

    public void NewGame()
    {
        // Check Player Data
        if (!CheckDataPlayer())
        {
            NoticeText.Value = "Tolong Isi Nama Karakter";
            noticePanel.Item.SetActive(true);
            return;
        }

        // Set Starting Clock Time
        StarTime.time  = new TimeFormat(0, 7, 0);

        /* Reserved
        // set offset to ending time according to set start clock time
        GameTimeUp.time.days += StarTime.time.days;
        GameTimeUp.time.hours += StarTime.time.hours;
        GameTimeUp.time.minutes += StarTime.time.minutes;
        */

        // reset player knowlage
        foreach (FloatVariable knowlage in playerData.knowlage)
        {
            knowlage.SetValue(0);
        }

        // Set Stat Accordingly
        foreach (FloatVariable stat in playerData.stats)
        {
            if (stat.name == "Energy")
            {
                stat.SetValue(100);
                Debug.Log("Energy set to 100");
            } else if (stat.name == "Money")
            {
                stat.SetValue(10);
                Debug.Log("Coin set to 10");
            }
            else
            {
                stat.SetValue(0);
                Debug.Log("The rest of stat reset to 0");
            }
        }

        // set player position to default position
        playerData.playerPosition.position = DefaultSpawnPoint.position;
        
        PlayGame();
    }

    public void LoadGame()
    {
        Debug.LogWarning("Function not implemented yet");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void ChangeScene(int id)
    {
        SceneManager.LoadScene(id);
    }

    
    public void SpawnPlayer(Vector2 position)
    {
        Instantiate<GameObject>(playerData.avatar, new
            Vector3(position.x, position.y, 0),
            Quaternion.identity);
        Debug.Log("player spawed");
    }
 
    /* Reserved (For Use With Unity Timeline)
    // assign the avatar sprite and animation to player character
    public void AssignAvatar(Sprite sprite, RuntimeAnimatorController animator)
    {
        //playerSprite.sprite = sprite;
        //playerAnimator.runtimeAnimatorController = animator;
    } */

    public void CheckEndGame()
    {
        if (CurrentTime.time.days >= GameTimeUp.time.days)
        {
            if (CurrentTime.time.hours >= GameTimeUp.time.hours)
            {
                if (CurrentTime.time.minutes >= GameTimeUp.time.minutes)
                {
                    IsPlaying.value = false;
                    ChangeScene(2);
                }
            }
        }
    }

    // will return false if required player data is incomplete or missing
    public bool CheckDataPlayer()
    {
        if (playerData.character_name.Value == string.Empty)
            return false;
        else
            return true;
    }
    #endregion
}
