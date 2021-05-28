using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class PulseBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform source;
    [SerializeField]
    private float pulseScale;
    [SerializeField] 
    private float pulseTime;
    
    public bool Pulse
    {
        get;
        set;
    }

    private bool _isPulsing = false;

    void Update()
    {
        if (Pulse && !_isPulsing)
        {
            DoPulseOut();
        }
    }

    private void DoPulseOut()
    {
        _isPulsing = true;
        source
            .DOScale(pulseScale, pulseTime/2)
            .OnComplete(DoPulseIn);
    }

    private void DoPulseIn()
    {
        source
            .DOScale(1, pulseTime/2)
            .OnComplete(() =>
            {
                if (!Pulse)
                {
                    _isPulsing = false;
                }
                else
                {
                    DoPulseOut();
                }
            });
    }

    
}
