using System;
using System.Xml;

namespace AutomationScriptConverter
{
    public class ScriptConverter
    {
        /// <summary>
        /// Converts the automation script by updating specified nodes and adding dependencies.
        /// </summary>
        /// <param name="filePath">The file path of the .pAutomation file.</param>
        /// <param name="oldMethodName">The old method name to be replaced.</param>
        /// <param name="newMethodName">The new method name to replace with.</param>
        /// <param name="automationSetId">The automation set ID to add as a dependency.</param>
        public void ConvertScript(string filePath, string oldMethodName, string newMethodName, string automationSetId)
        {
            // Load the XML document
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            // Add new dependency if not already present
            XmlNodeList dependenciesNodes = doc.GetElementsByTagName("Dependencies");
            bool dependencyExists = false;
            if (dependenciesNodes.Count > 0)
            {
                foreach (XmlNode node in dependenciesNodes[0].ChildNodes)
                {
                    if (node.Attributes?["Id"]?.Value == automationSetId)
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
}
