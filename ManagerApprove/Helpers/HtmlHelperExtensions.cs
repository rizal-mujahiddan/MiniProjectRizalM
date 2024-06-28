namespace ManagerApprove.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static string IsSelected(int currentValue, int optionValue) {
            return currentValue == optionValue ? "selected" : String.Empty;
        }
    }
}
