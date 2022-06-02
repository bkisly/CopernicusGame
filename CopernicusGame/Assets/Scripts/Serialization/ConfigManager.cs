using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ConfigManager : MonoBehaviour
{
    public GameObject Player;
    public Item[] ItemDatabase;
    public AudioMixer AudioMixer;
    public GameObject Subtitles;

    void Start()
    {
        ReadSettings();
        if (Player != null) ReadPlayerProgress();

        if (SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 9) Cursor.visible = false;
        else Cursor.visible = true;
    }

    public void SavePlayerProgress(Vector2 playerPosition, int levelIndex)
    {
        List<int> items = new List<int>();
        List<int> equippedItems = new List<int>();

        foreach (Item item in Equipment.instance.Items) items.Add(item.ID);
        foreach (Item equippedItem in Equipment.instance.EquippedItems) equippedItems.Add(equippedItem.ID);

        PlayerConfigInfo playerConfigInfo = new PlayerConfigInfo(
            levelIndex,
            playerPosition,
            PlayerStats.instance.CurrentHealth,
            items,
            equippedItems,
            PlayerStats.instance.Points);

        Serializer.Serialize(playerConfigInfo);
    }

    private void ReadPlayerProgress()
    {
        PlayerConfigInfo playerConfigInfo = Serializer.DeserializeSave();

        if(playerConfigInfo != null)
        {
            // Set the position
            Player.transform.position = new Vector3(playerConfigInfo.PositionX, playerConfigInfo.PositionY);

            // Configure the equipment
            Equipment.instance.Items.Clear();
            Equipment.instance.EquippedItems.Clear();

            foreach (int itemIndex in playerConfigInfo.Items) Equipment.instance.Items.Add(ItemDatabase[itemIndex - 1]);
            foreach (int itemIndex in playerConfigInfo.EquippedItems) Equipment.instance.EquippedItems.Add(ItemDatabase[itemIndex - 1]);

            // Configure PlayerStats
            PlayerStats.instance.SetCurrentHealth(playerConfigInfo.CurrentHealth);
            PlayerStats.instance.Points = playerConfigInfo.Points;
        }
    }

    private void ReadSettings()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            SettingsConfigInfo settingsConfigInfo = Serializer.DeserializeSettings();

            if(settingsConfigInfo != null)
            {
                AudioMixer.SetFloat("MasterVolume", settingsConfigInfo.MasterVolume);
                AudioMixer.SetFloat("MusicVolume", settingsConfigInfo.MusicVolume);
                AudioMixer.SetFloat("SFXVolume", settingsConfigInfo.SFXVolume);
                AudioMixer.SetFloat("VoiceVolume", settingsConfigInfo.VoiceVolume);

                if (Subtitles != null) Subtitles.SetActive(settingsConfigInfo.Subtitles);
            }
        }
    }
}
