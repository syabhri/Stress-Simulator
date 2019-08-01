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
    public FloatVariable LoadingProgress;

    [Header("External Variables")]
    public PlayerData playerData;
    public TimeContainer CurrentTime;
    //public TimeContainer TimePasser;

    [Header("Reference")]
    public ThingRuntimeSet noticePanel;
    public ThingRuntimeSet LoadingPanel;
    public ThingRuntimeSet Player;

    [Header("Conditions")]
    public bool IsPlaying;
    public BoolVariable[] ignoreInput;

    [Header("UI Output")]
    public StringVariable NoticeText;

    //[Header("Events")]
    //public GameEvent OnPaused;


    //private SpriteRenderer playerSprite;
    //private Animator playerAnimator;

    #endregion

    #region Unity Event Function
    private void Awake()
    {
        // set input group to 0;

        //playerSprite = Player.Item.GetComponent<SpriteRenderer>();
        //playerAnimator = Player.Item.GetComponent<Animator>();
        if (IsPlaying)
        {
            //AssignAvatar();
            SpawnPlayer(playerData.playerPosition.position);
        }

    }

    private void Update()
    {
        if (IsPlaying)
        {
            CheckEndGame();
        }
    }
    #endregion

    #region GameManager function
    public void PlayGame(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        LoadingPanel.Item.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            Debug.Log(progress);

            LoadingProgress.value = progress;

            yield return null;
        }
    }

    public void NewGame(int sceneIndex)
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
        
        PlayGame(sceneIndex);
    }

    public void LoadGame()
    {
        Debug.LogWarning("Function not implemented yet");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        Debug.Log("GamePaused");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Debug.Log("GamePaused");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void QuitToMEnu()
    {
        ChangeScene(0);
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
                    IsPlaying = false;
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

    private bool IgnoreInput()
    {
        foreach (BoolVariable condition in ignoreInput)
        {
            if (condition.value)
            {
                return true;
            }
        }
        return false;
    }
    #endregion
}
