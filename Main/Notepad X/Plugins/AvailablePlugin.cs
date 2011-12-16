using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace NotepadX.Plugins
{
    public class AvailablePlugins : System.Collections.CollectionBase
    {
        //A Simple class to hold some info about our Available Plugins

        /// <summary>
        /// Add a Plugin to the collection of Available plugins
        /// </summary>
        /// <param name="pluginToAdd">The Plugin to Add</param>
        public void Add(AvailablePlugin pluginToAdd)
        {
            this.List.Add(pluginToAdd);
        }

        /// <summary>
        /// Gets the AssemblyPath from the plugin from the collection at id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string _Get(int id)
        {
            return ((AvailablePlugin)this.List[id]).AssemblyPath;
        }

        /// <summary>
        /// Remove a Plugin to the collection of available plugins
        /// </summary>
        /// <param name="pluginToRemove">The Plugin to Remove</param>
        public void Remove(AvailablePlugin pluginToRemove)
        {
            this.List.Remove(pluginToRemove);
            pluginToRemove = null;
        }

        /// <summary>
        /// Checks if pluginNameOrPath exists in the collection of available plugins
        /// </summary>
        /// <param name="pluginNameOrPath">The Name/Path of the plugin to find</param>
        /// <returns>True if found, false if not found</returns>
        /// <remarks></remarks>
        public bool Exist(string pluginNameOrPath)
        {
            foreach (AvailablePlugin pluginOn in this.List) {
                try {
                    if (((pluginOn.Instance.Name.ToLower().Equals(pluginNameOrPath.ToLower())) | pluginOn.AssemblyPath.ToLower().Equals(pluginNameOrPath.ToLower()))) {
                        return true;
                    }
                } catch {
                }
            }
            return false;
        }

        /// <summary>
        /// Finds a plugin in the available Plugins
        /// </summary>
        /// <param name="pluginNameOrPath">The name or File path of the plugin to find</param>
        /// <returns>Available Plugin, or nothing if the plugin is not found</returns>
        public AvailablePlugin Find(string pluginNameOrPath)
        {
            //Loop through all the plugins
            foreach (AvailablePlugin pluginOn in this.List) {
                try {
                    //Find the one with the matching name or filename
                    if (((pluginOn.Instance.Name.Equals(pluginNameOrPath)) | pluginOn.AssemblyPath.Equals(pluginNameOrPath))) {
                        return pluginOn;
                    }
                } catch {
                }
            }
            return null;
        }
    }

    /// <summary>
    /// Data Class for Available Plugin.  Holds and instance of the loaded Plugin, as well as the Plugins Assembly Path
    /// </summary>
    public class AvailablePlugin
    {
        //This is the actual AvailablePlugin object..
        //Holds an instance of the plugin to access
        //Also holds assembly path
        private IPlugin myInstance = null;

        private string myAssemblyPath = "";
        /// <summary>
        /// The running Instance of this plugin
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public IPlugin Instance {
            get { return myInstance; }
            set { myInstance = value; }
        }
        
        public IMenuPlugin MenuItem
        {get; set; }

        /// <summary>
        /// The filename of this plugin
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string AssemblyPath {
            get { return myAssemblyPath; }
            set { myAssemblyPath = value; }
        }
    }
}
