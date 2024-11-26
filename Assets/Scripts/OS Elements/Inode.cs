using System.Collections.Generic;
using UnityEngine;

public class Inode : MonoBehaviour
{
    // File type enumerator
    public enum FileOptions {
        file,
        dir,
        other
    }
    /// @var iType the type of file the inode is {file, dir, other}
    public FileOptions iType;

    /// @var iName the name of the inode
    public string iName;
    /// @var iParent the gameobject that is the inode's parent.
    public Inode iParent;
    /// @var numEntries The number of children in the inode
    public int numEntries = 0; 

    public List<Inode> iChildren = new();

    public void Start() {

    }

}
