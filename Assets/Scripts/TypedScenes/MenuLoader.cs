using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MenuLoader : MonoBehaviour
{
    [SerializeField] private int _menuSceneId;
    [SerializeField] private PlayerScore _playerScore;

    public void RunMenu()
    {
        StopAllCoroutines();

        if(_playerScore != null)
        {
            PlayerPrefs.SetInt("PlayerScore", _playerScore.Score);
            PlayerPrefs.Save();
        }

        SceneManager.LoadScene(_menuSceneId);
    }
}
