namespace TexasHoldemCalculator.Interfaces.Messages
{
    public abstract class Message<T>
    {
        public T Data { get; set; }


        protected Message()
        {
            this.Data = default(T);
        }

        protected Message(T message)
        {
            this.Data = message;
        }
    }
}