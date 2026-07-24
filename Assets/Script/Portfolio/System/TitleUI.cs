using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject classSelectPanel;
    [SerializeField] private string gameSceneName = "GameScene";

    public void OnClickStart()
    {
        startPanel.SetActive(false);
        classSelectPanel.SetActive(true);
    }

    public void OnClickWarrior() => SelectClassAndStart(PlayerClassType.Warrior);
    public void OnClickArcher() => SelectClassAndStart(PlayerClassType.Archer);
    public void OnClickMage() => SelectClassAndStart(PlayerClassType.Mage);

    private void SelectClassAndStart(PlayerClassType classType)
    {
        GameManager.Instance.SetSelectedClass(classType);
        SceneManager.LoadScene(gameSceneName);
    }
}