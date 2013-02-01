using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using JavaScript.Runtime.Util;
using JetBrains.Annotations;

namespace JavaScript.Runtime.Startup
{
    public static class ApplicationDefinitionLoader
    {
        [NotNull]
        public static ApplicationDefinition FromCommandLine([NotNull] CommandLineParameters commandLine)
        {
            Verify.ArgumentNotNull(commandLine, "commandLine");

            CommandLineParameters parameters;

            var pathType = GetPathType(commandLine);
            switch (pathType)
            {
                case ExecutablePathType.Js:
                    parameters = commandLine;
                    break;
                case ExecutablePathType.Jsr:
                    parameters = LoadParametersFromJsr(commandLine);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new ApplicationDefinition(
                ResolveFullPath(parameters.ExecutablePath),
                ApplicationProgramParameters.FromCommandLine(parameters),
                ApplicationRuntimeParameters.FromCommandLine(parameters));
        }

        #region implementation details

        private enum ExecutablePathType
        {
            Js,
            Jsr
        }

        [NotNull]
        private static CommandLineParameters LoadParametersFromJsr([NotNull] CommandLineParameters commandLine)
        {
            var jsrXmlPath = ResolveFullPath(commandLine.ExecutablePath);
            var jsrXml = XDocument.Load(jsrXmlPath);

            var jsrNode = jsrXml.RequiredElement("jsr");
            var runtimeNode = jsrNode.RequiredElement("runtime");
            var programNode = jsrNode.RequiredElement("program");
            var executablePath = programNode.RequiredAttribute("path");
            executablePath = GetPathRelativeToJsrPath(jsrXmlPath, executablePath);

            var programParameters = programNode.RequiredElement("arguments")
                                               .Elements()
                                               .Select(ExtractArgument)
                                               .ToDictionary(_ => _.Name);
            var runtimeParameters = new[]
                                        {
                                            ExtractLibraries(runtimeNode)
                                        }
                .ToDictionary(_ => _.Name);

            var loadedParameters = new CommandLineParameters(
                executablePath,
                runtimeParameters,
                programParameters);

            var mergedParameters = CommandLineParameters.Merge(
                commandLine,
                loadedParameters);

            return mergedParameters;
        }

        [NotNull]
        private static CommandLineParameter ExtractArgument([NotNull] XElement element)
        {
            bool isFlag;
            if (element.Name == "argument")
            {
                isFlag = false;
            }
            else if (element.Name == "flag")
            {
                isFlag = true;
            }
            else
            {
                throw JsrStartupThrowHelper.UnknownXmlElement(element);
            }

            var name = element.RequiredAttribute("name");

            string value = null;
            if (!isFlag)
            {
                var attribute = element.Attribute("value");
                value = attribute != null 
                    ? attribute.Value 
                    : element.Value;
            }

            return new CommandLineParameter(name, value);
        }

        [CanBeNull]
        private static CommandLineParameter ExtractLibraries([NotNull] XElement runtimeNode)
        {
            var librariesNode = runtimeNode.Element("libraries");
            if (librariesNode == null)
            {
                return null;
            }

            var libraryReferences =
                string.Join(
                    ";",
                    librariesNode
                        .Elements("library")
                        .Select(_ => _.RequiredAttribute("path"))
                    );

            return new CommandLineParameter(JsrCommandLine.LibrariesParameterName, libraryReferences);
        }

        private static ExecutablePathType GetPathType([NotNull] CommandLineParameters commandLine)
        {
            var ext = Path.GetExtension(commandLine.ExecutablePath)
                ?? string.Empty;

            switch (ext)
            {
                case ".js":
                    return ExecutablePathType.Js;
                case ".jsr":
                    return ExecutablePathType.Jsr;

                default:
                    throw JsrStartupThrowHelper.UnknownFileExtension(ext);
            }
        }

        [NotNull]
        private static string ResolveFullPath([NotNull] string path)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), path);
            fullPath = Path.GetFullPath(fullPath);
            return fullPath;
        }

        [NotNull]
        private static string GetPathRelativeToJsrPath([NotNull] string jsrPath, [NotNull] string path)
        {
            var directory = Path.GetDirectoryName(jsrPath);
            var fullPath = Path.Combine(directory, path);
            fullPath = Path.GetFullPath(fullPath);
            return fullPath;
        }

        #endregion
    }
}