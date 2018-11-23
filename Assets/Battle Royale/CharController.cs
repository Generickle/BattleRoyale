using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial1{
    public class CharController : MonoBehaviour
    {
        Transform transform1;
        Rigidbody rigidbody1;
        Animator animator1;

        public Transform cameraShoulder;
        public Transform cameraHolder;
        private Transform camera1;

        private float rotX = 0f;
        private float rotY = 0f;

        public float minAngle = -70;
        public float maxAngle = 90;

        public float speed = 200;
        public float rotationSpeed = 25;
        public float cameraSpeed = 24;

        private Vector2 newSpeed;

        // Use this for initialization
        void Start()
        {
            transform1 = this.transform;
            rigidbody1 = GetComponent<Rigidbody>();
            animator1 = GetComponentInChildren<Animator>();
            camera1 = Camera.main.transform;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            PlayerControl();
            CameraControl();
            AnimationControl();
        }

        private void AnimationControl()
        {
            animator1.SetFloat("X", newSpeed.x);
            animator1.SetFloat("Y", newSpeed.y);
        }

        private void CameraControl()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            float deltaT = Time.deltaTime;

            rotY += mouseY * deltaT * rotationSpeed;

            //Giros de derecha a izquierda
            float xRot = mouseX * deltaT * rotationSpeed;
            transform1.Rotate(0, xRot, 0);

            //Giros de arriba hacia abajo
            rotY = Mathf.Clamp(rotY, minAngle, maxAngle);

            Quaternion localRotation = Quaternion.Euler(-rotY, 0, 0);
            cameraShoulder.localRotation = localRotation;

            camera1.position = Vector3.Lerp(camera1.position, cameraHolder.position, cameraSpeed * deltaT);
            camera1.rotation = Quaternion.Lerp(camera1.rotation, cameraHolder.rotation, cameraSpeed * deltaT);
        }

        private void PlayerControl()
        {
            Vector3 velocity = rigidbody1.velocity;

            float deltaX = Input.GetAxis("Horizontal");
            float deltaZ = Input.GetAxis("Vertical");
            newSpeed = new Vector2(deltaX, deltaZ);
            float deltaT = Time.deltaTime;

            Vector3 side = speed * deltaX * deltaT * transform1.right;
            Vector3 forward = speed * deltaZ * deltaT * transform1.forward;
            Vector3 endSpeed = side + forward;

            rigidbody1.velocity = endSpeed;

        }
    }

}