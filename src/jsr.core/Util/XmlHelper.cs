using System.Xml.Linq;
using JavaScript.Runtime.Startup;
using JetBrains.Annotations;

namespace JavaScript.Runtime.Util
{
    public static class XmlHelper
    {
        [NotNull]
        public static XElement RequiredElement([NotNull] this XContainer container, [NotNull] XName name)
        {
            var node = container.Element(name);
            if (node == null)
            {
                throw JsrStartupThrowHelper.XmlElementNotFound(container, name);
            }

            return node;
        }

        [NotNull]
        public static string RequiredAttribute([NotNull] this XElement element, [NotNull] XName name)
        {
            var attribute = element.Attribute(name);
            if (attribute == null)
            {
                throw JsrStartupThrowHelper.XmlAttributeNotFound(element, name);
            }

            return attribute.Value;
        }
    }
}