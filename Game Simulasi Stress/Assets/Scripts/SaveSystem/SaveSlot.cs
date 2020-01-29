using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class SaveSlot : MonoBehaviour
{
    public string saveName;
    public GameData gameData;

    public Image image;
    public TextMeshProUGUI charaterName;
    public TextMeshProUGUI days;

    [Space]
    public UnityEvent OnSaveExist;
    public UnityEvent OnSaveEmpty;

    public SaveData saveData;

    private void OnEnable()
    {
        if (SaveManager.SaveExists(saveName))
        {
            updateInformation();
            OnSaveExist.Invoke();
        }
        else
        {
            OnSaveEmpty.Invoke();
        }
    }

    public void updateInformation()
    {
        saveData = SaveManager.Load<SaveData>(saveName);

        foreach (GameObject avatar in gameData.Avatars)
        {
            if (avatar.name == saveData.avatar)
            {
                image.sprite = avatar.GetComponent<SpriteRenderer>().sprite;
            }
        }

        charaterName.SetText(saveData.character_name);
        days.SetText(saveData.current_time[2].ToString());
    }
}
