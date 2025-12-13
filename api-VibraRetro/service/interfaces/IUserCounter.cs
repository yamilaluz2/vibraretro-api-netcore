namespace api_VibraRetro.service.interfaces
{
    public interface IUserCounter
    {
        int Value { get; }
        int Increment();
        void Reset();
    }
}