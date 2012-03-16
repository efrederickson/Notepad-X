/*
 * User: elijah
 * Date: 3/10/2012
 * Time: 1:48 PM
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using IExtendFramework;
using IExtendFramework.Text;

namespace NotepadX
{
    /// <summary>
    /// Contians file filter info, such as last used, most used
    /// </summary>
    public class FileFilterSettings
    {
        static readonly string SETTINGS_PATH = Application.LocalUserAppDataPath + "\\FILEHITS.stbl";
        
        /// <summary>
        /// The hashtable that stores the hits
        /// </summary>
        static Hashtable hits = new Hashtable();
        
        private FileFilterSettings()
        {
        }
        
        /// <summary>
        /// Adds 1 to the hit count of the extension
        /// </summary>
        /// <param name="ext"></param>
        public static void AddAHit(IFileExtension ext)
        {
            if (hits.ContainsKey(ext.Extension))
                hits[ext.Extension] = (int)hits[ext.Extension] + 1;
            else
                hits[ext.Extension] = 1;
        }
        
        /// <summary>
        /// Creates a filter string suitable for use in an WinForms OpenFileDialog
        /// </summary>
        /// <returns></returns>
        public static string ToFilter()
        {
            // This contains all of the Smart File Extensions Sorting logic.
            // it loops through the extensions, and counts down from the highest 
            // stored value. If the current extensions hits equals the current loop
            // highest value, it adds it to the filter string.
            // Then, it returns the filter.
            
            AdvancedString filter = "";
            int highest = GetHighestHit();
            List<string> added = new List<string>();
            while (true)
            {
                foreach (ITextEditor i in IExtendFramework.Text.FileExtensionManager.Editors)
                {
                    if (added.Contains(i.Extension.Extension))
                        continue;
                    int a = 0;
                    //if (hits.ContainsKey(i.Extension))
                    a = hits[i.Extension.Extension] == null ? 0 : (int)hits[i.Extension.Extension];
                    
                    if (a == highest)
                    {
                        filter += i.Extension.Extension + " file|*" + i.Extension.Extension + "|";
                        added.Add(i.Extension.Extension);
                    }
                }
                
                highest--;
                if (highest == -1)
                    break;
            }
            if (filter.EndsWith("|"))
                filter = filter.Substring(0, filter.Length - 1);
            return filter;
        }
        
        /// <summary>
        /// Returns this current highest hit count
        /// </summary>
        /// <returns></returns>
        static int GetHighestHit()
        {
            int max = 0;
            foreach (int i in hits.Values)
            {
                if (i > max)
                    max = i;
            }
            return max;
        }
        
        /// <summary>
        /// Loads from a serialized hashtable
        /// </summary>
        public static void Load()
        {
            if (File.Exists(SETTINGS_PATH))
                hits = Serializer.Deserialize<Hashtable>(SETTINGS_PATH);
        }
        
        /// <summary>
        /// Saves to a serialized hashtable
        /// </summary>
        public static void Save()
        {
            Serializer.Serialize(hits, SETTINGS_PATH);
        }
    }
}
