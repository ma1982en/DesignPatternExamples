using PipelinePattern.Contracts;
using PipelinePattern.Steps;

namespace PipelinePattern;

public class ProcessorPipeline
{
    private readonly List<AStep> _steps = [];

    public ProcessorPipeline()
    {
        _steps.Add(new TextCleanerStep());
        _steps.Add(new WordCounterStep());
        _steps.Add(new SummarizerStep());
    }
    public string ExecutePipeline(string word)
    {
        object result = word;
        foreach (var pipelineStep in _steps)
        {
            result = pipelineStep.Execute(result);
        }
        return (string)result;
    }
}