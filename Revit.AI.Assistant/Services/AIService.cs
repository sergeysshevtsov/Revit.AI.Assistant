using Autodesk.Revit.UI;
using Python.Runtime;

namespace Revit.AI.Assistant.Services;
internal class AIService
{
    public AIService()
    {
        string pythonHome = @"C:\Python312"; //setup path to the python312
        string pythonDll = System.IO.Path.Combine(pythonHome, "python312.dll");

        Environment.SetEnvironmentVariable("PYTHONHOME", pythonHome, EnvironmentVariableTarget.Process);
        Environment.SetEnvironmentVariable("PYTHONNET_PYDLL", pythonDll, EnvironmentVariableTarget.Process);

        try
        {
            Runtime.PythonDLL = pythonDll;
        }
        catch { }

        if (!PythonEngine.IsInitialized)
        {
            PythonEngine.PythonHome = pythonHome;
            PythonEngine.PythonPath = $"{pythonHome}\\Lib;{pythonHome}\\Lib\\site-packages";
            PythonEngine.Initialize();
        }
    }

    public void RunPythonScript(UIApplication application, string reply)
    {
        if (application.ActiveUIDocument == null)
            return;

        using (Py.GIL())
        {
            using PyModule scope = Py.CreateScope();
            scope.Set("uiapp", application);

            var pythonCode = CleanCodeSnippet(reply);
            try
            {
                scope.Exec(pythonCode);
            }
            catch (PythonException ex)
            {
                TaskDialog.Show("Python Error", ex.Message);
            }
        }
    }

    private string CleanCodeSnippet(string code)
    {
        string[] lines = code.Trim().Split('\n');

        if (lines[0].Trim().StartsWith("```python"))
        {
            lines = lines.Skip(1).ToArray();
        }

        if (lines[lines.Length - 1].Trim() == "```")
        {
            lines = lines.Take(lines.Length - 1).ToArray();
        }

        string cleanSnippet = string.Join("\n", lines);

        cleanSnippet = cleanSnippet.Replace("\\n", "\n");
        cleanSnippet = cleanSnippet.Replace("\\_", "_");
        cleanSnippet = cleanSnippet.Replace("\\*", "*");

        return cleanSnippet;
    }
}
