using System.Configuration;

namespace ServiceLogic.DumpToXml
{

    public class DumpConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("directories")]
        public PathCollection GetPathItems
        {
            get { return (PathCollection) base["directories"]; }
        }
    }

    [ConfigurationCollection(typeof(DumpPath))]
    public class PathCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new DumpPath();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DumpPath) element).Path;
        }
        public DumpPath this[int indx]
        {
            get { return (DumpPath)BaseGet(indx); }
        }

    }

    public class DumpPath : ConfigurationElement
    {
        [ConfigurationProperty("path", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Path
        {
            get { return (string) base["path"]; }
            set { base["path"] = value; }
        }
    }
}
