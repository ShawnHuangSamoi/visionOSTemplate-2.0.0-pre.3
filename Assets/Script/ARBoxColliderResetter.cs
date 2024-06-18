using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Samoi.AR.Samples
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(ARPlane))]
    public class ARBoxColliderResetter : MonoBehaviour
    {
        [SerializeField]
        private BoxCollider m_BoxCollider;
        [SerializeField]
        private ARPlane m_ARPlane;
        [SerializeField]
        private float m_ColliderThickness = 0.1f;
        
        private void OnEnable()
        {
            m_ARPlane.boundaryChanged += OnBoundaryChanged;
        }
        
        private void OnDisable()
        {
            m_ARPlane.boundaryChanged -= OnBoundaryChanged;
        }
        
        private void OnBoundaryChanged(ARPlaneBoundaryChangedEventArgs eventArgs)
        {
            if (m_BoxCollider != null)
            {
                Destroy(m_BoxCollider);
            }
            
            StartCoroutine(SettleCollider());
        }
        
        private IEnumerator SettleCollider()
        {
            m_BoxCollider = gameObject.AddComponent<BoxCollider>();
            yield return null;
            m_BoxCollider.size = new Vector3(m_ARPlane.size.x, m_ColliderThickness, m_ARPlane.size.y);
        }
    }
}