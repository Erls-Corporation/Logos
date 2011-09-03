namespace Logos.UI.ViewModels
{
    public sealed class TagViewModel
    {
        readonly string _name;

        public TagViewModel(string name)
        {
            _name = name;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }
    }
}