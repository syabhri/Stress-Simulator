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

    [Header("External Variables")]
    public PlayerData playerData;
    public TimeContainer CurrentTime;

    [Header("Reference")]
    public ThingRuntimeSet noticePanel;
    public ThingRuntimeSet Player;

    [Header("Conditions")]
    public BoolVariable IsPlaying;

    [Header("UI Output")]
    public StringVariable NoticeText;

    private SpriteRenderer playerSprite;
    private Animator playerAnimator;

    #endregion

    #region Unity Event Function
    private void Start()
    {
        playerSprite = Player.Item.GetComponent<SpriteRenderer>();
        playerAnimator = Player.Item.GetComponent<Animator>();
        if (IsPlaying.value)
        {
            //SpawnPlayer(playerData.playerPosition.position);
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
        if (!CheckDataPlayer())
        {
            NoticeText.Value = "Tolong Isi Nama Karakter";
            noticePanel.Item.SetActive(true);
            return;
        }
        IsPlaying.value = true;
        ChangeScene("GameplayScene");
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

    // Function Reserved 
    /*
    public void SpawnPlayer(Vector2 position)
    {
        Instantiate<GameObject>(playerData.avatar, new
            Vector3(position.x, position.y, 0),
            Quaternion.identity);
        Debug.Log("player spawed");
    }*/

    // assign the avatar sprite and animation to player character
    public void AssignAvatar(Sprite sprite, RuntimeAnimatorController animator)
    {
        playerSprite.sprite = sprite;
        playerAnimator.runtimeAnimatorController = animator;
    }

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
