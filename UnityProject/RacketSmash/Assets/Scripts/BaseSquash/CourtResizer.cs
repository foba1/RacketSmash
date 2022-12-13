using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseSquash
{
    public class CourtResizer : MonoBehaviour
    {
        [SerializeField] float xSize = 10;
        [SerializeField] float ySize = 10;
        [SerializeField] float zSize = 10;
        [SerializeField] float thickness = 0.5f;

        [Header("References")]
        [SerializeField] Transform ground;
        [SerializeField] Transform top;
        [SerializeField] Transform left;
        [SerializeField] Transform right;
        [SerializeField] Transform front;
        [SerializeField] Transform back;

        [SerializeField] bool doValidate = false;
        private void OnValidate()
        {
            if (!doValidate)
                return;
            ground.position = new Vector3(0, -thickness / 2, 0);
            ground.localScale = new Vector3(xSize + 2 * thickness, thickness, zSize + 2 * thickness);
            top.position = new Vector3(0, ySize - thickness / 2, 0);
            top.localScale = new Vector3(xSize + 2 * thickness, thickness, zSize + 2 * thickness);

            left.position = new Vector3(-(xSize + thickness) / 2, ySize / 2, 0);
            left.localScale = new Vector3(thickness, ySize, zSize + 2 * thickness);
            right.position = new Vector3((xSize + thickness) / 2, ySize / 2, 0);
            right.localScale = new Vector3(thickness, ySize, zSize + 2 * thickness);

            front.position = new Vector3(-1.87f, 4.009f, 3.162f);
            front.localScale = new Vector3(0.025f, 0.025f, 0.05f);
            if (back.gameObject.activeSelf)
            {
                back.position = new Vector3(0, ySize / 2, -(zSize + thickness) / 2);
                back.localScale = new Vector3(xSize, ySize, thickness);
            }
        }
    }

}
