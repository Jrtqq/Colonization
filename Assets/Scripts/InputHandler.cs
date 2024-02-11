using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private LayerMask _planeMask;

    private float _maxClickDistance = 500;

    private Vector3 _baseSearchRadius = new(3.75f, 0.5f, 3.75f);

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (TryGetBaseOnClick(out Base @base))
            {

            }
        }
    }

    private bool TryGetBaseOnClick(out Base @base)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit, _maxClickDistance, _planeMask))
        {
            Collider[] colliders = Physics.OverlapBox(hit.point, _baseSearchRadius);

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out @base))
                    return true;
            }
        }

        @base = null;
        return false;
    }
}
