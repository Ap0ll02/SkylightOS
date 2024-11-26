using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
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
    Transform parentInitial = FindObjectOfType<FileSystem>().GetComponent<Transform>();
    public List<Inode> inodeTable = new();
    // Awake initializes entire filsystem
    public void Awake() {
        // [0]
        inodeTable.Add(MkFile("home", parentSpawn: parentInitial, ftype: Inode.FileOptions.dir));
        // [1-3] home Children
        inodeTable.Add(MkFile("User", parent: inodeTable[0], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Perry", parent: inodeTable[0], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Dorothy", parent: inodeTable[0], ftype: Inode.FileOptions.dir));

        // [4-5] User Children
        inodeTable.Add(MkFile("Settings", parent: inodeTable[1], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Themes", parent: inodeTable[1], ftype: Inode.FileOptions.dir));

        // [6-9] Settings Children
        inodeTable.Add(MkFile("Cursors", parent: inodeTable[4], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Languages", parent: inodeTable[4], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Keyboard", parent: inodeTable[4], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Network", parent: inodeTable[4], ftype: Inode.FileOptions.dir));
        // [10-13] Network Children
        inodeTable.Add(MkFile("Servers", parent: inodeTable[9], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Wi-Fi", parent: inodeTable[9], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Ethernet", parent: inodeTable[9], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Advanced", parent: inodeTable[9], ftype: Inode.FileOptions.dir));

        // [14-16] Perry Children
        inodeTable.Add(MkFile("Music", parent: inodeTable[2], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Private", parent: inodeTable[2], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Documents", parent: inodeTable[2], ftype: Inode.FileOptions.dir));

        // [17-18] Music Children
        inodeTable.Add(MkFile("Classical", parent: inodeTable[14], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Dubstep", parent: inodeTable[14], ftype: Inode.FileOptions.dir));
        // [19-21] Private Children
        inodeTable.Add(MkFile("Taxes", parent: inodeTable[15], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Venture Ideas", parent: inodeTable[15], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Company", parent: inodeTable[15], ftype: Inode.FileOptions.dir));
        // [22-23] Documents Children
        inodeTable.Add(MkFile("Passwords", parent: inodeTable[16], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Invoices", parent: inodeTable[16], ftype: Inode.FileOptions.dir));

        // [24] Classical Children 
        inodeTable.Add(MkFile("Beethoven's 9th Symphony.music", parent: inodeTable[17], ftype: Inode.FileOptions.file));
        // [25] Dubstep Children
        inodeTable.Add(MkFile("Nebula's Bleed Too.music", parent: inodeTable[18], ftype: Inode.FileOptions.file));

        // [26-27] Taxes Children
        inodeTable.Add(MkFile("2024-Year-Review.ss", parent: inodeTable[19], ftype: Inode.FileOptions.file));
        inodeTable.Add(MkFile("2007-Year-Review.ss", parent: inodeTable[19], ftype: Inode.FileOptions.file));
        // [28] Venture Ideas Children
        inodeTable.Add(MkFile("OceanFloorOS.text", parent: inodeTable[20], ftype: Inode.FileOptions.file));
        // [29-32] Company Children
        inodeTable.Add(MkFile("Cool Evidence Piece.png", parent: inodeTable[21], ftype: Inode.FileOptions.file));
        inodeTable.Add(MkFile("Employee Evaluations 2012.ss", parent: inodeTable[21], ftype: Inode.FileOptions.file));
        inodeTable.Add(MkFile("Profits.ss", parent: inodeTable[21], ftype: Inode.FileOptions.file));
        inodeTable.Add(MkFile("New Launch Features.text", parent: inodeTable[21], ftype: Inode.FileOptions.file));

        // [33-36] Passwords Children
        inodeTable.Add(MkFile("Countries.text", parent: inodeTable[22], ftype: Inode.FileOptions.file));
        inodeTable.Add(MkFile("Rock.text", parent: inodeTable[22], ftype: Inode.FileOptions.file));
        inodeTable.Add(MkFile("Computers.text", parent: inodeTable[22], ftype: Inode.FileOptions.file));
        inodeTable.Add(MkFile("Math.text", parent: inodeTable[22], ftype: Inode.FileOptions.file));
        // [37] Invoices Children
        inodeTable.Add(MkFile("You Owe Me.ss", parent: inodeTable[23], ftype: Inode.FileOptions.file));

        // [38-41] Dorothy's Children
        inodeTable.Add(MkFile("Photos", parent: inodeTable[3], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Videos", parent: inodeTable[3], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Downloads", parent: inodeTable[3], ftype: Inode.FileOptions.dir));  
        inodeTable.Add(MkFile("TextEditor", parent: inodeTable[3], ftype: Inode.FileOptions.dir));

        // TODO: [41-43] Photos Children
        inodeTable.Add(MkFile("Receipt.jpeg", parent: inodeTable[38], ftype: Inode.FileOptions.file));
        inodeTable.Add(MkFile("computer.png", parent: inodeTable[38], ftype: Inode.FileOptions.file));
        inodeTable.Add(MkFile("ml64sw.png", parent: inodeTable[38], ftype: Inode.FileOptions.file));
        // [44] Videos Children
        inodeTable.Add(MkFile("James Birthday.flv", parent: inodeTable[39], ftype: Inode.FileOptions.file));
        // [45-46] Downloads Children
        inodeTable.Add(MkFile("Special", parent: inodeTable[40], ftype: Inode.FileOptions.dir));
        inodeTable.Add(MkFile("Recent", parent: inodeTable[40], ftype: Inode.FileOptions.dir));
        // [47] TextEditor's Children
        inodeTable.Add(MkFile("Settings.oml", parent: inodeTable[41], ftype: Inode.FileOptions.file));

        // [48-50] Special's Children
        inodeTable.Add(MkFile("better_browser.install", parent: inodeTable[45], ftype: Inode.FileOptions.file));
        inodeTable.Add(MkFile("spruce_email.install", parent: inodeTable[45], ftype: Inode.FileOptions.file));
        inodeTable.Add(MkFile("Mist.install", parent: inodeTable[45], ftype: Inode.FileOptions.file));
        // [51-54] Recent's Children
        inodeTable.Add(MkFile("Antibacterial.install", parent: inodeTable[46], ftype: Inode.FileOptions.file));
        inodeTable.Add(MkFile("Antifungal.install", parent: inodeTable[46], ftype: Inode.FileOptions.file));
        inodeTable.Add(MkFile("Antivirus.install", parent: inodeTable[46], ftype: Inode.FileOptions.file));
        inodeTable.Add(MkFile("Antiparasite.install", parent: inodeTable[46], ftype: Inode.FileOptions.file));

        foreach(var file in inodeTable) {
            foreach(var cfile in inodeTable) {
                if (cfile.iParent == file) {
                    file.iChildren.Add(cfile);
                    file.numEntries = file.iChildren.Count;
                }
            }
        }
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

    public Inode DelFile(Inode i) {
        return null;
    }
}
