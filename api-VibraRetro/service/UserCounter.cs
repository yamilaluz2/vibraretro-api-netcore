using api_VibraRetro.service.interfaces;

namespace api_VibraRetro.service
{
    public class UserCounter : IUserCounter
    {
        private int _value = 0;
        public int Value => _value;

        public int Increment()
        {
            _value++;
            return _value;
        }

        public void Reset() => _value = 0;
    }
}