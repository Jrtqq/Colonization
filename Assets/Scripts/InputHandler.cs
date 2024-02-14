using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private LayerMask _planeMask;
    [SerializeField] private Material _planeMaterial; //для подсвечивания, если база выбрана
    [SerializeField] private Flag _flagPrefab;

    private Color _defaultColor = Color.white;
    private Color _highlightedColor = new(0.6f, 0.9f, 0.4f);

    private float _maxClickDistance = 500;
    private Vector3 _baseSearchRadius = new(3.75f, 0.5f, 3.75f);

    private Base _selectedBase = null;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_selectedBase == null && TryGetBaseOnClick(out _selectedBase))
                _planeMaterial.color = _highlightedColor;

            else if (_selectedBase != null && TryGetBaseOnClick(out Base _) == false)
            {
                _planeMaterial.color = _defaultColor;

                if (TryGetClickPoint(out Vector3 point))
                {
                    Flag flag = Instantiate(_flagPrefab, point, Quaternion.identity);
                    _selectedBase.SetFlag(flag);
                }

                _selectedBase = null;
            }
        }
    }

    private bool TryGetBaseOnClick(out Base @base)
    {
        if (TryGetClickPoint(out Vector3 point))
        {
            Collider[] colliders = Physics.OverlapBox(point, _baseSearchRadius);

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out @base))
                    return true;
            }
        }

        @base = null;
        return false;
    }

    private bool TryGetClickPoint(out Vector3 point)
    {
        point = Vector3.zero;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _maxClickDistance, _planeMask))
        {
            point = hit.point;
            return true;
        }

        return false;
    }
}
