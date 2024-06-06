using System;
using System.Xml;

class Program
{
    static void Main(string[] args)
    {
        // Ensure the correct number of arguments are provided
        if (args.Length != 4)
        {
            Console.WriteLine("Usage: dotnet run <FilePath> <OldMethodName> <NewMethodName> <AutomationSetId>");
            return;
        }

        // Assign arguments to variables
        string filePath = args[0];
        string oldMethodName = args[1];
        string newMethodName = args[2];
        string automationSetId = args[3];

        // Load the XML document
        XmlDocument doc = new XmlDocument();
        doc.Load(filePath);

        // Add new dependency if it doesn't already exist
        XmlNodeList dependenciesNodes = doc.GetElementsByTagName("Dependencies");
        if (dependenciesNodes.Count > 0)
        {
            bool dependencyExists = false;

            foreach (XmlNode dependency in dependenciesNodes[0].ChildNodes)
            {
                if (dependency.Attributes != null && dependency.Attributes["Id"]?.Value == automationSetId)
                {
                    dependencyExists = true;
                    break;
                }
            }

            if (!dependencyExists)
            {
                XmlElement newDependency = doc.CreateElement("Dependency");
                newDependency.SetAttribute("Id", automationSetId);
                newDependency.SetAttribute("IsUsed", "True");
                dependenciesNodes[0].AppendChild(newDependency);
            }
        }

        // Update InstanceName, DisplayName, and ConnectionBlock
        XmlNodeList instanceNodes = doc.SelectNodes($"//ConnectionBlock[InstanceName/@Value='{oldMethodName}']");
        if (instanceNodes != null)
        {
            foreach (XmlNode node in instanceNodes)
            {
                XmlNode instanceNameNode = node.SelectSingleNode("InstanceName");
                if (instanceNameNode != null && instanceNameNode.Attributes["Value"] != null)
                {
                    instanceNameNode.Attributes["Value"].Value = "RA_Common";
                }

                XmlNode displayNameNode = node.SelectSingleNode("DisplayName");
                if (displayNameNode != null && displayNameNode.Attributes["Value"] != null)
                {
                    displayNameNode.Attributes["Value"].Value = newMethodName;
                }

                if (node.Attributes != null)
                {
                    node.Attributes.RemoveAll();
                }
            }
        }

        // Update ComponentName, DisplayName, InstanceTypeName, etc.
        XmlNodeList componentNodes = doc.SelectNodes($"//OpenSpan.Automation.ConnectableMethod[ComponentName/@Value='{oldMethodName}']");
        if (componentNodes != null)
        {
            foreach (XmlNode node in componentNodes)
            {
                XmlNode componentNameNode = node.SelectSingleNode("ComponentName");
                if (componentNameNode != null && componentNameNode.Attributes["Value"] != null)
                {
                    componentNameNode.Attributes["Value"].Value = "RA_Common";
                }

                XmlNode displayNameNode = node.SelectSingleNode("DisplayName");
                if (displayNameNode != null && displayNameNode.Attributes["Value"] != null)
                {
                    displayNameNode.Attributes["Value"].Value = newMethodName;
                }

                XmlNode instanceTypeNameNode = node.SelectSingleNode("InstanceTypeName");
                if (instanceTypeNameNode != null && instanceTypeNameNode.Attributes["Value"] != null)
                {
                    instanceTypeNameNode.Attributes["Value"].Value = "OpenSpan.Automation.AutomatorSet";
                }

                XmlNode instanceUniqueIdNode = node.SelectSingleNode("InstanceUniqueId");
                if (instanceUniqueIdNode != null && instanceUniqueIdNode.Attributes["Value"] != null)
                {
                    string instanceUniqueId = instanceUniqueIdNode.Attributes["Value"].Value;
                    if (!string.IsNullOrEmpty(instanceUniqueId))
                    {
                        XmlElement entryPointAutomationId = doc.CreateElement("EntryPointAutomationId");
                        entryPointAutomationId.SetAttribute("Value", instanceUniqueId);
                        XmlNode exceptionsHandledNode = node.SelectSingleNode("ExceptionsHandled");
                        if (exceptionsHandledNode != null)
                        {
                            node.InsertBefore(entryPointAutomationId, exceptionsHandledNode);
                        }
                    }

                    instanceUniqueIdNode.Attributes["Value"].Value = automationSetId;
                }

                XmlNode memberDetailsNode = node.SelectSingleNode("MemberDetails");
                if (memberDetailsNode != null && memberDetailsNode.Attributes["Value"] != null)
                {
                    memberDetailsNode.Attributes["Value"].Value = $".{newMethodName}() Method";
                }

                XmlNode memberNameNode = node.SelectSingleNode("Content/Items/OpenSpan.Automation.MemberPrototype/MemberName");
                if (memberNameNode != null && memberNameNode.Attributes["Value"] != null)
                {
                    memberNameNode.Attributes["Value"].Value = newMethodName;
                }
            }
        }

        // Save the updated XML document
        doc.Save(filePath);

        Console.WriteLine("Automation script updated successfully.");
    }
}
