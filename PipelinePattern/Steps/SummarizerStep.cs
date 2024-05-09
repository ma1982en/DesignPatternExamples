using PipelinePattern.Contracts;

namespace PipelinePattern.Steps;

public class SummarizerStep : AStep
{
    public override object Execute(object parameter)
    {
        var wordFrequency = parameter as Dictionary<string, int>;
        if (wordFrequency == null || wordFrequency.Count == 0)
        {
            throw new ArgumentException();
        }
        var topWords = wordFrequency
            .OrderByDescending(kvp => kvp.Value)
            .Take(3)
            .Select(kvp => kvp.Key);
        return $"Top words: {string.Join(", ", topWords)}";
    }
}