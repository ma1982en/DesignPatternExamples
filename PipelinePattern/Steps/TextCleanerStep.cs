using PipelinePattern.Contracts;

namespace PipelinePattern.Steps;

public class TextCleanerStep : AStep 
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public override object Execute(object parameter)
    {
        var text = parameter as string;
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentException();
        }
        var cleanedText = new string(text
            .Where(c => !char.IsPunctuation(c))
            .ToArray());
        return cleanedText.ToLower();
    }
}