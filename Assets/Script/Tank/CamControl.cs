using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamControl : MonoBehaviour
{
    public enum Type
    {
        FirstPerson = 0,
        ThridPerson = 1
    };

    static CinemachineVirtualCamera firstPersonCam;
    static CinemachineFreeLook thridPersonCam;

    public static bool firstPersonActive = false;

    public static bool isFirstPersonActive()
    {
        return firstPersonActive;
    }

    public static void SwitchCamera()
    {
        if (firstPersonActive)
            toThridPerson();
        else
            toFirstPerson();
    }

    public static void SwitchCamera(Type type)
    {
        if (type == Type.ThridPerson)
            toThridPerson();
        else
            toFirstPerson();
    }

    public static void RegisterFirstPerson(CinemachineVirtualCamera camera)
    {
        firstPersonCam = camera;
    }

    public static void RegisterThridPerson(CinemachineFreeLook camera)
    {
        thridPersonCam = camera;
    }

    private static void toFirstPerson()
    {
        Camera.main.cullingMask &= ~(1 << 7); //remove layer 7
        firstPersonCam.Priority = 10;
        thridPersonCam.Priority = 0;
        firstPersonActive = true;
    }

    private static void toThridPerson()
    {
        Camera.main.cullingMask |= (1 << 7); //add layer 7
        firstPersonCam.Priority = 0;
        thridPersonCam.Priority = 10;
        firstPersonActive = false;
    }
}
