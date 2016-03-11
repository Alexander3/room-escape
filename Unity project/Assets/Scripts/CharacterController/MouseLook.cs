using Assets;
using Scripts;
using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [Serializable]
    public class MouseLook
    {
        public float XSensitivity = 2f;
        public float YSensitivity = 2f;
        public bool ClampVerticalRotation = true;
        public float MinimumX = -90F;
        public float MaximumX = 90F;
        public bool Smooth;
        public float SmoothSpeed = 5f;


        private Quaternion _CharacterTargetRot;
        private Quaternion _CameraTargetRot;
        private IControls _Controls;


        public void Init(Transform character, Transform camera)
        {
            _CharacterTargetRot = character.localRotation;
            _CameraTargetRot = camera.localRotation;
            _Controls = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<IControls>();
        }


        public void LookRotation(Transform character, Transform camera)
        {
            float yRot = _Controls.HorizontalLook * XSensitivity;
            float xRot = _Controls.VerticalLook * YSensitivity;

            _CharacterTargetRot *= Quaternion.Euler (0f, yRot, 0f);
            _CameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);

            if(ClampVerticalRotation)
                _CameraTargetRot = ClampRotationAroundXAxis (_CameraTargetRot);

            if(Smooth)
            {
                character.localRotation = Quaternion.Slerp (character.localRotation, _CharacterTargetRot,
                    SmoothSpeed * Time.deltaTime);
                camera.localRotation = Quaternion.Slerp (camera.localRotation, _CameraTargetRot,
                    SmoothSpeed * Time.deltaTime);
            }
            else
            {
                character.localRotation = _CharacterTargetRot;
                camera.localRotation = _CameraTargetRot;
            }
        }


        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

            angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

            q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }

    }
}
