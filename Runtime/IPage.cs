namespace Game.UI
{
    public interface IPage
    {
        string Name { get; }
        bool IsVisible { get; }
        void Show();
        void Hide();
    }
}