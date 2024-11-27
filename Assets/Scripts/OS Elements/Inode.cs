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
    /// @var iChildren a list of all children inodes. Essentially the inodetable wihtin the inodetable
    public List<Inode> iChildren = new();

    public List<Data> iData = new();

    /// @brief Shows files from the specified, and includes the children.
    // NOTE: This DOES NOT recursively show children
    public void ShowFile(Inode i) {
        i.gameObject.SetActive(true);
        foreach (var file in i.iChildren)
        {
            file.gameObject.SetActive(true);
        }
    }

    /// @brief Hides files from the specified file, and includes the children.
    // NOTE: This DOES NOT recursively hide children
    public void HideFile(Inode i) {
        foreach (var file in i.iChildren)
        {
            file.gameObject.SetActive(false);
        }
        i.gameObject.SetActive(false);
    }
}