﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Werewolf.StatusIndicators.Services;

namespace Werewolf.StatusIndicators.Components
{
    public class Cone: SpellIndicator
    {
        // Constants

        public const float CONE_ANIM_SPEED = 40f;

        public Transform LBord;

        public Transform RBord;

        public List<Splat> Splats = new List<Splat>();

        [SerializeField]
        [Range(0, 360)]
        private float angle;

        public float Angle
        {
            get
            {
                return angle;
            }
            set
            {
                this.angle = value;
                SetAngle(value);
            }
        }

        // Methods

        public override void Update()
        {
            this.transform.rotation =
                    Quaternion.LookRotation(FlattenVector(SplatManager.Get3DMousePosition()) - this.transform.position);
        }

        private void SetAngle(float angle)
        {
            foreach (var splat in this.Splats)
            {
                if (null != splat)
                    splat.SetShaderFloat("_Expand", Normalize.GetValue(angle - 1, 360));
            }

            if (this.LBord == null || this.RBord == null)
            {
                return;
            }

            this.LBord.localEulerAngles = new Vector3(0, -(angle / 2), 0);
            this.RBord.localEulerAngles = new Vector3(0, (angle / 2), 0);
        }
    }
}