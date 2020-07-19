using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Shared.Level
{
    public class UIManager : MonoBehaviour
    {
        public GameObject VictoryPanel;
        public GameObject TryAgainPanel;
        public GameObject ThanksForPlayingPanel;

        public void ShowThanksForPlayingPanel()
        {
            ThanksForPlayingPanel.SetActive(true);
        }

        public void ShowTryAgainPanel()
        {
            TryAgainPanel.SetActive(true);
        }

        public void ShowVictoryPanel(string nextLevel)
        {
            var subtitleText = VictoryPanel
                .GetComponentsInChildren<Text>()
                .First(text => text.gameObject.name.Equals("Subtitle"));

            subtitleText.text = $"Proceed to Level {nextLevel}";

            VictoryPanel.SetActive(true);
        }
    }
}
