using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class CreateToonSelection : MonoBehaviour
{
    public void SelectThisToon()
    {
        HLManager.Instance.DisableCreateToonListHL();
    }
}
