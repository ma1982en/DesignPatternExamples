using Microsoft.VisualStudio.TestTools.UnitTesting;
using PipelinePattern.Steps;

namespace PipelinePattern.Tests;

[TestClass]
public class TextCleanerStepTests
{
    [TestMethod]
    public void Execute_input_string_expect_string()
    {
        //arrange
        var input = "remove !.;";
        var expect = "remove ";

        //act
        var textCleanerStep = new TextCleanerStep();
        var result = textCleanerStep.Execute(input);
        var text = result as string;
        
        //assert
        Assert.IsNotNull(text);
        Assert.AreEqual(expect,text);
    }
}