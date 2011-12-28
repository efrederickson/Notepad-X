/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/27/2011
 * Time: 3:23 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace NotepadX.Macros.Expansions.Plugin
{
    /// <summary>
    /// A Macro
    /// </summary>
    public class Macro
    {
        public Macro(string fn, string desc, string name)
        {
            Description = desc;
            Filename = fn;
            Name = name;
        }
        
        public string Description
        {get; set; }
        
        public string Filename
        {get; set; }
        
        public string Name
        {get; set; }
    }
}
