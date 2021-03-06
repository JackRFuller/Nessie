﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CharacterFieldOfView))]
public class CharacterFieldOfViewEditor : Editor
{
   private void OnSceneGUI()
   {
       CharacterFieldOfView fov = (CharacterFieldOfView)target;

       Handles.color = Color.white;
       Handles.DrawWireArc(fov.transform.position,Vector3.up, Vector3.forward, 360, fov.ViewRadius);

       Vector3 viewAngleA = fov.DirFromAngle(-fov.ViewAngle * 0.5f, false);
       Vector3 viewAngleB = fov.DirFromAngle(fov.ViewAngle * 0.5f,false);

       Handles.DrawLine(fov.transform.position,fov.transform.position + viewAngleA * fov.ViewRadius);
       Handles.DrawLine(fov.transform.position,fov.transform.position + viewAngleB * fov.ViewRadius);
   }
}
