using System;
using Cysharp.Threading.Tasks;
using Data.UnityObjects;
using Inputs;
using UnityEngine;

namespace Controllers
{
    public class GunController : MonoBehaviour
    {
        [SerializeField] private GunDataScriptable gunData;
        
        private InputHandler _inputHandler;
        private bool _canBaseShoot;

        private void Awake()
        {
            _inputHandler = new InputHandler();
        }

        private void Update()
        {
            BaseShoot().Forget();
        }

        private async UniTaskVoid BaseShoot()
        {
            if (_inputHandler.GetAttackInput())
            {
                await UniTask.Delay(TimeSpan.FromSeconds(gunData.GunData.BaseAttackFireRate));
                Debug.Log("Attacink Pow Pow Dıkş DIkş");
            }
        }
    }
}
