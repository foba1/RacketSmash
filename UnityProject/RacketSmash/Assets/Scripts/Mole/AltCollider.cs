using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WhackAMole
{
    public class AltCollider : MonoBehaviour
    {
        [SerializeField] private GameObject parent;
        private MoleBase mole;

        // Start is called before the first frame update
        void Start()
        {
            mole= parent.GetComponent<MoleBase>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            mole.collisionEnter(collision);
        }


    }
}

