using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creator: Jack Ratermann
/// This class manages the entire inode based filesystem. Mainly for creating files and their children + parent relations.
/// Depends on inode and FileSystem gameobject in hierarchy.
/// Creating and Deleting Files is not super supported yet, creating is possible but deleting is not.
/// </summary>
public class FileSystem : MonoBehaviour
{
    [SerializeField] GameObject inode;
    Transform parentInitial;
    public List<Inode> inodeTable = new();
    public List<List<Inode>> inodeChildrenManagerList = new();
    // Awake initializes entire filsystem
    public void Awake() {
        parentInitial = FindObjectOfType<FileSystem>().GetComponent<Transform>();
        // [0]
        inodeTable.Add(MkFile("home", parentSpawn: parentInitial, ftype: Inode.FileOptions.dir));
        // [1-3] home Children
        inodeTable.Add(MkFile("User", parentSpawn: inodeTable[0].gameObject.transform, parent: inodeTable[0], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Perry", parentSpawn: inodeTable[0].gameObject.transform, parent: inodeTable[0], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Dorothy", parentSpawn: inodeTable[0].gameObject.transform, parent: inodeTable[0], ftype: Inode.FileOptions.dir));

        // [4-5] User Children
        inodeTable.Add(MkFile("Settings", parentSpawn: inodeTable[1].gameObject.transform, parent: inodeTable[1], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Themes", parentSpawn: inodeTable[1].gameObject.transform, parent: inodeTable[1], ftype: Inode.FileOptions.dir));

        // [6-9] Settings Children
        inodeTable.Add(MkFile("Cursors", parentSpawn: inodeTable[4].gameObject.transform, parent: inodeTable[4], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Languages", parentSpawn: inodeTable[4].gameObject.transform, parent: inodeTable[4], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Keyboard", parentSpawn: inodeTable[4].gameObject.transform, parent: inodeTable[4], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Network", parentSpawn: inodeTable[4].gameObject.transform, parent: inodeTable[4], ftype: Inode.FileOptions.dir));
        // [10-13] Network Children
        inodeTable.Add(MkFile("Servers", parentSpawn: inodeTable[9].gameObject.transform, parent: inodeTable[9], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Wi-Fi", parentSpawn: inodeTable[9].gameObject.transform, parent: inodeTable[9], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Ethernet", parentSpawn: inodeTable[9].gameObject.transform, parent: inodeTable[9], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Advanced", parentSpawn: inodeTable[9].gameObject.transform, parent: inodeTable[9], ftype: Inode.FileOptions.dir));

        // [14-16] Perry Children
        inodeTable.Add(MkFile("Music", parentSpawn: inodeTable[2].gameObject.transform, parent: inodeTable[2], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Private", parentSpawn: inodeTable[2].gameObject.transform, parent: inodeTable[2], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Documents", parentSpawn: inodeTable[2].gameObject.transform, parent: inodeTable[2], ftype: Inode.FileOptions.dir));

        // [17-18] Music Children
        inodeTable.Add(MkFile("Classical", parentSpawn: inodeTable[14].gameObject.transform, parent: inodeTable[14], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Dubstep", parentSpawn: inodeTable[14].gameObject.transform, parent: inodeTable[14], ftype: Inode.FileOptions.dir));
        // [19-21] Private Children
        inodeTable.Add(MkFile("Taxes", parentSpawn: inodeTable[15].gameObject.transform, parent: inodeTable[15], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Venture Ideas", parentSpawn: inodeTable[15].gameObject.transform, parent: inodeTable[15], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Company", parentSpawn: inodeTable[15].gameObject.transform, parent: inodeTable[15], ftype: Inode.FileOptions.dir));
        // [22-23] Documents Children
        inodeTable.Add(MkFile("Passwords", parentSpawn: inodeTable[16].gameObject.transform, parent: inodeTable[16], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Invoices", parentSpawn: inodeTable[16].gameObject.transform, parent: inodeTable[16], ftype: Inode.FileOptions.dir));

        // [24] Classical Children 
        inodeTable.Add(MkFile("Beethoven's 9th Symphony.music", parentSpawn: inodeTable[17].gameObject.transform, parent: inodeTable[17], ftype: Inode.FileOptions.file));
        // [25] Dubstep Children
        inodeTable.Add(MkFile("Nebula's Bleed Too.music", parentSpawn: inodeTable[18].gameObject.transform, parent: inodeTable[18], ftype: Inode.FileOptions.file));

        // [26-27] Taxes Children
        inodeTable.Add(MkFile("2024-Year-Review.ss", parentSpawn: inodeTable[19].gameObject.transform, parent: inodeTable[19], ftype: Inode.FileOptions.file));
        inodeTable.Add(MkFile("2007-Year-Review.ss", parentSpawn: inodeTable[19].gameObject.transform, parent: inodeTable[19], ftype: Inode.FileOptions.file));
        // [28] Venture Ideas Children
        inodeTable.Add(MkFile("OceanFloorOS.text", parentSpawn: inodeTable[20].gameObject.transform, parent: inodeTable[20], ftype: Inode.FileOptions.file));
        // [29-32] Company Children
        inodeTable.Add(MkFile("Cool Evidence Piece.png", parentSpawn: inodeTable[21].gameObject.transform, parent: inodeTable[21], ftype: Inode.FileOptions.file));
        inodeTable.Add(MkFile("Employee Evaluations 2012.ss", parentSpawn: inodeTable[21].gameObject.transform, parent: inodeTable[21], ftype: Inode.FileOptions.file));
        inodeTable.Add(MkFile("Profits.ss", parentSpawn: inodeTable[21].gameObject.transform, parent: inodeTable[21], ftype: Inode.FileOptions.file));
        inodeTable.Add(MkFile("New Launch Features.text", parentSpawn: inodeTable[21].gameObject.transform, parent: inodeTable[21], ftype: Inode.FileOptions.file));

        // [33-36] Passwords Children
        inodeTable.Add(MkFile("Countries.text", parentSpawn: inodeTable[22].gameObject.transform, parent: inodeTable[22], ftype: Inode.FileOptions.file));
        inodeTable.Add(MkFile("Rock.text", parentSpawn: inodeTable[22].gameObject.transform, parent: inodeTable[22], ftype: Inode.FileOptions.file));
        inodeTable.Add(MkFile("Computers.text", parentSpawn: inodeTable[22].gameObject.transform, parent: inodeTable[22], ftype: Inode.FileOptions.file));
        inodeTable.Add(MkFile("Math.text", parentSpawn: inodeTable[22].gameObject.transform, parent: inodeTable[22], ftype: Inode.FileOptions.file));
        // [37] Invoices Children
        inodeTable.Add(MkFile("You Owe Me.ss", parentSpawn: inodeTable[23].gameObject.transform, parent: inodeTable[23], ftype: Inode.FileOptions.file));

        // [38-41] Dorothy's Children
        inodeTable.Add(MkFile("Photos", parentSpawn: inodeTable[3].gameObject.transform, parent: inodeTable[3], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Videos", parentSpawn: inodeTable[3].gameObject.transform, parent: inodeTable[3], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Downloads", parentSpawn: inodeTable[3].gameObject.transform, parent: inodeTable[3], ftype: Inode.FileOptions.dir));  
        inodeTable.Add(MkFile("TextEditor", parentSpawn: inodeTable[3].gameObject.transform, parent: inodeTable[3], ftype: Inode.FileOptions.dir));

        // [41-43] Photos Children
        inodeTable.Add(MkFile("Receipt.jpeg", parentSpawn: inodeTable[38].gameObject.transform, parent: inodeTable[38], ftype: Inode.FileOptions.file));
        inodeTable.Add(MkFile("computer.png", parentSpawn: inodeTable[38].gameObject.transform, parent: inodeTable[38], ftype: Inode.FileOptions.file));
        inodeTable.Add(MkFile("ml64sw.png", parentSpawn: inodeTable[38].gameObject.transform, parent: inodeTable[38], ftype: Inode.FileOptions.file));
        // [44] Videos Children
        inodeTable.Add(MkFile("James Birthday.flv", parentSpawn: inodeTable[39].gameObject.transform, parent: inodeTable[39], ftype: Inode.FileOptions.file));
        // [45-46] Downloads Children
        inodeTable.Add(MkFile("Special", parentSpawn: inodeTable[40].gameObject.transform, parent: inodeTable[40], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Recent", parentSpawn: inodeTable[40].gameObject.transform, parent: inodeTable[40], ftype: Inode.FileOptions.dir));
        // [47] TextEditor's Children
        inodeTable.Add(MkFile("Settings.oml", parentSpawn: inodeTable[41].gameObject.transform, parent: inodeTable[41], ftype: Inode.FileOptions.file));

        // [48-50] Special's Children
        inodeTable.Add(MkFile("better_browser.install", parentSpawn: inodeTable[45].gameObject.transform, parent: inodeTable[45], ftype: Inode.FileOptions.file));
        inodeTable.Add(MkFile("spruce_email.install", parentSpawn: inodeTable[45].gameObject.transform, parent: inodeTable[45], ftype: Inode.FileOptions.file));
        inodeTable.Add(MkFile("Mist.install", parentSpawn: inodeTable[45].gameObject.transform, parent: inodeTable[45], ftype: Inode.FileOptions.file));
        // [51-54] Recent's Children
        inodeTable.Add(MkFile("Antibacterial.install", parentSpawn: inodeTable[46].gameObject.transform, parent: inodeTable[46], ftype: Inode.FileOptions.file));
        inodeTable.Add(MkFile("Antifungal.install", parentSpawn: inodeTable[46].gameObject.transform, parent: inodeTable[46], ftype: Inode.FileOptions.file));
        inodeTable.Add(MkFile("Antivirus.install", parentSpawn: inodeTable[46].gameObject.transform, parent: inodeTable[46], ftype: Inode.FileOptions.file));
        inodeTable.Add(MkFile("Antiparasite.install", parentSpawn: inodeTable[46].gameObject.transform, parent: inodeTable[46], ftype: Inode.FileOptions.file));

        foreach(var file in inodeTable) {
            foreach(var cfile in inodeTable) {
                if (cfile.iParent == file) {
                    file.iChildren.Add(cfile);
                    file.numEntries = file.iChildren.Count;
                }
            }
        }
    }
    public Inode MkFile(string name = "newFileMade", Transform parentSpawn = null, Inode parent = null, 
    Inode.FileOptions ftype = Inode.FileOptions.file, List<Inode> children = null) {
        //inodeChildrenManagerList[chListCounter];
        GameObject fileObj = Instantiate(inode, parent: parentSpawn);
        fileObj.name = "inode " + name;
        Inode file = fileObj.GetComponent<Inode>();
        file.iName = name;
        file.iType = ftype;
        // Consider making parent itself.
        file.iParent = parent;
        file.iChildren = IncList(inodeChildrenManagerList);
        file.numEntries = 0;
        return file;
    }

    public List<Inode> IncList(List<List<Inode>> l) {
        l.Add(new List<Inode>());
        return l[^1];
    }

    public Inode DelFile(Inode i) {
        return null;
    }
}
