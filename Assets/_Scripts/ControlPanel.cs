using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ControlPanel : MonoBehaviour
{
    public List<NavMeshAgent> agents;
    public List<MonoBehaviour> scripts;

    public PlayerBehaviour player;
    public bool isGamePause = false;

    public GameObject pauseLabelPanel;

    public PlayerDataSO PlayerData;

    // Start is called before the first frame update
    void Start()
    {
        agents = FindObjectsOfType<NavMeshAgent>().ToList();
        player = FindObjectOfType<PlayerBehaviour>();

        foreach (var enemy in FindObjectsOfType<EnemyBehaviour>())
        {
            scripts.Add(enemy);
        }

        scripts.Add(player);
        scripts.Add(FindObjectOfType<CameraController>());

        LoadFromPlayerPreferences();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onLoadButtonPressed()
    {
        player.controller.enabled = false;
        player.transform.position = PlayerData.playerPosition;
        player.transform.rotation = PlayerData.PlayerRotation;
        player.health = PlayerData.playerHealth;
        player.controller.enabled = true;
    }

    public void onSaveButtonPressed()
    {
        PlayerData.playerPosition = player.transform.position;
        PlayerData.PlayerRotation = player.transform.rotation;
        PlayerData.playerHealth = player.health;

        SaveToPlayerPreferences();
    }

    public void onPauseButtonTogled()
    {
        isGamePause = !isGamePause;
        pauseLabelPanel.SetActive(isGamePause);

        foreach (var agent in agents)
        {
            agent.enabled = !isGamePause;
        }

        foreach (var script in scripts)
        {
            script.enabled = !isGamePause;
        }
    }

    public void OnApplicationQuit()
    {
        SaveToPlayerPreferences();
    }

    public void LoadFromPlayerPreferences()
    {
        PlayerData.playerPosition.x = PlayerPrefs.GetFloat("playerPositionX");
        PlayerData.playerPosition.y = PlayerPrefs.GetFloat("PlayerPositionY");
        PlayerData.playerPosition.z = PlayerPrefs.GetFloat("PlayerPositionZ");

        PlayerData.PlayerRotation.x = PlayerPrefs.GetFloat("playerRotationX");
        PlayerData.PlayerRotation.y = PlayerPrefs.GetFloat("playerRotationY");
        PlayerData.PlayerRotation.z = PlayerPrefs.GetFloat("playerRotationZ");
        PlayerData.PlayerRotation.w = PlayerPrefs.GetFloat("playerRotationW");

        PlayerData.playerHealth = PlayerPrefs.GetInt("playerHealt");
    }

    public void SaveToPlayerPreferences()
    {
        PlayerPrefs.SetFloat("playerPositionX", PlayerData.playerPosition.x);
        PlayerPrefs.SetFloat("PlayerPositionY", PlayerData.playerPosition.y);
        PlayerPrefs.SetFloat("PlayerPositionZ", PlayerData.playerPosition.z);

        PlayerPrefs.SetFloat("playerRotationX", PlayerData.PlayerRotation.x);
        PlayerPrefs.SetFloat("playerRotationY", PlayerData.PlayerRotation.y);
        PlayerPrefs.SetFloat("playerRotationZ", PlayerData.PlayerRotation.z);
        PlayerPrefs.SetFloat("playerRotationW", PlayerData.PlayerRotation.w);

        PlayerPrefs.SetInt("playerHealth", PlayerData.playerHealth);

        PlayerPrefs.Save();
    }
}
