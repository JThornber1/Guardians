﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JT;

namespace JT
{
    public class AssignTransform : MonoBehaviour
    {
        public TransformVariable transformVariable;

		private void Awake()
		{
			transformVariable.value = this.transform;
			Destroy(this);
		}

	}
}
