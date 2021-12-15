using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileManager
{
    public static string ReadTextFile(string file_path)
    {
        StreamReader inp_stm = new StreamReader(file_path);
        string inp_ln = "";

        while(!inp_stm.EndOfStream)
            inp_ln += inp_stm.ReadLine() + "\n";

        inp_stm.Close();  
        return inp_ln;
    } 
}
