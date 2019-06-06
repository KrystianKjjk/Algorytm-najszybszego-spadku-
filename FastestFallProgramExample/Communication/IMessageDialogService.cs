namespace FastestFallProgramExample.Communication
{
    public interface IMessageDialogService
    {
        void ShowInfoDialog(string text);
        MessageDialogResult showOkCancelDialog(string text, string title);
    }
}