using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class Address : MonoBehaviour
{
    public static Address Instance;
    
    public Dictionary<Specify, GameObject> MembersName => _membersName;
    
    [SerializeField] private List<AddressProperties> Member = new List<AddressProperties>();
    private Dictionary<Specify, GameObject> _membersName = new Dictionary<Specify, GameObject>();
    
    public enum Specify
    {
        XRInteractionManager,
    }
    
    private void Awake()
    {
        Instance = this;
        
        foreach (AddressProperties member in Member)
        {
            MembersName.Add(member.type, member.value);
        }
    }
}

[Serializable]
public class AddressProperties
{
    public Address.Specify type;
    public GameObject value;
}