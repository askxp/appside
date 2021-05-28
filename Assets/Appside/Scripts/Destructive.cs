using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Appside.Scripts
{
    public class Destructive : MonoBehaviour, IDestructive
    {
        public Action<IDestructive> onDestroyed = delegate {  };
    
        private Material[] _materials;
        private Collider[] _colliders;
        private static readonly int EdgeWidth = Shader.PropertyToID("_EdgeWidth");
        private static readonly int Step = Shader.PropertyToID("_Step");
        private bool _isDestroying;


        private void Awake()
        {
            MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
            List<Material> materials = new List<Material>();
            foreach (var r in renderers)
            {
                materials.AddRange(r.materials);
            }
            _materials = materials.ToArray();
            _colliders = GetComponentsInChildren<Collider>();
        }

        private IEnumerator Start()
        {
            //HOTFIX:
            yield return new WaitForEndOfFrame();
            LevelManager.Get().RegisterObject(this);
        }

        private IEnumerator Destroy()
        {
            _isDestroying = true;
            Hide();
            yield return new WaitForSeconds(1f);
            DisableCollider();
            onDestroyed(this);
            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
        }

        private void LaunchDestruction()
        {
            StartCoroutine(nameof(Destroy));
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Explosion"))
            {
                LaunchDestruction();
            }
        }
    
        private void DisableCollider()
        {
            foreach (Collider c in _colliders)
            {
                c.enabled = false;
            }
        }

        private void Hide()
        {
            for (int i = 0; i < _materials.Length; i++)
            {
                float edgeWidth = _materials[i].GetFloat(EdgeWidth);
                _materials[i].DOFloat(1+edgeWidth, Step, 2f);
            }
        }

        // private void Show()
        // {
        //     _isDestroying = false;
        //     for (int i = 0; i < _materials.Length; i++)
        //     {
        //         float edgeWidth = _materials[i].GetFloat(EdgeWidth);
        //         _materials[i].DOFloat(-edgeWidth, Step, 2f);
        //     }
        // }

        private void OnCollisionEnter(Collision other)
        {
            if(_isDestroying) return;
            if (other.gameObject.CompareTag("Player") || 
                other.gameObject.CompareTag("Explosion"))
            {
                LaunchDestruction();
            }
        }

    }

    public interface IDestructive
    {
    
    }
}