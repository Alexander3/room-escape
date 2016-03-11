using System;
using UnityEngine;

namespace Scripts
{
    class UnityControls : MonoBehaviour, IControls
    {
        private readonly string _HorizontalMoveAxis = "HorizontalMove";
        private readonly string _VerticalMoveAxis = "VerticalMove";

        private readonly string _HorizontalLook = "HorizontalLook";
        private readonly string _VerticalLook = "VerticalLook";


        private readonly string _ScrollWheel = "ScrollWheel";
        private readonly string _UseButton = "Use";
        private readonly string _JumpButton = "Jump";
        private readonly string _DropButton = "Drop";
        private readonly string _CancelButton = "Cancel";
        private readonly string _SneakButton = "Sneak";
        private readonly string _ResetViewButton = "Reset View";
        float IControls.HorizontalMove
        {
            get
            {
                return Input.GetAxis(_HorizontalMoveAxis);
            }
        }
        float IControls.VerticalMove
        {
            get
            {
                return Input.GetAxis(_VerticalMoveAxis);
            }
        }

        float IControls.HorizontalHead
        {
            get { throw new NotImplementedException(); }
        }

        float IControls.VerticalHead
        {
            get { throw new NotImplementedException(); }
        }

        float IControls.ScrollWheel
        {
            get { return Input.GetAxis(_ScrollWheel); }
        }

        bool IControls.Use
        {
            get { return Input.GetButtonUp(_UseButton); }
        }

        bool IControls.Jump
        {
            get { return Input.GetButtonUp(_JumpButton); }
        }

        bool IControls.Drop
        {
            get { return Input.GetButtonUp(_DropButton); }
        }

        bool IControls.Cancel
        {
            get { return Input.GetButtonUp(_CancelButton); }
        }

        float IControls.HorizontalLook
        {
            get { return Input.GetAxis(_HorizontalLook); }
        }

        float IControls.VerticalLook
        {
            get { return Input.GetAxis(_VerticalLook); }
        }

        float IControls.MouseAbsX
        {
            get { return Input.mousePosition.x; }
        }

        float IControls.MouseAbsY
        {
            get { return Input.mousePosition.y; }
        }

        bool IControls.Sneaking
        {
            get { return Input.GetButton(_SneakButton); }
        }

        bool IControls.ResetView
        {
            get { return Input.GetButton(_ResetViewButton); }
        }
    }
}
