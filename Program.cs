using System;
using System.IO;
using System.Xml;

namespace AutomationScriptConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 4)
            {
                Console.WriteLine("Usage: AutomationScriptConverter <FilePath> <OldMethodName> <NewMethodName> <AutomationSetId>");
                return;
            }

            string filePath = args[0];
            string oldMethodName = args[1];
            string newMethodName = args[2];
            string automationSetId = args[3];

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);

                // Add Dependency
                XmlNode? dependenciesNode = doc.SelectSingleNode("//Dependencies");
                if (dependenciesNode != null)
                {
                    XmlElement newDependency = doc.CreateElement("Dependency");
                    newDependency.SetAttribute("Id", automationSetId);
                    newDependency.SetAttribute("IsUsed", "True");
                    dependenciesNode.AppendChild(newDependency);
                }

                // Update InstanceName, DisplayName, and ConnectionBlock
                XmlNodeList? instanceNodes = doc.SelectNodes($"//ConnectionBlock[InstanceName/@Value='{oldMethodName}']");
                if (instanceNodes != null)
                {
                    foreach (XmlNode node in instanceNodes)
                    {
                        XmlNode? instanceNameNode = node.SelectSingleNode("InstanceName");
                        if (instanceNameNode?.Attributes?["Value"] != null)
                        {
                            instanceNameNode.Attributes["Value"].Value = "RA_Common";
                        }

                        XmlNode? displayNameNode = node.SelectSingleNode("DisplayName");
                        if (displayNameNode?.Attributes?["Value"] != null)
                        {
                            displayNameNode.Attributes["Value"].Value = newMethodName;
                        }

                        // Remove attributes from ConnectionBlock
                        node.Attributes?.RemoveAll();
                    }
                }

                // Update ComponentName, DisplayName, InstanceTypeName, etc.
                XmlNodeList? componentNodes = doc.SelectNodes($"//OpenSpan.Automation.ConnectableMethod[ComponentName/@Value='{oldMethodName}']");
                if (componentNodes != null)
                {
                    foreach (XmlNode node in componentNodes)
                    {
                        XmlNode? componentNameNode = node.SelectSingleNode("ComponentName");
                        if (componentNameNode?.Attributes?["Value"] != null)
                        {
                            componentNameNode.Attributes["Value"].Value = "RA_Common";
                        }

                        XmlNode? displayNameNode = node.SelectSingleNode("DisplayName");
                        if (displayNameNode?.Attributes?["Value"] != null)
                        {
                            displayNameNode.Attributes["Value"].Value = newMethodName;
                        }

                        XmlNode? instanceTypeNameNode = node.SelectSingleNode("InstanceTypeName");
                        if (instanceTypeNameNode?.Attributes?["Value"] != null)
                        {
                            instanceTypeNameNode.Attributes["Value"].Value = "OpenSpan.Automation.AutomatorSet";
                        }

                        XmlNode? instanceUniqueIdNode = node.SelectSingleNode("InstanceUniqueId");
                        if (instanceUniqueIdNode?.Attributes?["Value"] != null)
                        {
                            string? instanceUniqueId = instanceUniqueIdNode.Attributes["Value"].Value;
                            if (!string.IsNullOrEmpty(instanceUniqueId))
                            {
                                XmlElement entryPointAutomationId = doc.CreateElement("EntryPointAutomationId");
                                entryPointAutomationId.SetAttribute("Value", instanceUniqueId);
                                // Insert EntryPointAutomationId before <ExceptionsHandled>
                                XmlNode? exceptionsHandledNode = node.SelectSingleNode("ExceptionsHandled");
                                if (exceptionsHandledNode != null)
                                {
                                    node.InsertBefore(entryPointAutomationId, exceptionsHandledNode);
                                }
                            }

                            instanceUniqueIdNode.Attributes["Value"].Value = automationSetId;
                        }

                        XmlNode? memberDetailsNode = node.SelectSingleNode("MemberDetails");
                        if (memberDetailsNode?.Attributes?["Value"] != null)
                        {
                            memberDetailsNode.Attributes["Value"].Value = $".{newMethodName}() Method";
                        }

                        XmlNode? memberNameNode = node.SelectSingleNode("Content/Items/OpenSpan.Automation.MemberPrototype/MemberName");
                        if (memberNameNode?.Attributes?["Value"] != null)
                        {
                            memberNameNode.Attributes["Value"].Value = newMethodName;
                        }
                    }
                }

                doc.Save(filePath);
                Console.WriteLine("File updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
