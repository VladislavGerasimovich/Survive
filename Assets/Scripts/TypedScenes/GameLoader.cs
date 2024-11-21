using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TypedScenes
{
    public class GameLoader : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private int _gameSceneId;

        private float _divider;

        private void Awake()
        {
            _divider = 0.9f;
        }

        private void Start()
        {
            StartCoroutine(RunGame());
        }

        private IEnumerator RunGame()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(_gameSceneId);

            while (operation.isDone == false)
            {
                float progress = operation.progress / _divider;
                _slider.value = progress;

                yield return null;
            }
        }
    }
}