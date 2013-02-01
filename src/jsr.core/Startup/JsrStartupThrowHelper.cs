using System.Xml;
using System.Xml.Linq;
using JetBrains.Annotations;

namespace JavaScript.Runtime.Startup
{
    public static class JsrStartupThrowHelper
    {
        public static JsrStartupException XmlElementNotFound([NotNull] XContainer container, [NotNull] XName name)
        {
            var message = string.Format(
                "Required XML element \"{0}\" not found at ({1}, {2})",
                name,
                ((IXmlLineInfo) container).LineNumber,
                ((IXmlLineInfo) container).LinePosition);

            return new JsrStartupException(message);
        }

        public static JsrStartupException XmlAttributeNotFound([NotNull] XElement element, [NotNull] XName name)
        {
            var message = string.Format(
                "Required XML attrubite \"{0}\" not found in element \"{1}\" at ({2}, {3})",
                name,
                element.Name,
                ((IXmlLineInfo)element).LineNumber,
                ((IXmlLineInfo)element).LinePosition);

            return new JsrStartupException(message);
        }

        public static JsrStartupException UnknownXmlElement([NotNull] XElement element)
        {
            var message = string.Format(
                "Unexpected XML element \"{0}\" at ({1}, {2})",
                element.Name,
                ((IXmlLineInfo)element).LineNumber,
                ((IXmlLineInfo)element).LinePosition);

            return new JsrStartupException(message);
        }

        public static JsrStartupException UnknownFileExtension(string extension)
        {
            var message = string.Format("File extension \"{0}\" is unknown", extension);
            return new JsrStartupException(message);
        }

        public static JsrStartupException UnableToGetApplicationDirectory()
        {
            return new JsrStartupException("Unable to get application directory path");
        }

        public static JsrStartupException UnableToGetBinariesDirectory()
        {
            return new JsrStartupException("Unable to get binaries directory path");
        }
    }
}