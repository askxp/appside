using System;
using System.Collections;
using System.Collections.Generic;
using Appside.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressVisualisation : MonoBehaviour
{
     [SerializeField] private Text counter;
     [SerializeField] private PulseBehaviour icon;
     [SerializeField] private PulseBehaviour winText;
     private void Start()
     {
          LevelManager.Get().onProgress += ShowProgress;
          LevelManager.Get().onComplete += () => { StartCoroutine(nameof(ShowWinText));};
     }

     private void ShowProgress(int current, int total)
     {
          counter.text = string.Format("{0}/{1}", current, total);
          StartCoroutine(nameof(AnimateIcon));
     }

     private IEnumerator AnimateIcon()
     {
          icon.Pulse = true;
          yield return new WaitForSeconds(1f);
          icon.Pulse = false;
     }

     private IEnumerator ShowWinText()
     {
          winText.gameObject.SetActive(true);
          winText.Pulse = true;
          icon.gameObject.SetActive(false);
          counter.gameObject.SetActive(false);
          yield return new WaitForSeconds(1f);
          winText.Pulse = false;
     }
}
