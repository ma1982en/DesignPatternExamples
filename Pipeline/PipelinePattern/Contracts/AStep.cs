namespace PipelinePattern.Contracts;

public abstract class AStep 
{
    public abstract object Execute(object parameter);
}