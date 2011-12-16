/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/15/2011
 * Time: 13:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace NotepadX.Plugins
{
    /// <summary>
    /// Description of StaticPluginInfo.
    /// </summary>
    public class StaticPluginInfo
    {
        private StaticPluginInfo()
        {
        }
        
        public static readonly string IPluginInterface = "NotepadX.Plugins.IPlugin";
        public static readonly string IMenuPluginInterface = "NotepadX.Plugins.IMenuPlugin";
    }
}
