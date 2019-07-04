using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables and Class Reverences
    [Header("Properties")]
    public Vector2Variable spawnPoint;

    [Header("Reference")]
    public PlayerData playerData;

    [Header("Conditions")]
    public BoolVariable IsPlaying;

    #endregion

    #region Unity Event Function
    private void Start()
    {
        if (IsPlaying.value)
        {
            SpawnPlayer(playerData.playerPosition.position);
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
        ChangeScene("GameplayScene");
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public static void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public static void ChangeScene(int id)
    {
        SceneManager.LoadScene(id);
    }

    public void SpawnPlayer(Vector2 position)
    {
        Instantiate<GameObject>(playerData.avatar, new
            Vector3(position.x, position.y, 0),
            Quaternion.identity);
    }
    #endregion
}
