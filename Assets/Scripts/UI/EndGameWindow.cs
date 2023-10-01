using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public sealed class EndGameWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _time;
        
        public void Show(float time)
        {
            Time.timeScale = 0f;
            gameObject.SetActive(true);
            _time.text = TimeSpan.FromSeconds(time).ToString(@"mm\:ss");
        }
    }
}