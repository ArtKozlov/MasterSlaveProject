
using System.Configuration;

namespace WcfUserServiceLibrary.Infostructure
{
    
    public class PortConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("ports")]
        public PortsCollection ServiceNodesItems
        {
            get { return (PortsCollection)base["ports"]; }
        }
    }

    [ConfigurationCollection(typeof(ServicePort))]
    public class PortsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ServicePort();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ServicePort)element).Port;
        }

        public ServicePort this[int indx]
        {
            get { return (ServicePort)BaseGet(indx); }
        }
    }

    public class ServicePort : ConfigurationElement
    {      
        [ConfigurationProperty("port", DefaultValue = 0, IsKey = true, IsRequired = true)]
        public int Port
        {
            get { return (int)base["port"]; }
            set { base["port"] = value; }
        }

        [ConfigurationProperty("ip", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Ip
        {
            get { return (string)base["ip"]; }
            set { base["ip"] = value; }
        }
    }

}