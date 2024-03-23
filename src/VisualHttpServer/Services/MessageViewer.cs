using System.Windows;

namespace VisualHttpServer.Services;

internal class MessageViewer : IMessageViewer
{
    public void View(string caption, string text)
    {
        MessageBox.Show(text, caption);
    }
}