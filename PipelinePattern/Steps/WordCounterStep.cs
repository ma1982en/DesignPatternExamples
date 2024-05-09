using PipelinePattern.Contracts;

namespace PipelinePattern.Steps;

public class WordCounterStep : AStep 
{
    public override object Execute(object parameter)
    {
        var text = parameter as string;
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentException();
        }
        var words = text.Split(' ');
        var wordFrequency = new Dictionary<string, int>();
        foreach (var word in words)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                continue;
            }

            if (wordFrequency.ContainsKey(word))
            {
                wordFrequency[word]++;
            }
            else
            {
                wordFrequency[word] = 1;
            }
        }

        return wordFrequency;
    }
}