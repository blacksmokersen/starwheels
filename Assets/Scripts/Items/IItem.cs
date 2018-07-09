namespace Items
{
    public interface IItem
    {
        void UseForward();
        void UseBackward();
        void Stack();
        int Count { get; set; }
        bool Instantiable { get; set; }
    }
}
