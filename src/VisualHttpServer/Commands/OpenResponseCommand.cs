using System.Windows.Input;
using Microsoft.Win32;
using VisualHttpServer.Model;
using VisualHttpServer.Services;

namespace VisualHttpServer.Commands;

internal class OpenResponseCommand(IMessageViewer messageViewer) : ICommand
{
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public async void Execute(object? parameter)
    {
        if (parameter is not RouteUi route)
        {
            return;
        }

        var dialog = new OpenFileDialog
        {
            DefaultExt = ".json",
            Filter = "JSON files (.json)|*.json"
        };

        if (dialog.ShowDialog() == true)
        {
            var fileName = dialog.FileName;
            try
            {
                await route.Response.ReadFromFileAsync(fileName);
            }
            catch
            {
                messageViewer.View("Error open response", $"Can't open file '{fileName}'.");
            }
        }
    }

    public event EventHandler? CanExecuteChanged;
}