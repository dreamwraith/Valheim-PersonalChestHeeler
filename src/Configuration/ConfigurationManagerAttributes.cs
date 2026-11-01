using System;
using BepInEx.Configuration;

namespace PersonalChestHeeler.Configuration
{
    /// <summary>
    /// ConfigurationManager integration attribute definition.
    /// BepInEx ConfigurationManager discovers this class via reflection on ConfigDescription.Tags.
    /// </summary>
    public class ConfigurationManagerAttributes
    {
        public string? DispName { get; set; }
        public bool? IsAdvanced { get; set; }
        public int? Order { get; set; }
        public bool? Browsable { get; set; }
        public bool? ReadOnly { get; set; }
        public Action<ConfigEntryBase>? CustomDrawer { get; set; }
    }
}
