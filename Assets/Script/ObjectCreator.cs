using DG.Tweening;
using Unity.PolySpatial.InputDevices;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.LowLevel;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace Samoi.AR.Samples
{
    public class ObjectCreator : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_ObjectToSpawn;
        [SerializeField]
        private float m_SpawnDuration = 0.5f;

        private void OnEnable()
        {
            // enable enhanced touch support to use active touches for properly pooling input phases
            EnhancedTouchSupport.Enable();
        }

        private void Update()
        {
            var activeTouches = Touch.activeTouches;

            if (activeTouches.Count > 0)
            {
                var primaryTouchData = EnhancedSpatialPointerSupport.GetPointerState(activeTouches[0]);
                if (activeTouches[0].phase == TouchPhase.Began)
                {
                    // allow balloons to be popped with a poke or indirect pinch
                    if (primaryTouchData.Kind == SpatialPointerKind.IndirectPinch)
                    {
                        var touchPosition = primaryTouchData.interactionPosition;
                        SpawnObject(touchPosition);
                    }
                }
            }
        }
        
        private void SpawnObject(Vector3 position)
        {
            var spawnedObj = Instantiate(m_ObjectToSpawn, position, Quaternion.identity);
            var objectScale = spawnedObj.transform.localScale;
            spawnedObj.transform.localScale = Vector3.zero;
            spawnedObj.transform.DOScale(objectScale, m_SpawnDuration).SetEase(Ease.OutBounce);
        }
    }
}
