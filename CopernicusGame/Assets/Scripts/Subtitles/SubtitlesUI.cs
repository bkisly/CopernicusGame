using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SubtitlesUI : MonoBehaviour
{
    public Subtitle[] Subtitles;
    public Text SubtitlesText;

    private float _timer = 0f;
    private int _subtitlesIndex = 0;

    private Subtitle _currentSubtitle { get { return Subtitles[_subtitlesIndex]; } }

    private void Start()
    {
        SubtitlesText.text = "";
    }

    private void Update()
    {
        print(_timer);

        if (_subtitlesIndex < Subtitles.Length)
        {
            if (_timer >= _currentSubtitle.StartTime) SubtitlesText.text = _currentSubtitle.Text;

            if (_timer >= _currentSubtitle.EndTime)
            {
                SubtitlesText.text = "";
                _subtitlesIndex++;
            }

            _timer += Time.deltaTime;
        }
    }
}
