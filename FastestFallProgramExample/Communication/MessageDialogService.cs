using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FastestFallProgramExample.Communication
{
    public class MessageDialogService : IMessageDialogService
    {
        public void ShowInfoDialog(string text)
        {
            MessageBox.Show(text, "informacja");
        }
        public MessageDialogResult showOkCancelDialog(string title, string text)
        {
            var result = MessageBox.Show(text, title, MessageBoxButton.OKCancel);
            return result == MessageBoxResult.OK 
                ? MessageDialogResult.OK 
                : MessageDialogResult.Cancel;
        }
    }
    public enum MessageDialogResult
    {
        Cancel,
        OK
    }
}
