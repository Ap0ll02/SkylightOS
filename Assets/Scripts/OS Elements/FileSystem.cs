using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FileSystem : MonoBehaviour
{
    [SerializeField] GameObject inode;
    Transform parentInitial = FindObjectOfType<FileSystem>().GetComponent<Transform>();
    public List<Inode> inodeTable = new();
    public void Start() {
        inodeTable.Add(MkFile("home", parentSpawn: parentInitial, ftype: Inode.FileOptions.dir));
        
        MkFile();
    }

    public Inode MkFile(string name = "newFileMade", Transform parentSpawn = null, Inode parent = null, Inode.FileOptions ftype = Inode.FileOptions.file, List<Inode> children = null) {
        GameObject fileObj = Instantiate(inode, parent: parentSpawn);
        Inode file = fileObj.GetComponent<Inode>();
        file.iName = name;
        file.iType = ftype;
        // Consider making parent itself.
        file.iParent = parent;
        file.iChildren = children;
        file.numEntries = file.iChildren.Count;

        return file;
    }
}
