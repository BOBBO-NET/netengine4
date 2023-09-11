namespace BobboNet.Knowledge
{
    public interface IKnowledgeCase
    {
        string GetName();
        bool IsCase(IKnowledge knowledge);
    }
}