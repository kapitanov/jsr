using NUnit.Framework;

namespace JavaScript.Runtime.Test
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class CommandLineParserTest
    {
        [Test]
        [TestCase("")]
        [TestCase("   ")]
        public void Parse_empty_string(string input)
        {
            var result = CommandLineParser.Parse(input);

            Assert.IsNullOrEmpty(result.ExecutablePath);
            Assert.IsEmpty(result.RuntimeParameters);
            Assert.IsEmpty(result.ProgramParameters);
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        [TestCase("exe")]
        [TestCase("exe  ")]
        [TestCase("  exe")]
        [TestCase("  exe  ")]
        public void Parse_exe_path_only(string input)
        {
            var result = CommandLineParser.Parse(input);

            Assert.AreEqual("exe", result.ExecutablePath);
            Assert.IsEmpty(result.RuntimeParameters);
            Assert.IsEmpty(result.ProgramParameters);
            Assert.IsTrue(result.IsValid);
        }

        [Test]
        [TestCase("/x=1 /y=abc /z exe")]
        [TestCase("/x=1  /y=abc /z exe")]
        [TestCase(" /x=1 /y=abc /z exe")]
        [TestCase("/x=1 /y=abc /z  exe")]
        [TestCase("/x=1 /y=abc /z exe ")]
        public void Parse_exe_path_and_runtime_params(string input)
        {
            var result = CommandLineParser.Parse(input);

            Assert.AreEqual("exe", result.ExecutablePath);
            Assert.AreEqual(3, result.RuntimeParameters.Count);
            Assert.IsTrue(result.RuntimeParameters["x"].HasValue);
            Assert.AreEqual("1", result.RuntimeParameters["x"].Value);
            Assert.IsTrue(result.RuntimeParameters["y"].HasValue);
            Assert.AreEqual("abc", result.RuntimeParameters["y"].Value);
            Assert.IsFalse(result.RuntimeParameters["z"].HasValue);

            Assert.IsEmpty(result.ProgramParameters);

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        [TestCase("exe /x=1 /y=abc /z")]
        [TestCase(" exe /x=1 /y=abc /z")]
        [TestCase("exe  /x=1 /y=abc /z")]
        [TestCase("exe /x=1 /y=abc  /z")]
        [TestCase("exe /x=1 /y=abc /z ")]
        public void Parse_exe_path_and_program_params(string input)
        {
            var result = CommandLineParser.Parse(input);

            Assert.AreEqual("exe", result.ExecutablePath);

            Assert.AreEqual(3, result.ProgramParameters.Count);
            Assert.IsTrue(result.ProgramParameters["x"].HasValue);
            Assert.AreEqual("1", result.ProgramParameters["x"].Value);
            Assert.IsTrue(result.ProgramParameters["y"].HasValue);
            Assert.AreEqual("abc", result.ProgramParameters["y"].Value);
            Assert.IsFalse(result.ProgramParameters["z"].HasValue);

            Assert.IsEmpty(result.RuntimeParameters);

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        [TestCase("/x=1 /y=abc /z exe /x=1 /y=abc /z")]
        [TestCase("/x=1 /y=abc /z  exe /x=1 /y=abc /z")]
        [TestCase("/x=1 /y=abc /z exe  /x=1 /y=abc /z")]
        [TestCase(" /x=1 /y=abc /z exe /x=1 /y=abc /z")]
        [TestCase("/x=1 /y=abc /z exe /x=1 /y=abc /z ")]
        [TestCase("/x=1 /y=abc  /z exe /x=1 /y=abc /z")]
        [TestCase("/x=1 /y=abc  /z exe /x=1  /y=abc /z")]
        public void Parse_exe_path_and_all_params(string input)
        {
            var result = CommandLineParser.Parse(input);

            Assert.AreEqual("exe", result.ExecutablePath);

            Assert.AreEqual("exe", result.ExecutablePath);
            Assert.AreEqual(3, result.RuntimeParameters.Count);
            Assert.IsTrue(result.RuntimeParameters["x"].HasValue);
            Assert.AreEqual("1", result.RuntimeParameters["x"].Value);
            Assert.IsTrue(result.RuntimeParameters["y"].HasValue);
            Assert.AreEqual("abc", result.RuntimeParameters["y"].Value);
            Assert.IsFalse(result.RuntimeParameters["z"].HasValue);

            Assert.AreEqual(3, result.ProgramParameters.Count);
            Assert.IsTrue(result.ProgramParameters["x"].HasValue);
            Assert.AreEqual("1", result.ProgramParameters["x"].Value);
            Assert.IsTrue(result.ProgramParameters["y"].HasValue);
            Assert.AreEqual("abc", result.ProgramParameters["y"].Value);
            Assert.IsFalse(result.ProgramParameters["z"].HasValue);
          
            Assert.IsTrue(result.IsValid);
        }

        [Test]
        [TestCase("/x=\"\" /y=\"abc\" exe /x=\"\" /y=\"abc\"")]
        public void Parse_exe_path_and_all_params_with_quoted_values(string input)
        {
            var result = CommandLineParser.Parse(input);

            Assert.AreEqual("exe", result.ExecutablePath);

            Assert.AreEqual("exe", result.ExecutablePath);
            Assert.AreEqual(2, result.RuntimeParameters.Count);
            Assert.IsTrue(result.RuntimeParameters["x"].HasValue);
            Assert.AreEqual("", result.RuntimeParameters["x"].Value);
            Assert.IsTrue(result.RuntimeParameters["y"].HasValue);
            Assert.AreEqual("abc", result.RuntimeParameters["y"].Value);
            
            Assert.AreEqual(2, result.ProgramParameters.Count);
            Assert.IsTrue(result.ProgramParameters["x"].HasValue);
            Assert.AreEqual("", result.ProgramParameters["x"].Value);
            Assert.IsTrue(result.ProgramParameters["y"].HasValue);
            Assert.AreEqual("abc", result.ProgramParameters["y"].Value);
            
            Assert.IsTrue(result.IsValid);
        }
    }
}
