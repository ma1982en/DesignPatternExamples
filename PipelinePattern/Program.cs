using PipelinePattern.Steps;

namespace PipelinePattern;

class Program
{
    static void Main(string[] args)
    {
        var inputText = "Your input input text here.";
        var pipelineProcessor = new ProcessorPipeline();
        var text = pipelineProcessor.ExecutePipeline(inputText);
        Console.WriteLine(text);
    }
}